using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryRangedEnemy : Enemy
{
    private GameObject player;

    private Transform gun;

    private GameObject bulletParent;

    [SerializeField] private float sightDistance = 10f;
    [SerializeField] private LayerMask visibleLayer;
    [SerializeField] private float knockbackDuration = 0.1f;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float firingRate = 0.5f;

    [SerializeField] [Range(0, 1)] private float rotationSpeed;

    private float currentKnockbackTime;

    private Coroutine firingCoroutine;

    private bool firing;

    void Start()
    {
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
        if (player && currentKnockbackTime <= 0)
        {
            if (PlayerInSight())
            {
                //Look towards Player
                transform.rotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);

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
            }
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
            //Look towards Player
            transform.rotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);

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
            //Getting the GameObject of the Collider can lead to an Exception
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
            return false;
        }
    }

    public override void Knockback(Vector2 direction, int projectileForce)
    {
        //Stationary Target cant be knocked back, but stopps shooting
        currentKnockbackTime = knockbackDuration;
    }
}
