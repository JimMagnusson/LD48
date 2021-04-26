using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] float timeToReachTarget = 1f;
    [SerializeField] int targetYPos = 50;

    [SerializeField] Image mainMenuIdle;
    [SerializeField] Image mainMenuPlay;
    [SerializeField] Image mainMenuQuit;
    [SerializeField] Image introductionIdle;
    [SerializeField] Image introductionContinue;
    [SerializeField] Image title;
    [SerializeField] GameObject introductionMenu;

    [SerializeField] Button playButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button continueButton;

    private LevelLoader levelLoader;

    private Vector2 startAnchorPos = Vector2.zero;
    private Vector2 targetAnchorPos = Vector2.zero;
    private bool isMoving = false;
    private float t = 0;

    private void Start()
    {
        mainMenuIdle.enabled = true;
        mainMenuPlay.enabled = false;
        mainMenuQuit.enabled = false;
        introductionIdle.enabled = false;
        introductionContinue.enabled = false;
        title.enabled = true;
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    private void Update()
    {
        if (isMoving)
        {
            t += Time.unscaledDeltaTime / timeToReachTarget;
            introductionMenu.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(startAnchorPos, targetAnchorPos, t);
        }
    }

    public void EnterGame()
    {
        levelLoader.LoadNextScene();
    }

    public void ShowIntroductionContinue()
    {
        introductionIdle.enabled = false;
        introductionContinue.enabled = true;
    }

    public void ShowIntroductionIdle()
    {
        introductionIdle.enabled = true;
        introductionContinue.enabled = false;
    }

    public void EnterIntroduction()
    {
        MoveIntroductionMenu();
        introductionIdle.gameObject.SetActive(true);
        introductionContinue.gameObject.SetActive(true);
        introductionIdle.enabled = true;
        introductionContinue.enabled = false;

        mainMenuIdle.enabled = true;
        mainMenuPlay.enabled = false;
        playButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowMainMenuPlay()
    {
        mainMenuIdle.enabled = false;
        mainMenuPlay.enabled = true;
    }

    public void ShowMainMenuQuit()
    {
        mainMenuIdle.enabled = false;
        mainMenuQuit.enabled = true;
    }

    public void ShowMainMenuIdle()
    {
        mainMenuIdle.enabled = true;
        mainMenuQuit.enabled = false;
        mainMenuPlay.enabled = false;
    }

    private void MoveIntroductionMenu()
    {
        startAnchorPos = introductionMenu.GetComponent<RectTransform>().anchoredPosition;
        targetAnchorPos = new Vector2(0, targetYPos);
        isMoving = true;
        t = 0;
    }
}
