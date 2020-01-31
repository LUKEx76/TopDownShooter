using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;

    public float BulletSpeed { get { return bulletSpeed; } }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
