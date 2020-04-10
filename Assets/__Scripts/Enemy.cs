using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    //Parent Class for all Enemies
    //Holds values for Score-, Healthpoints
    //Handels Hit Detection
    //Publishes Events for Sound- and GameController

    [SerializeField] private int StartHealth = 5;

    private int currentHealth;

    private Rigidbody2D rigidbody;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        currentHealth = StartHealth;
    }


    void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        var projectile = whatHitMe.GetComponent<Projectile>();
        if (projectile)
        {
            currentHealth -= projectile.Damage;

            //Knockback
            //Stop moveposition before Knockback
            var mellee = GetComponent<MeleeBehaviour>();
            if (mellee)
            {
                Debug.Log("Hit melee");
                mellee.Knockback(projectile.transform.rotation * Vector2.up, projectile.KnockbackForce);
            }

            if (currentHealth <= 0)
            {
                Destroy(projectile.gameObject);
                Destroy(gameObject);
            }
            Destroy(projectile.gameObject);
        }
    }

    //Could do an Inteface for Behaviour!!
    /*interface IKnockback
    {
        void knockback(Vector2 direction, int projectileForce);
    }*/
}
