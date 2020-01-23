using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour
{

    [SerializeField] private float liveTime = 3f;

    [SerializeField] private float bulletSpeed = 10f;

    private float age;


    void Start()
    {
        age = liveTime;
    }

    void Update()
    {
        age -= Time.deltaTime;
        if (age <= 0)
        {
            Destroy(gameObject);
        }
    }

    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }

}
