using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 6f;

    [SerializeField] private int damage = 1;

    [SerializeField] private float knockbackForce = 10;

    public float Speed { get { return speed; } }

    public int Damage { get { return damage; } }

    public float KnockbackForce { get { return knockbackForce; } }

}
