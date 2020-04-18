using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;  //To load and swap between Scenes

public class SceneController : MonoBehaviour
{
    private GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public void LoadDevLevel()
    {
        SceneManager.LoadSceneAsync(SceneNames.DEV_TEST_LEVEL);
    }

    public void LoadMainMenu()
    {
        gameController.Reset();
        SceneManager.LoadSceneAsync(SceneNames.MAIN_MENU);
    }

    public void LoadPrototyp()
    {
        SceneManager.LoadSceneAsync(SceneNames.PROTOTYP);
    }

    public void LoadInGameMenu()
    {
        gameController.PauseGame();
        SceneManager.LoadSceneAsync(SceneNames.INGAME_MENU, LoadSceneMode.Additive);
    }

    public void PopInGameMenu()
    {
        gameController.ResumeGame();
        SceneManager.UnloadSceneAsync(SceneNames.INGAME_MENU);
    }

    public void LoadSelectLevel()
    {
        SceneManager.LoadSceneAsync(SceneNames.SELECT_LEVEL, LoadSceneMode.Additive);
    }

    public void LoadHighscore()
    {
        SceneManager.LoadSceneAsync(SceneNames.HIGHSCORE, LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadShop()
    {
        SceneManager.LoadSceneAsync(SceneNames.SHOP, LoadSceneMode.Additive);
    }

    public void PopShop()
    {
        gameController.ResumeGame();
        SceneManager.UnloadSceneAsync(SceneNames.SHOP);
    }

    public void LoadTutorial()
    {
        SceneManager.LoadSceneAsync(SceneNames.TUTORIAL);
    }

    public void LoadLevel1()
    {
        gameController.Level = 1;
        SceneManager.LoadSceneAsync(SceneNames.LEVEL1_1);
    }

    public void LoadLevel2()
    {
        gameController.Level = 2;
        SceneManager.LoadSceneAsync(SceneNames.LEVEL2_1);
    }

    public void LoadLevel3()
    {
        gameController.Level = 3;
        SceneManager.LoadSceneAsync(SceneNames.LEVEL3_1);
    }

    public void PopHighscore()
    {
        SceneManager.UnloadSceneAsync(SceneNames.HIGHSCORE);
    }

    public void PopSelectLevel()
    {
        SceneManager.UnloadSceneAsync(SceneNames.SELECT_LEVEL);
    }

    public void LoadGameOver()
    {
        SceneManager.LoadSceneAsync(SceneNames.GAME_OVER, LoadSceneMode.Additive);
    }

    public void LoadGameWon()
    {
        SceneManager.LoadSceneAsync(SceneNames.GAME_WON, LoadSceneMode.Additive);
    }

    public void NextStage()
    {
        gameController.NextStage();
    }

    public void LoadNextStage()
    {
        switch (gameController.Level)
        {
            case 1:
                switch (gameController.Stage)
                {
                    case 1:
                        SceneManager.LoadSceneAsync(SceneNames.LEVEL1_1);
                        break;

                    case 2:
                        SceneManager.LoadSceneAsync(SceneNames.LEVEL1_2);
                        break;

                    case 3:
                        SceneManager.LoadSceneAsync(SceneNames.LEVEL1_3);
                        break;
                }
                break;

            case 2:
                switch (gameController.Stage)
                {
                    case 1:
                        SceneManager.LoadSceneAsync(SceneNames.LEVEL2_1);
                        break;

                    case 2:
                        SceneManager.LoadSceneAsync(SceneNames.LEVEL2_2);
                        break;

                    case 3:
                        SceneManager.LoadSceneAsync(SceneNames.LEVEL2_3);
                        break;
                }
                break;

            case 3:
                switch (gameController.Stage)
                {
                    case 1:
                        SceneManager.LoadSceneAsync(SceneNames.LEVEL3_1);
                        break;

                    case 2:
                        SceneManager.LoadSceneAsync(SceneNames.LEVEL3_2);
                        break;

                    case 3:
                        SceneManager.LoadSceneAsync(SceneNames.LEVEL3_3);
                        break;
                }
                break;
        }
    }
}
