using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Box : MonoBehaviour
{

    private Tilemap tilemap;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }


    void OnTriggerStay2D(Collider2D whatHitMe)
    {
        var projectile = whatHitMe.GetComponent<Projectile>();
        if (projectile)
        {
            if (tilemap.HasTile(tilemap.WorldToCell(projectile.transform.position)))
            {

                tilemap.SetTile(tilemap.WorldToCell(projectile.transform.position), null);

                //Sound and Animation before Destroy
                Destroy(projectile.gameObject);
            }

        }

    }
}
