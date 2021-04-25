using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverController : MonoBehaviour
{
    [SerializeField] Image gameOverIdle;
    [SerializeField] Image gameOverRestart;
    [SerializeField] Image gameOverQuit;
    [SerializeField] Button quitButton;
    [SerializeField] Button restartButton;

    [SerializeField] TextMeshProUGUI depthText;
    [SerializeField] int depthStartZeroes = 4;

    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] int highScoreStartZeroes = 3;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int scoreTextStartZeroes = 3;

    private LevelLoader levelLoader;
    private ScoreManager scoreManager;

    private string depthTextStringEndFormat;
    private string highscoreStringEndFormat;
    private string scoreStringEndFormat;

    private void Start()
    {
        gameOverIdle.enabled = true;
        gameOverRestart.enabled = false;
        gameOverQuit.enabled = false;
        levelLoader = FindObjectOfType<LevelLoader>();
        scoreManager = FindObjectOfType<ScoreManager>();

        depthTextStringEndFormat = depthText.text.Substring(depthStartZeroes, depthText.text.Length - depthStartZeroes);
        highscoreStringEndFormat = highScoreText.text.Substring(highScoreStartZeroes, highScoreText.text.Length - highScoreStartZeroes);
        scoreStringEndFormat = scoreText.text.Substring(scoreTextStartZeroes, scoreText.text.Length - scoreTextStartZeroes);


        int depth = PlayerPrefs.GetInt("Depth", 0);
        depthText.text = depth.ToString() + depthTextStringEndFormat;
        highScoreText.text = scoreManager.HighScore.ToString() + highscoreStringEndFormat;
        scoreText.text = PlayerPrefs.GetInt("Score", 0).ToString() + scoreStringEndFormat;

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
