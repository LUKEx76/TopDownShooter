using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;

    [SerializeField] private GameObject enemyParent;

    [SerializeField] private float spawnIntervall = 2f;

    private float cooldown;

    void Start()
    {
        cooldown = spawnIntervall;
    }



    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0f)
        {
            cooldown = spawnIntervall;
            Vector2 randomDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

            Enemy enemy = Instantiate(enemyPrefab);
            enemy.transform.position = this.transform.position;

            if (enemyParent)
            {
                enemy.transform.parent = enemyParent.transform;
            }

            Rigidbody2D erb = enemy.GetComponent<Rigidbody2D>();
            erb.velocity = randomDir.normalized * enemy.EnemySpeed;
        }

    }
}
