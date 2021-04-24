using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Prefabs

    private enum PlatformType
    {
        oneSegment,
        twoSegments
    }

    private enum ObstacleType
    {
        stoneWall,
        rocks,
        dynamite
    }

    [SerializeField] [Tooltip("In degrees")] int holeSizeOneSegment = 70;

    [SerializeField] private PlatformType platformType = PlatformType.oneSegment;

    [SerializeField] private int maxObstacles = 3;
    [SerializeField] private int minObstacles = 0;

    [SerializeField] private int wallWidth = 30;
    [SerializeField] private int rocksWidth = 30;
    [SerializeField] private int dynamiteWidth = 30;
    [SerializeField] private int placementTries = 100;

    [SerializeField] private int DEGREES_IN_CIRCLE = 360;

    private int[] validPlacementAngles;
    private int numberOfObstaclesToPlace;
    private List<ObstacleType> placeableObstacles;
    private ObstacleType objectToPlace;

    private bool canPlaceObject = true;


    void Start()
    {
        RandomizeRotation();
        GetValidPlacementAngles();
        DetermineNumberOfObstaclesToPlace();
        DeterminePlaceableObstacles();

        for(int i = 0; i < numberOfObstaclesToPlace; i++)
        {
            SelectObjectToPlace();
            CheckIfObstacleCanBePlaced();
        }
    }

    private void CheckIfObstacleCanBePlaced()
    {
        int tries = 0;
        bool isFinished = false;
        int width = 0;
        switch(objectToPlace)
        {
            // TODO:
        }

        while(isFinished || tries >= placementTries)
        {
            int randomStartingIndex = UnityEngine.Random.Range(0, validPlacementAngles.Length - 1);
            int randomStartingAngle = validPlacementAngles[randomStartingIndex];
            //TODO:
            //for(int i = 1; i < d)
            tries++;
        }
    }

    private void RandomizeRotation()
    {
        float randomAngle = UnityEngine.Random.Range(0, 359);
        transform.Rotate(Vector3.up, randomAngle);
    }

    private void GetValidPlacementAngles()
    {
        switch(platformType)
        {
            case PlatformType.oneSegment:
                validPlacementAngles = new int[DEGREES_IN_CIRCLE - holeSizeOneSegment];
                for (int i = 0; i < 280; i++)
                {
                    validPlacementAngles[i] = i;
                }
                for(int i = 280; i < 290; i++)
                {
                    validPlacementAngles[i] = i + holeSizeOneSegment;
                }
                break;
            case PlatformType.twoSegments:
                // TODO: Make
                break;
            default:
                Debug.Log("Invalid platformType");
                break;

        }
    }

    private void DetermineNumberOfObstaclesToPlace()
    {
        numberOfObstaclesToPlace = UnityEngine.Random.Range(minObstacles, maxObstacles);
    }

    private void DeterminePlaceableObstacles()
    {
        placeableObstacles = new List<ObstacleType>();
        for (int i = 0; i < System.Enum.GetValues(typeof(ObstacleType)).Length; i++)
        {
            placeableObstacles.Add((ObstacleType)i);
        }

    }
    private void SelectObjectToPlace()
    {
        int randomIndex = UnityEngine.Random.Range(0, placeableObstacles.Count);
        objectToPlace = placeableObstacles[randomIndex];
        // Can only have one wall per platform
        if (objectToPlace == ObstacleType.stoneWall)
        {
            placeableObstacles.Remove(objectToPlace);
        }
    }
}
