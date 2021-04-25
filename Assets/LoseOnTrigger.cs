using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseOnTrigger : MonoBehaviour
{
    private Player player;
    private bool isTriggered = false;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isTriggered) { return; }
            isTriggered = true;

            player.Lose();
        }
    }
}
