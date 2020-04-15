using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float invulnerabilityAfterHit = 0.5f;

    private float currentInvulnerability;

    private GameController gameController;

    private SpriteRenderer spriteRenderer;

    //Maybe disable Components after Hit
    //private PlayerMovement playerMovement;
    //private WeaponController weaponController;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameController = FindObjectOfType<GameController>();
        currentInvulnerability = 0;
    }

    void Update()
    {
        //Show Animation during invulnerability
        if (currentInvulnerability > 0)
        {
            currentInvulnerability -= Time.deltaTime;
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }


    void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        var projectile = whatHitMe.GetComponent<Projectile>();
        if (projectile)
        {
            HitTaken();
            Destroy(projectile.gameObject);
        }
    }

    public void HitTaken()
    {
        if (currentInvulnerability <= 0)
        {
            currentInvulnerability = invulnerabilityAfterHit;
            gameController.LoseOneHealth();
        }
    }
}
