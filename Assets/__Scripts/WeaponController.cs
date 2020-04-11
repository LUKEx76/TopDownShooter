using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WeaponController : MonoBehaviour
{
    //PRIVATE FIELDS
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float firingRate = 0.25f;
    [SerializeField] private Joystick aimStick;

    private Transform gun;
    private GameObject bulletParent;
    private Rigidbody2D rb;
    private Coroutine firingCoroutine;
    private bool firing;


    void Start()
    {
        firing = false;
        gun = transform.Find("Gun");
        rb = GetComponent<Rigidbody2D>();
        bulletParent = GameObject.Find("BulletParent");
        if (!bulletParent)
        {
            bulletParent = new GameObject("BulletParent");
        }
    }


    void Update()
    {
        if (aimStick.Vertical != 0f || aimStick.Horizontal != 0f)
        {
            rb.rotation = Mathf.Atan2(aimStick.Vertical, aimStick.Horizontal) * Mathf.Rad2Deg - 90f;
            if (!firing)
            {
                firingCoroutine = StartCoroutine(FireCoroutine());
                firing = true;
            }
        }
        else if (firingCoroutine != null && firing)
        {
            StopCoroutine(firingCoroutine);
            firing = false;
        }
    }


    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            //Instantiate Projectile Prefab
            Projectile projectile = Instantiate(projectilePrefab, bulletParent.transform);
            projectile.transform.position = gun.transform.position;
            projectile.transform.rotation = gun.transform.rotation;

            //Get RB of Projectile
            Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();

            //Get Vector from Shoot Joystick
            Vector2 shootDir = Vector2fromAngle(rb.rotation);
            rbProjectile.velocity = shootDir.normalized * projectile.Speed;
            yield return new WaitForSeconds(firingRate);
        }

    }

    private Vector2 Vector2fromAngle(float angle)
    {
        angle += 90f;
        angle *= Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

}
