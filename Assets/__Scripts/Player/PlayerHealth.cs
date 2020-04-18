using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private AudioClip hitTakenSound;
    [SerializeField] private float invulnerabilityAfterHit = 0.5f;

    private float currentInvulnerability;

    private GameController gameController;

    private SpriteRenderer spriteRenderer;

    private AudioController audioController;

    //Maybe disable Components after Hit
    //private PlayerMovement playerMovement;
    //private WeaponController weaponController;


    void Start()
    {
        audioController = FindObjectOfType<AudioController>();
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
        var apple = whatHitMe.GetComponent<Apple>();
        var coin = whatHitMe.GetComponent<Coin>();
        var portal = whatHitMe.GetComponent<Portal>();

        if (projectile && !projectile.FriendlyFire)
        {
            HitTaken();
            audioController.PlayOneShot(projectile.DestroySound);
            Destroy(projectile.gameObject);
        }

        if (apple)
        {
            gameController.HealFor(apple.HealsFor);
            audioController.PlayOneShot(apple.ConsumedSound);
            Destroy(apple.gameObject);
        }

        if (coin)
        {
            gameController.AddCoins(coin.CoinValue);
            audioController.PlayOneShot(coin.CollectedSound);
            Destroy(coin.gameObject);
        }

        if (portal)
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
            gameController.Portal();
        }
    }

    public void HitTaken()
    {
        if (currentInvulnerability <= 0)
        {
            audioController.PlayOneShot(hitTakenSound);
            currentInvulnerability = invulnerabilityAfterHit;
            gameController.LoseOneHealth();
        }
    }
}
