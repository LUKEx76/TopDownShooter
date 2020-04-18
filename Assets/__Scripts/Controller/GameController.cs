using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameController : MonoBehaviour
{

    [SerializeField] private int startingHealth;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip gameWonSound;
    private TextMeshProUGUI scoreText; //UI Display of Score
    private TextMeshProUGUI coinText; //UI Display of Coins
    private SceneController sceneController;
    private LifeCounter lifeCounter;
    private AudioController audioController;

    private int maxHealth;
    private int currentHealth;
    private int playerScore;
    private int collectedCoins;
    private int highscore;
    private int level;
    private int stage;
    private bool muted;
    private bool heartUpgrade;
    private bool fireRateUpgrade;
    private bool trippleBulletUpgrade;


    public int MaxHealth { get => maxHealth; }
    public int CurrentHealth { get => currentHealth; }
    public int CollectedCoins { get => collectedCoins; }
    public int PlayerScore { get => playerScore; }
    public int Highscore { get => highscore; }
    public int Level { get => level; set => level = value; }
    public int Stage { get => stage; }
    public bool Muted { get => muted; }
    public bool HeartUpgrade { get => heartUpgrade; }
    public bool FireRateUpgrade { get => fireRateUpgrade; }
    public bool TrippleBulletUpgrade { get => trippleBulletUpgrade; }


    void Awake()
    {
        SetupSingleton();
        collectedCoins = 0;
        playerScore = 0;
        maxHealth = startingHealth;
        currentHealth = startingHealth;
        muted = false;
        heartUpgrade = false;
        fireRateUpgrade = false;
        trippleBulletUpgrade = false;
        LoadHighscore();
        stage = 1;
        level = 0;
    }

    void Start()
    {
        try
        {
            scoreText = FindObjectOfType<ScoreText>().GetComponent<TextMeshProUGUI>();
            coinText = FindObjectOfType<CoinText>().GetComponent<TextMeshProUGUI>();
        }
        catch
        {
            scoreText = null;
            coinText = null;
        }

        audioController = FindObjectOfType<AudioController>();
        sceneController = FindObjectOfType<SceneController>();
        lifeCounter = FindObjectOfType<LifeCounter>();
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
        try
        {
            scoreText = FindObjectOfType<ScoreText>().GetComponent<TextMeshProUGUI>();
            coinText = FindObjectOfType<CoinText>().GetComponent<TextMeshProUGUI>();
        }
        catch
        {
            scoreText = null;
            coinText = null;
        }
        //UI Display

        if (scoreText && coinText)
        {
            scoreText.text = "Score: " + playerScore.ToString();
            coinText.text = collectedCoins.ToString();
        }
    }

    public void LoseOneHealth()
    {
        currentHealth--;
        //Draw Health Icons on UI
        lifeCounter = FindObjectOfType<LifeCounter>();

        lifeCounter.DrawHealth();

        if (currentHealth <= 0)
        {
            sceneController = FindObjectOfType<SceneController>();
            audioController = FindObjectOfType<AudioController>();
            audioController.PlayOneShot(gameOverSound);
            PauseGame();
            sceneController.LoadGameOver();
            //Destroy(FindObjectOfType<PlayerMovement>().gameObject); Causes Unity to crash
        }
    }

    public void HealFor(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        lifeCounter = FindObjectOfType<LifeCounter>();
        if (lifeCounter)
        {
            lifeCounter.DrawHealth();
        }
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

    public void ToggleSound()
    {
        muted = !muted;
    }

    public void Reset()
    {
        //When going back to Main Menu reset GameController
        Time.timeScale = 1;
        Destroy(this.gameObject);
    }

    public void Portal()
    {
        sceneController = FindObjectOfType<SceneController>();
        audioController = FindObjectOfType<AudioController>();

        if (level == 3 && stage == 3)
        {
            audioController.PlayOneShot(gameWonSound);
            PauseGame();
            sceneController.LoadGameWon();
        }
        else
        {
            PauseGame();
            sceneController.LoadShop();
        }
    }

    public void NextStage()
    {
        if (level == 0)
        {
            level = 1;
        }
        else
        {
            stage++;
            if (stage > 3)
            {
                level++;
                stage = 0;
            }
        }
        sceneController.LoadNextStage();
        ResumeGame();
    }

    public void SpendCoins(int amount)
    {
        collectedCoins -= amount;
        if (collectedCoins < 0)
        {
            collectedCoins = 0;
        }
        UpdateUI();
    }

    public void UpgradeExtraHeart()
    {
        heartUpgrade = true;
        maxHealth += 2;
        currentHealth += 2;
        lifeCounter = FindObjectOfType<LifeCounter>();
        if (lifeCounter)
        {
            lifeCounter.DrawHealth();
        }
    }

    public void UpgradeFireRate()
    {
        //WeaponController has to check if Upgrade is True
        fireRateUpgrade = true;
    }

    public void UpgradeTrippleBullet()
    {
        //WeaponController has to check if Upgrade is True
        trippleBulletUpgrade = true;
    }


    public void SaveHighscore()
    {
        highscore = PlayerPrefs.GetInt("Highscore");
        if (playerScore > highscore)
        {
            PlayerPrefs.SetInt("Highscore", playerScore);
        }
        sceneController.LoadMainMenu();
        Reset();
    }

    public void LoadHighscore()
    {
        highscore = PlayerPrefs.GetInt("Highscore");
    }
}
