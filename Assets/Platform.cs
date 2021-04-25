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
    [SerializeField] private int maxPlacementTries = 100;

    [SerializeField] private int DEGREES_IN_CIRCLE = 360;

    private int[] validPlacementAngles;
    private int numberOfObstaclesToPlace;
    private List<ObstacleType> placeableObstacles;
    private ObstacleType objectToPlace;

    private List<int> anglesToRemove = new List<int>();


    void Start()
    {
        RandomizeRotation();
        GetValidPlacementAngles();
        DetermineNumberOfObstaclesToPlace();
        DeterminePlaceableObstacles();

        for(int i = 0; i < numberOfObstaclesToPlace; i++)
        {
            SelectObjectToPlace();
            if(CheckIfObstacleCanBePlaced())
            {
                Debug.Log("Object " + i + " can be placed");
                // Remove from valid angles

                // Place the object at the right position.
            }
            else
            {
                Debug.Log("Can't place object " + i);
            }
        }
    }

    /*
     * Returns true if the object can be placed. If true, the section to be removed is stored in anglesToRemove;
     */
    private bool CheckIfObstacleCanBePlaced()
    {
        bool canPlaceObject = true;
        int tries = 0;
        bool isFinished = false;
        int width = 0;

        // Get the width of the object to place
        switch (objectToPlace)
        {
            case ObstacleType.stoneWall:
                width = wallWidth;
                break;
            case ObstacleType.rocks:
                width = rocksWidth;
                break;
            case ObstacleType.dynamite:
                width = dynamiteWidth;
                break;
            default:
                Debug.Log("Obstacle type not handled in switch case.");
                break;

        }

        while(!isFinished && tries < maxPlacementTries)
        {
            canPlaceObject = true; // Assume it can be placed. Is set to false if it's not the case.
            int randomStartingIndex = UnityEngine.Random.Range(0, validPlacementAngles.Length - 1);
            int randomStartingAngle = validPlacementAngles[randomStartingIndex];
            Debug.Log("RandomStartingAngle: " + randomStartingAngle + " , index: " + randomStartingIndex);

            for (int i = 1; i < width; i++)
            {
                if(randomStartingIndex + i >= validPlacementAngles.Length) // Handles the transition from last angle in validPlacementAngles to 0. For example, 359 to 0.
                {
                    if(validPlacementAngles[randomStartingIndex + i - validPlacementAngles.Length] != randomStartingIndex + i - validPlacementAngles.Length)
                    {
                        canPlaceObject = false;
                        break;
                    }
                }
                else
                {
                    // Check if the object can fit.
                    if (validPlacementAngles[randomStartingIndex + i] != randomStartingAngle + i)
                    {
                        canPlaceObject = false;
                        break;
                    }
                }
            }

            // If we failed to place object, repeat
            if(!canPlaceObject)
            {
                tries++; // Repeats trying to fit the object at different starting angles until maxTries
            }
            else
            {
                isFinished = true; // Exit the loop
            }
            
        }
        return canPlaceObject;
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
