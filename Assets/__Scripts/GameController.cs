using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private int startingHealth;
    [SerializeField] private TextMeshProUGUI scoreText; //UI Display of Score

    private int maxHealth;
    private int currentHealth;
    private int playerScore;


    void Awake()
    {
        SetupSingleton();
        playerScore = 0;
        currentHealth = startingHealth;
    }

    void Start()
    {
        UpdateScore();
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

        //Check if There are still Enemies in the Level
        //FindObjectsOfType<Enemy>().Length - 1;
    }

    void UpdateScore()
    {
        Debug.Log("Score: " + playerScore);

        //UI Display
        //scoreText.text = playerScore.ToString();
    }

    public void LoseOneHealth()
    {
        currentHealth--;
        Debug.Log("PLAYER HEALTH: " + currentHealth);

        //Draw Health Icons on UI
        if (currentHealth <= 0)
        {
            //GAME OVER
            Debug.Log("GAME OVER");
        }
    }
}
