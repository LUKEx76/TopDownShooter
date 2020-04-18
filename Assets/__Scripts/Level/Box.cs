using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Box : MonoBehaviour
{
    [SerializeField] private int scoreValue = 10;
    [SerializeField] private AudioClip destroySound;
    private Tilemap tilemap;

    private AudioController audioController;

    public int ScoreValue { get => scoreValue; }

    //Delegate Function to be reacted on by GameController
    public delegate void BoxDestroyed(Box box);
    public static BoxDestroyed BoxDestroyedEvent;//Static Method to be implemented in the Listener(GameController)

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        audioController = FindObjectOfType<AudioController>();
    }


    void OnTriggerStay2D(Collider2D whatHitMe)
    {
        var projectile = whatHitMe.GetComponent<Projectile>();
        if (projectile)
        {
            if (tilemap.HasTile(tilemap.WorldToCell(projectile.transform.position)))
            {
                audioController.PlayOneShot(destroySound);
                PublishBoxDestroyedEvent();
                tilemap.SetTile(tilemap.WorldToCell(projectile.transform.position), null);

                //Sound and Animation before Destroy
                audioController.PlayOneShot(projectile.DestroySound);
                Destroy(projectile.gameObject);
            }
        }
    }

    public void PublishBoxDestroyedEvent()
    {
        if (BoxDestroyedEvent != null) //Check for Subscribtions
        {
            BoxDestroyedEvent(this);
        }
    }
}
