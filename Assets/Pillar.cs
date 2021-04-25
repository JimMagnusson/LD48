using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    [SerializeField] GameObject oneSegmentPlatformPrefab = null;
    [SerializeField] GameObject twoSegmentPlatformPrefab = null;

    [SerializeField] int numberOfSegmentPrefabs = 2;

    [SerializeField] float distanceBetweenPlatforms = 0f;

    [SerializeField] float localPosAtTop = 11.8f;

    private GameObject platformPrefab = null;

    void Start()
    {
        // Spawn three platforms
        for(int i = 0; i < 3; i++)
        {
            int randomNum = Random.Range(0, numberOfSegmentPrefabs);
            switch (randomNum)
            {
                case 0:
                    platformPrefab = oneSegmentPlatformPrefab;
                    break;
                case 1:
                    platformPrefab = twoSegmentPlatformPrefab;
                    break;
                default:
                    Debug.LogError("No switch case found for segment number: " + randomNum);
                    break;
            }
            GameObject platform = Instantiate(platformPrefab, transform.position, Quaternion.identity, transform);
            float offsetFromTop = distanceBetweenPlatforms / 2;
            platform.transform.localPosition = new Vector3(0, localPosAtTop - i * distanceBetweenPlatforms - offsetFromTop, 0);
        }
    }
}
