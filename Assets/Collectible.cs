using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private AudioClip collectSound = null;

    [SerializeField] private Gem gemType;
    private bool isCollected = false;

    public Gem GetGemType()
    {
        return gemType;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            GetComponent<AudioSource>().PlayOneShot(collectSound);
            GetComponentInChildren<MeshRenderer>().enabled = false;
        }
    }
}
