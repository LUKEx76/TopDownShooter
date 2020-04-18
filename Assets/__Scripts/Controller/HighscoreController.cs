using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreController : MonoBehaviour
{
    private GameController gameController;
    private TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText = FindObjectOfType<TextMeshProUGUI>();
        gameController = FindObjectOfType<GameController>();
        gameController.LoadHighscore();

        scoreText.text = gameController.Highscore.ToString();
    }


}
