using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private int startingHealth;
    [SerializeField] private TextMeshProUGUI scoreText; //UI Display of Score
    [SerializeField] private TextMeshProUGUI coinText; //UI Display of Coins

    private int maxHealth;
    private int currentHealth;
    private int playerScore;
    private int collectedCoins;

    public int MaxHealth { get => maxHealth; }
    public int CurrentHealth { get => currentHealth; }

    private LifeCounter lifeCounter;

    void Awake()
    {
        SetupSingleton();
        collectedCoins = 0;
        playerScore = 0;
        maxHealth = startingHealth;
        currentHealth = startingHealth;
    }

    void Start()
    {
        lifeCounter = FindObjectOfType<LifeCounter>();
        //lifeCounter.DrawHealth();
        UpdateUI();
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
        Box.BoxDestroyedEvent += OnBoxDestroyedEvent;
    }

    void OnDisable()
    {
        //Unsubsribtion to Events
        Enemy.EnemyKilledEvent -= OnEnemyKilledEvent;
        Box.BoxDestroyedEvent += OnBoxDestroyedEvent;
    }

    void OnEnemyKilledEvent(Enemy enemy)
    {
        playerScore += enemy.ScoreValue;
        UpdateUI();


        //Check if There are still Enemies in the Level
        //FindObjectsOfType<Enemy>().Length - 1;
    }

    void OnBoxDestroyedEvent(Box box)
    {
        playerScore += box.ScoreValue;
        UpdateUI();
    }

    void UpdateUI()
    {
        //UI Display
        scoreText.text = "Score: " + playerScore.ToString();
        coinText.text = "Coins: " + collectedCoins.ToString();
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

    public void HealFor(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        lifeCounter.DrawHealth();
    }

    public void AddCoins(int coinValue)
    {
        collectedCoins += coinValue;
        playerScore += coinValue * 100;
        UpdateUI();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
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


    public void Reset()
    {
        //When going back to Main Menu reset GameController
        Time.timeScale = 1;
        Destroy(this.gameObject);
    }
}
