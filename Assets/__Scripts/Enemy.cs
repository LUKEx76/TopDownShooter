using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 5f;

    [SerializeField] private float liveTime = 10f;

    private float age;

    public float GetSpeed()
    {
        return enemySpeed;
    }

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

    private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        //parameter "whatHitMe" is the collider component of the Object hit
        //Diffrent behaviours required depending of who was collided with

        //Could check the Tag Type of the Object
        //Could check the different Components
        var player = whatHitMe.GetComponent<PlayerMovement>();
        var bullet = whatHitMe.GetComponent<Bullet>();
        if (player)
        {
            //Destroy Enemy (this)
            //Play Detonation Sound
            player.Respawn(); // or inflict Damage
        }
        if (bullet)
        {
            //Destroy Enemy (this)
            //Play Detonation Sound
            Destroy(bullet.gameObject);
            Destroy(gameObject);
        }
    }
}
