using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    [SerializeField] private GameObject explosionParticles = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(explosionParticles == null) { return; }
            GameObject explosion = Instantiate(explosionParticles, transform.position, Quaternion.identity, transform);
            // TODO: Trigger SFX
        }
    }
}
