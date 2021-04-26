using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    [SerializeField] private GameObject explosionParticles = null;

    [SerializeField] private AudioClip explosionSFX = null;

    private bool hasExploded = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(explosionParticles == null || explosionSFX == null || hasExploded) { return; }
            hasExploded = true;
            GameObject explosion = Instantiate(explosionParticles, transform.position, Quaternion.identity, transform);
            GetComponent<AudioSource>().PlayOneShot(explosionSFX);
        }
    }
}
