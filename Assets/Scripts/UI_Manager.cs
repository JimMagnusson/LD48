using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI depthText;
    [SerializeField] private int depthStartZeroes = 4;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private int highScoreStartZeroes = 4;

    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private int coinStartZeroes = 3;


    private string depthTextStringEndFormat ;
    private string highscoreStringEndFormat;
    private string coinTextStringEndFormat ;
    private DepthMeter depthMeter;
    private ScoreManager scoreManager;

    void Start()
    {
        depthMeter = FindObjectOfType<DepthMeter>();
        depthTextStringEndFormat = depthText.text.Substring(depthStartZeroes, depthText.text.Length - depthStartZeroes);
        coinTextStringEndFormat= coinText.text.Substring(coinStartZeroes, coinText.text.Length - coinStartZeroes);
        highscoreStringEndFormat = highScoreText.text.Substring(highScoreStartZeroes, highScoreText.text.Length - highScoreStartZeroes);

        scoreManager = FindObjectOfType<ScoreManager>();
        highScoreText.text = scoreManager.HighScore.ToString() + highscoreStringEndFormat;
    }

    public void UpdateDepthUI()
    {
        depthText.text = depthMeter.GetMaxDepth().ToString() + depthTextStringEndFormat;
    }

    public void UpdateScoreUI()
    {
        coinText.text = scoreManager.Score.ToString() + coinTextStringEndFormat;
    }
}
