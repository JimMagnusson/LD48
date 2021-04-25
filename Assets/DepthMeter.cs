using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthMeter : MonoBehaviour
{
    [SerializeField] private float currentDepth = 0;
    private float depthOffset = 0;
    // Start is called before the first frame update
    void Start()
    {
        depthOffset = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        currentDepth = depthOffset - transform.position.y;
    }

    public int GetCurrentDepth()
    {
        return (int)(currentDepth + 0.5);
    }
}
