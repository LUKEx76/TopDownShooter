using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MeleeEnemy : Enemy
{
    private GameObject player;
    private Rigidbody2D rb;

    [SerializeField] private float movementSpeed = 5f;

    [SerializeField] private float knockBackDuration = 1f;

    private float currentKnockbackTime;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentKnockbackTime = 0;
    }

    void FixedUpdate()
    {
        currentKnockbackTime -= Time.fixedDeltaTime;
        player = FindObjectOfType<PlayerMovement>().gameObject;
        if (player && currentKnockbackTime <= 0)
        {
            Move();
        }
    }

    void Move()
    {
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        if (direction.magnitude > 1f)
        {
            //Move Enemy Towards Player
            rb.MovePosition(rb.position + direction.normalized * movementSpeed * Time.fixedDeltaTime);
            //Rotate Enemy Towards Player
            rb.rotation = angle;
        }
    }

    public override void Knockback(Vector2 direction, int projectileForce)
    {
        currentKnockbackTime = knockBackDuration;
        //StopMoving
        //AddForce
        rb.AddForce(direction * projectileForce, ForceMode2D.Impulse);
    }

}
