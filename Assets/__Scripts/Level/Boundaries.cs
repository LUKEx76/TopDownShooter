using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        var projectile = whatHitMe.GetComponent<Projectile>();
        if (projectile)
        {
            Destroy(projectile.gameObject);
        }
    }
}
