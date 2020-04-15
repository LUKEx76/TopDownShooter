using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeEnemy : Enemy
{
    private GameObject player;
    private Rigidbody2D rb;

    [SerializeField] private float movementSpeed = 3f;

    [SerializeField] private float knockbackDuration = 0.1f;
    [SerializeField] private float attackKnockbackForce = 1;

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
        else
        {
            Attack();
        }
    }

    void Attack()
    {
        //Imflickt Damage
        if (player)
        {
            player.GetComponent<PlayerHealth>().HitTaken();
        }
        Knockback(Util.Vector2fromAngle(rb.rotation - 180), attackKnockbackForce);
    }

    public override void Knockback(Vector2 direction, float force)
    {
        currentKnockbackTime = knockbackDuration;
        //StopMoving
        //AddForce
        rb.AddForce(direction.normalized * force, ForceMode2D.Impulse);
    }

}
