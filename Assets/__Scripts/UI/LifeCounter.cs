using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    [SerializeField] private LifeIcon fullHeart;
    [SerializeField] private LifeIcon halfHeart;
    [SerializeField] private LifeIcon emptyHeart;

    private GameController gameController;

    private int currentHealth;

    private int maxHealth;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        DrawHealth();
    }

    public void DrawHealth()
    {
        if (gameController)
        {
            //Clear Images
            int children = transform.childCount;
            for (int i = 0; i < children; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

            //Draw new Images
            currentHealth = gameController.CurrentHealth;
            maxHealth = gameController.MaxHealth;

            for (int i = 0; i < maxHealth; i += 2)
            {
                if (i < currentHealth)
                {
                    //Draw actual Health
                    if (i + 1 == currentHealth)
                    {
                        //Draw Only Half an Heart
                        Instantiate(halfHeart, transform);
                    }
                    else
                    {
                        //Draw a Full Heart
                        Instantiate(fullHeart, transform);
                    }
                }
                else
                {
                    //Draw an Empty Heart
                    Instantiate(emptyHeart, transform);
                }

            }
        }
    }
}
