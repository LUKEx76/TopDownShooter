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

    [SerializeField] private AudioClip destroySound;

    [SerializeField] private bool friendlyFire;

    public float Speed { get => speed; }

    public int Damage { get => damage; }

    public float KnockbackForce { get => knockbackForce; }

    public AudioClip DestroySound { get => destroySound; }

    public bool FriendlyFire { get => friendlyFire; }

}
