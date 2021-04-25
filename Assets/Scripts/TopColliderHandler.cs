using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopColliderHandler : MonoBehaviour
{
    public PlayerAndPillarSpawner PillarSpawner { get; set; }
    private bool isTriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (isTriggered) { return; }
            isTriggered = true;

            PillarSpawner.SpawnNextPillar();
        }
    }
}
