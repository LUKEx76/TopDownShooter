using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int startingHealth;
    [SerializeField] private TextMeshProUGUI scoreText; //UI Display of Score

    private int currentHealth;

    private int playerScore = 0;

    void Awake()
    {
        SetupSingleton();
    }

    void Start()
    {
        UpdateScore();
        currentHealth = startingHealth;
    }

    void SetupSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            //If GameController already exists
            Destroy(gameObject);
        }
        else
        {
            //Scene persistence
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnEnable()
    {
        //Subsribtion to Events
        Enemy.EnemyKilledEvent += OnEnemyKilledEvent;
    }

    void OnDisable()
    {
        //Unsubsribtion to Events
        Enemy.EnemyKilledEvent -= OnEnemyKilledEvent;
    }

    void OnEnemyKilledEvent(Enemy enemy)
    {
        playerScore += enemy.ScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        Debug.Log("Score: " + playerScore);

        //UI Display
        //scoreText.text = playerScore.ToString();
    }
}
