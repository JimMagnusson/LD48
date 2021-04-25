using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    [SerializeField] Image gameOverIdle;
    [SerializeField] Image gameOverRestart;
    [SerializeField] Image gameOverQuit;
    [SerializeField] Button quitButton;
    [SerializeField] Button restartButton;

    private LevelLoader levelLoader;

    private void Start()
    {
        gameOverIdle.enabled = true;
        gameOverRestart.enabled = false;
        gameOverQuit.enabled = false;
        levelLoader = FindObjectOfType<LevelLoader>();
        // TODO: start animation
    }

    public void Restart()
    {
        levelLoader.LoadSceneWithBuildIndex(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowGameOverRestart()
    {
        gameOverIdle.enabled = false;
        gameOverRestart.enabled = true;
    }

    public void ShowGameOverQuit()
    {
        gameOverIdle.enabled = false;
        gameOverQuit.enabled = true;
    }

    public void ShowGameOverIdle()
    {
        gameOverIdle.enabled = true;
        gameOverQuit.enabled = false;
        gameOverRestart.enabled = false;
    }


    private void Update()
    {
        // Do animation stuff
    }

}
