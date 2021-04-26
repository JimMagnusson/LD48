using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] float depthMultiplier = 1f;
    [SerializeField] int topazScore = 10;
    [SerializeField] int emeraldScore = 20;
    [SerializeField] int rubyScore = 30;
    [SerializeField] int sapphireScore = 40;

    public int Score { get; private set; }
    public int HighScore { get; private set; }


    private int gemScore = 0;
    private DepthMeter depthMeter;
    private UI_Manager ui_Manager;

    private void Awake()
    {
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
    }
    private void Start()
    {
        Score = 0;
        depthMeter = FindObjectOfType<DepthMeter>();
        ui_Manager = FindObjectOfType<UI_Manager>();
    }

    public void UpdateScore()
    {
        Score = (int)((depthMeter.GetMaxDepth() * depthMultiplier + gemScore) + 0.5);
        ui_Manager.UpdateScoreUI();
    }

    /*
     * If the score is a new highscore, a new highscore is saved.
     */
    public void SaveHighScore()
    {
        UpdateScore();
        if (Score > HighScore)
        {
            PlayerPrefs.SetInt("HighScore", Score);
        }

        // Save depth and score
        PlayerPrefs.SetInt("Depth", depthMeter.GetMaxDepth());
        PlayerPrefs.SetInt("Score", Score);
    }


    public void AddGemToScore(Gem gemType)
    {
        switch(gemType)
        {
            case Gem.topaz:
                gemScore += topazScore;
                break;
            case Gem.emerald:
                gemScore += emeraldScore;
                break;
            case Gem.ruby:
                gemScore += rubyScore;
                break;
            case Gem.sapphire:
                gemScore += sapphireScore;
                break;
            default:
                Debug.LogError("No switch case for the gem type");
                break;
        }
        UpdateScore();
    }



}
