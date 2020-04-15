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

    public int MaxHealth { get => maxHealth; }
    public int CurrentHealth { get => currentHealth; }

    private LifeCounter lifeCounter;

    void Awake()
    {
        SetupSingleton();
        playerScore = 0;
        maxHealth = startingHealth;
        currentHealth = startingHealth;
    }

    void Start()
    {
        lifeCounter = FindObjectOfType<LifeCounter>();
        lifeCounter.DrawHealth();
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
        //UI Display
        scoreText.text = "Score: " + playerScore.ToString();
    }

    public void LoseOneHealth()
    {
        currentHealth--;
        //Draw Health Icons on UI
        lifeCounter.DrawHealth();

        if (currentHealth <= 0)
        {
            Destroy(FindObjectOfType<PlayerMovement>().gameObject);
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void TogglePauseResume()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}
