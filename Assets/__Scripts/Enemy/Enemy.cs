using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    //Parent Class for all Enemies
    //Holds values for Score-, Healthpoints
    //Handels Hit Detection
    //Publishes Events for Sound- and GameController


    [SerializeField] private int startHealth = 3;
    [SerializeField] private int scoreValue = 100;

    [SerializeField] private AudioClip hitTakenClip;
    [SerializeField] private AudioClip dieClip;

    private int currentHealth;

    private Animator anim;
    protected AudioController audioController;
    private string damageAnimationTrigger = "ProjectileHit";


    //Events for GameController

    //Delegate Function to be reacted on by GameController
    public delegate void EnemyKilled(Enemy enemy);
    public static EnemyKilled EnemyKilledEvent; //Static Method to be implemented in the Listener(GameController)

    public int ScoreValue { get => scoreValue; }

    //DONT USE START() IN PARENT CLASS!!
    void Awake()
    {
        currentHealth = startHealth;
        anim = GetComponent<Animator>();
        audioController = FindObjectOfType<AudioController>();
    }


    void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        var projectile = whatHitMe.GetComponent<Projectile>();
        if (projectile)
        {
            currentHealth -= projectile.Damage;
            anim.SetTrigger(damageAnimationTrigger);

            audioController.PlayOneShot(hitTakenClip);

            //Stop moveposition before Knockback
            Knockback(projectile.transform.rotation * Vector2.up, projectile.KnockbackForce);

            audioController.PlayOneShot(projectile.DestroySound);
            Destroy(projectile.gameObject);

            if (currentHealth <= 0)
            {
                audioController.PlayOneShot(dieClip);
                PublishEnemyKilledEvent();
                Destroy(gameObject);
            }
        }
    }

    public void PublishEnemyKilledEvent()
    {
        if (EnemyKilledEvent != null) //Check for Event-Subscribtions
        {
            EnemyKilledEvent(this);
        }
    }

    //Virtual Functions
    public virtual void Knockback(Vector2 direction, float force) { } //Functionality in Child

}
