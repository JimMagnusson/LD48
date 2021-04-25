using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAndPillarSpawner : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject pillarPrefab;
    [SerializeField] GameObject cylinderPrefab;


    [SerializeField] Vector3 playerStartPos;
    [SerializeField] int maxPillarCount = 4;
    [SerializeField] int numberOfStartingCylinders = 3;

    private GameObject newPillar = null;

    private Queue<GameObject> pillars;
    private float pillarYPos = 0;
    private int numberOfPillarsSpawned = 0;
    private const float PILLAR_LENGTH = 23.937f;

    void Start()
    {
        pillars = new Queue<GameObject>();
        SpawnStartingCylinders();
        SpawnNextPillar();
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Instantiate(playerPrefab, playerStartPos, Quaternion.identity);
    }

    private void SpawnStartingCylinders()
    {
        for(int i = numberOfStartingCylinders; i > 0; i--)
        {
            GameObject cylinder = Instantiate(cylinderPrefab, new Vector3(0, i * PILLAR_LENGTH, 0), Quaternion.identity,transform);
            pillars.Enqueue(cylinder);
        }
    }

    public void SpawnNextPillar()
    {
        pillarYPos = PILLAR_LENGTH * numberOfPillarsSpawned;

        newPillar = Instantiate(pillarPrefab, new Vector3(0, -pillarYPos,0), Quaternion.identity);
        newPillar.GetComponentInChildren<TopColliderHandler>().PillarSpawner = this;
        newPillar.GetComponentInChildren<BottomColliderHandler>().PillarSpawner = this;
        pillars.Enqueue(newPillar);
        numberOfPillarsSpawned++;

        if(pillars.Count > maxPillarCount)
        {
            GameObject pillarToRemove = pillars.Dequeue();
            Destroy(pillarToRemove);
        }
    }
}
