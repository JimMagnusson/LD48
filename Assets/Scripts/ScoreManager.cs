using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] float depthMultiplier = 1f;
    public int Score { get; private set; }
    public int HighScore { get; private set; }

    private DepthMeter depthMeter;

    private void Awake()
    {
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
    }
    private void Start()
    {
        Score = 0;
        depthMeter = FindObjectOfType<DepthMeter>();
    }

    private void Update()
    {
        // Get score from new max depth
        Score = (int)((depthMeter.GetMaxDepth() * depthMultiplier) + 0.5);

        // TODO:
        // Get score from coins
    }

    /*
     * If the score is a new highscore, a new highscore is saved.
     */
    public void SaveHighScore()
    {
        if(Score > HighScore)
        {
            PlayerPrefs.SetInt("HighScore", Score);
        }
    }

}
