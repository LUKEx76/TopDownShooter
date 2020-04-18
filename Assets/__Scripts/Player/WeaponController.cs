using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WeaponController : MonoBehaviour
{
    //PRIVATE FIELDS
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float firingRate = 0.25f;
    [SerializeField] private AudioClip shootSound;

    private GameController gameController;
    private AudioController audioController;
    private FixedJoystick shootStick;
    private Transform gun;
    private GameObject bulletParent;
    private Rigidbody2D rb;
    private Coroutine firingCoroutine;
    private bool firing; //To start Coroutine only once and not on every frame


    void Start()
    {
        audioController = FindObjectOfType<AudioController>();
        gameController = FindObjectOfType<GameController>();
        shootStick = FindObjectOfType<ShootJoystick>().gameObject.GetComponent<FixedJoystick>();
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
        if (shootStick.Vertical != 0f || shootStick.Horizontal != 0f)
        {
            //Maybe aimStick Horizontal and Vertical
            rb.rotation = Util.AngleFromVector2(shootStick.Direction);
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
            ShootOneProjectile(rb.rotation);

            //Check if TrippleBullet Upgrade is active
            if (gameController.TrippleBulletUpgrade)
            {
                ShootOneProjectile(rb.rotation + 10f);
                ShootOneProjectile(rb.rotation - 10f);
            }

            //Check if FIreRate is Upgraded
            if (gameController.FireRateUpgrade)
            {
                yield return new WaitForSeconds(firingRate / 2);
            }
            else
            {
                yield return new WaitForSeconds(firingRate);
            }

        }

    }

    void ShootOneProjectile(float directionAngle)
    {
        audioController.PlayOneShot(shootSound);
        //Instantiate Projectile Prefab
        Projectile projectile = Instantiate(projectilePrefab, bulletParent.transform);
        projectile.transform.position = gun.transform.position;
        projectile.transform.rotation = gun.transform.rotation;

        //Get RB of Projectile
        Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();

        //Get Vector from Shoot Joystick
        Vector2 shootDir = Util.Vector2fromAngle(directionAngle);
        rbProjectile.AddForce(shootDir.normalized * projectile.Speed, ForceMode2D.Impulse);
    }


}
