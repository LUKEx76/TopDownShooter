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
        if (gameController)
        {
            gameController.Reset();
        }
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
        SceneManager.LoadSceneAsync(SceneNames.SELECT_LEVEL);
    }

    public void LoadHighscore()
    {
        SceneManager.LoadSceneAsync(SceneNames.HIGHSCORE);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
