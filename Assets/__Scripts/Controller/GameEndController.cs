using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameEndController : MonoBehaviour
{
    private GameController gameController;

    private TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText = FindObjectOfType<TextMeshProUGUI>();
        gameController = FindObjectOfType<GameController>();
        scoreText.text = gameController.PlayerScore.ToString();
    }

    public void EndGame()
    {
        gameController.SaveHighscore();
    }
}
