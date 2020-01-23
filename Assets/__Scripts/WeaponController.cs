using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    [SerializeField] private Camera cam;

    [SerializeField] private Bullet bulletPrefab;

    [SerializeField] private float firingRate = 0.25f;

    [SerializeField] private Joystick joystick;

    private float fireIn;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fireIn = firingRate;
    }


    void Update()
    {
        fireIn -= Time.deltaTime;

        if (cam && (Mathf.Abs(joystick.Horizontal) > 0f || Mathf.Abs(joystick.Vertical) > 0f))
        {

            Vector2 lookDir = new Vector2(joystick.Horizontal, joystick.Vertical);
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

            rb.rotation = angle;
        }

        if ((Mathf.Abs(joystick.Horizontal) > 0f || Mathf.Abs(joystick.Vertical) > 0f) && fireIn <= 0)
        {
            fireIn = firingRate;
            FireBullet();
        }
    }

    private void FireBullet()
    {
        //Instantiate Bullet Prefab
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.transform.position = this.transform.position;

        //Get RB of Bullet
        Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();

        //Get Vector from Shoot Joystick
        Vector2 shootDir = new Vector2(joystick.Horizontal, joystick.Vertical);
        rbb.velocity = shootDir.normalized * bullet.GetBulletSpeed();
    }
}
