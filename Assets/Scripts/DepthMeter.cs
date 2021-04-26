using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthMeter : MonoBehaviour
{
    [SerializeField] private float currentDepth = 0;
    private float depthOffset = 0;
    private float maxDepth = 0;

    // Highly coupled, would change if I had more time.
    private ScoreManager scoreManager;
    private UI_Manager ui_Manager;

    // Start is called before the first frame update
    void Start()
    {
        depthOffset = transform.position.y;
        ui_Manager = FindObjectOfType<UI_Manager>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentDepth = depthOffset - transform.position.y;
        if (maxDepth < currentDepth)
        {
            maxDepth = currentDepth;
            scoreManager.UpdateScore();
            ui_Manager.UpdateDepthUI();
        }
    }

    public int GetCurrentDepth()
    {
        return (int)(currentDepth + 0.5);
    }

    public int GetMaxDepth()
    {
        return (int)(maxDepth + 0.5);
    }
}
