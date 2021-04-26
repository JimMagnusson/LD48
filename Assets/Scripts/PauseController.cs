using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField] float timeToReachTarget = 1f;
    [SerializeField] int targetYPos = 50;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Image pauseIdle;
    [SerializeField] Image pauseRestart;
    [SerializeField] Image pauseQuit;
    [SerializeField] Image pauseContinue;

    
    private LevelLoader levelLoader;
    private Vector2 targetAnchorPos = Vector2.zero;
    private bool isMoving = false;
    private float t = 0;
    private Vector2 startAnchorPos = Vector2.zero;
    private bool isPaused = false;

    private void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
        startAnchorPos = pauseMenu.GetComponent<RectTransform>().anchoredPosition;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.P)) && !isPaused)
        {
            PauseGame();
        }
        else if((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.P)) && isPaused)
        {
            ResumeGame();
        }

        if (isMoving)
        {
            t += Time.unscaledDeltaTime / timeToReachTarget;
            pauseMenu.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(startAnchorPos, targetAnchorPos, t);
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        ShowPauseMenu();
    }

    private void ShowPauseMenu()
    {
        MovePauseScreen();

        pauseMenu.SetActive(true);
        pauseIdle.enabled = true;
        pauseRestart.enabled = false;
        pauseQuit.enabled = false;
        pauseContinue.enabled = false;
    }

    private void MovePauseScreen()
    {
        targetAnchorPos = new Vector2(0, targetYPos);
        isMoving = true;
        t = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        pauseMenu.SetActive(false);
        isMoving = false;
        t = 0;
        pauseMenu.GetComponent<RectTransform>().anchoredPosition = startAnchorPos;
    }

    public void Restart()
    {
        ResumeGame();
        levelLoader.LoadSceneWithBuildIndex(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowPauseRestart()
    {
        pauseIdle.enabled = false;
        pauseRestart.enabled = true;
    }

    public void ShowPauseQuit()
    {
        pauseIdle.enabled = false;
        pauseQuit.enabled = true;
    }

    public void ShowPauseContinue()
    {
        pauseIdle.enabled = false;
        pauseContinue.enabled = true;
    }

    public void ShowPauseIdle()
    {
        pauseIdle.enabled = true;
        pauseQuit.enabled = false;
        pauseRestart.enabled = false;
        pauseContinue.enabled = false;
    }

}
