using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private Button heartUpgradeButton;
    [SerializeField] private Button fireRateUpgradeButton;
    [SerializeField] private Button trippleBulletUpgradeButton;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        if (gameController.HeartUpgrade)
        {
            heartUpgradeButton.interactable = false;
        }
        if (gameController.FireRateUpgrade)
        {
            fireRateUpgradeButton.interactable = false;
        }
        if (gameController.TrippleBulletUpgrade)
        {
            trippleBulletUpgradeButton.interactable = false;
        }
        DrawCoins();
    }

    void DrawCoins()
    {
        coinText.text = "Coins: " + gameController.CollectedCoins.ToString();
    }

    public void BuyHeartUpgrade()
    {
        //Alter Cost!
        if (gameController.CollectedCoins >= 3)
        {
            gameController.SpendCoins(3);
            gameController.UpgradeExtraHeart();
            heartUpgradeButton.interactable = false;
        }
        DrawCoins();
    }

    public void BuyFireRateUpgrade()
    {
        //Alter COST!
        if (gameController.CollectedCoins >= 3)
        {
            gameController.SpendCoins(3);
            gameController.UpgradeFireRate();
            fireRateUpgradeButton.interactable = false;
        }
        DrawCoins();
    }

    public void BuyTrippleBulletUpgrade()
    {
        //Alter Cost!
        if (gameController.CollectedCoins >= 8)
        {
            gameController.SpendCoins(8);
            gameController.UpgradeTrippleBullet();
            trippleBulletUpgradeButton.interactable = false;
        }
        DrawCoins();
    }
}
