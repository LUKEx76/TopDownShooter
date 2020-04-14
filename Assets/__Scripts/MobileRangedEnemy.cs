using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileRangedEnemy : Enemy
{
    private GameObject player;

    private Rigidbody2D rb;

    private Transform gun;

    private GameObject bulletParent;


    [SerializeField] private float sightDistance = 10f;
    [SerializeField] private LayerMask visibleLayer;
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float knockbackDuration = 0.1f;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float firingRate = 0.5f;

    private float currentKnockbackTime;

    private Coroutine firingCoroutine;

    private bool firing;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletParent = GameObject.Find("BulletParent");
        if (!bulletParent)
        {
            bulletParent = new GameObject("BulletParent");
        }
        gun = transform.Find("Gun");
        currentKnockbackTime = 0;
        firing = false;
    }

    void FixedUpdate()
    {
        currentKnockbackTime -= Time.fixedDeltaTime;
        player = FindObjectOfType<PlayerMovement>().gameObject;
        //Decide whether to shoot or move
        if (player && currentKnockbackTime <= 0)
        {
            rb.velocity = Vector2.zero;
            if (PlayerInSight())
            {
                rb.rotation = Util.AngleFromVector2(player.transform.position - gun.transform.position);
                if (!firing)
                {
                    Shoot();
                }
            }
            else
            {
                if (firing)
                {
                    StopCoroutine(firingCoroutine);
                    firing = false;
                }
                Move();
            }
        }
    }


    void Move()
    {
        //Replace with A* Pathfinding
        Vector2 direction = player.transform.position - gun.transform.position;
        float angle = Util.AngleFromVector2(direction);

        if (direction.magnitude > 1f)
        {
            //Move Enemy Towards Player
            rb.MovePosition(rb.position + direction.normalized * movementSpeed * Time.fixedDeltaTime);
            //Rotate Enemy Towards Player
            rb.rotation = angle;
        }

    }

    void Shoot()
    {
        firingCoroutine = StartCoroutine(FireCoroutine());
        firing = true;
    }

    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            rb.rotation = Util.AngleFromVector2(player.transform.position - gun.transform.position);
            //Instantiate Projectile Prefab
            Projectile projectile = Instantiate(projectilePrefab, bulletParent.transform);
            projectile.transform.position = gun.transform.position;
            projectile.transform.rotation = gun.transform.rotation;

            //Get RB of Projectile
            Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();

            //Get Direction Vector from Player Position
            Vector2 shootDir = player.transform.position - gun.transform.position;
            //Either AddForce or set Velocity
            rbProjectile.velocity = shootDir.normalized * projectile.Speed;
            yield return new WaitForSeconds(firingRate);
        }
    }

    bool PlayerInSight()
    {
        Debug.DrawRay(gun.transform.position, (player.transform.position - gun.transform.position).normalized * sightDistance, Color.magenta);
        var hit = Physics2D.Raycast(gun.transform.position, player.transform.position - gun.transform.position, sightDistance, visibleLayer);

        try
        {
            var player_hit = hit.collider.gameObject.GetComponent<PlayerMovement>();
            if (player_hit)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            Debug.Log("Error Occured");
        }

        return false;
    }

    public override void Knockback(Vector2 direction, int projectileForce)
    {
        //StopMoving
        currentKnockbackTime = knockbackDuration;

        //Dependent on Mass!
        rb.AddForce(direction * projectileForce, ForceMode2D.Impulse);
    }
}
