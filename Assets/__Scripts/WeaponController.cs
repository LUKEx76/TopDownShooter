using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;

    [SerializeField] private float firingRate = 0.25f;

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
    }

    public void FireBullet()
    {
        if (fireIn <= 0)
        {
            fireIn = firingRate;

            //Instantiate Bullet Prefab
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.transform.position = this.transform.position;

            //Get RB of Bullet
            Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();

            //Get Vector from Shoot Joystick
            Vector2 shootDir = Vector2fromAngle(rb.rotation);
            rbb.velocity = shootDir.normalized * bullet.GetBulletSpeed();
        }
    }

    private Vector2 Vector2fromAngle(float angle)
    {
        angle += 90f;
        angle *= Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}
