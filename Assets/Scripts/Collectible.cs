using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private AudioClip collectSound = null;

    [SerializeField] private Gem gemType;
    public bool IsCollected = false;

    private float shortDelay = 0.1f;

    public Gem GetGemType()
    {
        return gemType;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !IsCollected)
        {
            GetComponent<AudioSource>().PlayOneShot(collectSound);
            GetComponentInChildren<MeshRenderer>().enabled = false;
            StartCoroutine(SetCollectedAfterShortDelay());
        }
    }

    private IEnumerator SetCollectedAfterShortDelay()
    {
        yield return new WaitForSeconds(shortDelay);
        IsCollected = true;
    }
}
