using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;

    [SerializeField] private float firingRate = 0.25f;

    [SerializeField] private Joystick aimStick;

    [SerializeField] private GameObject bulletParent;

    private float fireIn;

    private bool firing = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fireIn = firingRate;
    }


    void Update()
    {
        fireIn -= Time.deltaTime;

        //If DualStick Controlls are selected
        if (aimStick.gameObject.activeSelf && (aimStick.Vertical != 0f || aimStick.Horizontal != 0f))
        {
            FireBullet();
        }

        if (firing)
        {
            FireBullet();
        }
    }

    public void onPress()
    {
        FireBullet();
        firing = true;
    }

    public void onRelease()
    {
        firing = false;
    }

    public void FireBullet()
    {
        if (fireIn <= 0)
        {
            fireIn = firingRate;

            //Instantiate Bullet Prefab
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.transform.position = this.transform.position;

            if (bulletParent)
            {
                bullet.transform.parent = bulletParent.transform;
            }

            //Get RB of Bullet
            Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();

            //Get Vector from Shoot Joystick
            Vector2 shootDir = Vector2fromAngle(rb.rotation);
            rbb.velocity = shootDir.normalized * bullet.BulletSpeed;


        }
    }

    private Vector2 Vector2fromAngle(float angle)
    {
        angle += 90f;
        angle *= Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}
