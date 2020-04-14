using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeEnemy : Enemy
{
    private GameObject player;
    private Rigidbody2D rb;

    [SerializeField] private float movementSpeed = 3f;

    [SerializeField] private float knockbackDuration = 0.1f;

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
        //Replace with A* Pathfinding
        Vector2 direction = player.transform.position - transform.position;
        float angle = Util.AngleFromVector2(direction);

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
        currentKnockbackTime = knockbackDuration;
        //StopMoving
        //AddForce
        rb.AddForce(direction * projectileForce, ForceMode2D.Impulse);
    }

}
