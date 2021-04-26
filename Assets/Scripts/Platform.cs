using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private enum PlatformType
    {
        oneSegment,
        twoSegments
    }

    private enum ObstacleType
    {
        stoneWall,
        rocks,
        dynamite,
        diamond,
        sapphire,
        ruby,
        emerald,
        topaz,
    }

    // Prefabs
    [SerializeField] private GameObject stoneWallPrefab;
    [SerializeField] private GameObject rocksPrefab;
    [SerializeField] private GameObject dynamitePrefab;

    [SerializeField] private GameObject diamondPrefab;
    [SerializeField] private GameObject sapphirePrefab;
    [SerializeField] private GameObject rubyPrefab;
    [SerializeField] private GameObject emeraldPrefab;
    [SerializeField] private GameObject topazPrefab;

    [SerializeField] [Tooltip("In degrees")] int holeSize = 70;

    [SerializeField] private PlatformType platformType = PlatformType.oneSegment;

    [SerializeField] private int maxObstacles = 3;
    [SerializeField] private int minObstacles = 0;

    [SerializeField] private int wallWidth = 30;
    [SerializeField] private int rocksWidth = 30;
    [SerializeField] private int dynamiteWidth = 30;

    [SerializeField] private int diamondWidth = 10;
    [SerializeField] private int sapphireWidth = 10;
    [SerializeField] private int rubyWidth = 10;
    [SerializeField] private int emeraldWidth = 10;
    [SerializeField] private int topazWidth = 10;

    [SerializeField] private int maxPlacementTries = 100;

    private float platformRandomAngle = 0;
    private List<int> validPlacementAngles;
    private int numberOfObstaclesToPlace;
    private List<ObstacleType> placeableObstacles;
    private ObstacleType objectToPlace;

    private GameObject objectPrefab;
    private int objectWidth = 0;


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
                SpawnObject();
                RemoveValidAngles();
            }
        }
    }

    private void SpawnObject()
    {
        int rotationAngle = anglesToRemove[0];
        GameObject objectGO = Instantiate(objectPrefab, transform.position, Quaternion.identity, transform);
        objectGO.transform.Rotate(Vector3.up, rotationAngle + platformRandomAngle);
    }

    private void RemoveValidAngles()
    {
        foreach(int angle in anglesToRemove)
        {
            bool isRemoved = validPlacementAngles.Remove(angle);
            if(!isRemoved)
            {
                Debug.Log("Couldn't remove angle from validPlacementAngles.");
            }
        }
    }

    /*
     * Returns true if the object can be placed. The section to be removed is stored in anglesToRemove;
     */
    private bool CheckIfObstacleCanBePlaced()
    {
        bool canPlaceObject = true;
        int tries = 0;
        bool isFinished = false;
        List<int> possibleAnglesToRemove = new List<int>();

        while(!isFinished && tries < maxPlacementTries)
        {
            canPlaceObject = true; // Assume it can be placed. Is set to false if it's not the case.
            possibleAnglesToRemove = new List<int>();
            int randomStartingIndex = UnityEngine.Random.Range(0, validPlacementAngles.Count - 1);
            int randomStartingAngle = validPlacementAngles[randomStartingIndex];
            possibleAnglesToRemove.Add(randomStartingAngle);

            for (int i = 1; i < objectWidth; i++)
            {
                if(randomStartingIndex + i >= validPlacementAngles.Count) // Handles the transition from last angle in validPlacementAngles to 0. For example, 359 to 0.
                {
                    if(validPlacementAngles[randomStartingIndex + i - validPlacementAngles.Count] != randomStartingIndex + i - validPlacementAngles.Count)
                    {
                        canPlaceObject = false;
                        break;
                    }
                    possibleAnglesToRemove.Add(randomStartingIndex + i - validPlacementAngles.Count);
                }
                else
                {
                    // Check if the object can fit.
                    if (validPlacementAngles[randomStartingIndex + i] != randomStartingAngle + i)
                    {
                        canPlaceObject = false;
                        break;
                    }
                    possibleAnglesToRemove.Add(randomStartingAngle + i);
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
        anglesToRemove = possibleAnglesToRemove;
        return canPlaceObject;
    }

    private void RandomizeRotation()
    {
        platformRandomAngle = UnityEngine.Random.Range(0, 359);
        transform.Rotate(Vector3.up, platformRandomAngle);
    }

    private void GetValidPlacementAngles()
    {
        switch(platformType)
        {
            case PlatformType.oneSegment:
                validPlacementAngles = new List<int>();
                for (int i = 0; i < 280; i++)
                {
                    validPlacementAngles.Add(i);
                }
                for(int i = 280; i < 290; i++)
                {
                    validPlacementAngles.Add(i + holeSize);
                }
                break;
            case PlatformType.twoSegments:
                validPlacementAngles = new List<int>();
                for (int i = 0; i < 105; i++)
                {
                    validPlacementAngles.Add(i);
                }
                for(int i = 105; i < 210; i++)
                {
                    validPlacementAngles.Add(i + holeSize);
                }
                for (int i = 210; i < 220; i++)
                {
                    validPlacementAngles.Add(i + 2*holeSize);
                }
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

        // Set 
        switch (objectToPlace)
        {
            case ObstacleType.stoneWall:
                objectPrefab = stoneWallPrefab;
                objectWidth = wallWidth;
                break;
            case ObstacleType.rocks:
                objectPrefab = rocksPrefab;
                objectWidth = rocksWidth;
                break;
            case ObstacleType.dynamite:
                objectPrefab = dynamitePrefab;
                objectWidth = dynamiteWidth;
                break;
            case ObstacleType.diamond:
                objectPrefab = diamondPrefab;
                objectWidth = diamondWidth;
                break;
            case ObstacleType.sapphire:
                objectPrefab = sapphirePrefab;
                objectWidth = sapphireWidth;
                break;
            case ObstacleType.ruby:
                objectPrefab = rubyPrefab;
                objectWidth = rubyWidth;
                break;
            case ObstacleType.emerald:
                objectPrefab = emeraldPrefab;
                objectWidth = emeraldWidth;
                break;
            case ObstacleType.topaz:
                objectPrefab = topazPrefab;
                objectWidth = topazWidth;
                break;
            default:
                Debug.Log("Obstacle type not handled in switch case.");
                break;

        }

    }
}
