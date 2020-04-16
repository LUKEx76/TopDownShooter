using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawner : MonoBehaviour
{
    //Spawns one Prefab as Children of SpawnParent on the Spawners Position
    //Triggers on Start()

    [SerializeField] private GameObject prefab;

    private GameObject spawnParent;

    void Awake()
    {
        //Find Parent or Create it
        //Keeps Parent unique in the Scene
        spawnParent = GameObject.Find("SpawnParent");
        if (!spawnParent)
        {
            spawnParent = new GameObject("SpawnParent");
        }

        if (prefab)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        //Spawn Prefab on Spawner Position
        var instance = Instantiate(prefab, spawnParent.transform);
        instance.transform.position = transform.position;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.25f);
    }
}
