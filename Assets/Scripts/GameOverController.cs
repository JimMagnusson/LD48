using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameOverController : MonoBehaviour
{
    [SerializeField] float timeToReachTarget = 1f;
    [SerializeField] int targetYPos = 50;

    [SerializeField] GameObject gameOverMenu;
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
    private DepthMeter depthMeter;

    private string depthTextStringEndFormat;
    private string highscoreStringEndFormat;
    private string scoreStringEndFormat;

    private Vector2 currentAnchoredPos = Vector2.zero;
    private Vector2 targetAnchorPos = Vector2.zero;
    private bool isMoving = false;
    private float t = 0;

    private void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
        scoreManager = FindObjectOfType<ScoreManager>();
        depthMeter = FindObjectOfType<DepthMeter>();
    }

    private void Update()
    {
        if(isMoving)
        {
            t += Time.deltaTime / timeToReachTarget;
            gameOverMenu.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(currentAnchoredPos, targetAnchorPos, t);
        }
    }

    public void ShowGameOverMenu()
    {
        MoveGameOverScreen();

        gameOverMenu.SetActive(true);
        gameOverIdle.enabled = true;
        gameOverRestart.enabled = false;
        gameOverQuit.enabled = false;

        depthTextStringEndFormat = depthText.text.Substring(depthStartZeroes, depthText.text.Length - depthStartZeroes);
        highscoreStringEndFormat = highScoreText.text.Substring(highScoreStartZeroes, highScoreText.text.Length - highScoreStartZeroes);
        scoreStringEndFormat = scoreText.text.Substring(scoreTextStartZeroes, scoreText.text.Length - scoreTextStartZeroes);

        depthText.text = depthMeter.GetMaxDepth().ToString() + depthTextStringEndFormat;
        highScoreText.text = scoreManager.HighScore.ToString() + highscoreStringEndFormat;
        scoreText.text = scoreManager.Score.ToString() + scoreStringEndFormat;
    }

    private void MoveGameOverScreen()
    {
        currentAnchoredPos = gameOverMenu.GetComponent<RectTransform>().anchoredPosition;
        targetAnchorPos = new Vector2(0, targetYPos);
        isMoving = true;
        t = 0;
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

}
