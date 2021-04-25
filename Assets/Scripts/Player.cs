using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool IsMovingRight { get; set; }
    public bool CanMoveRight { get; private set; }
    public bool CanMoveLeft { get; private set; }

    void Start()
    {
        IsMovingRight = false;
        CanMoveRight = true;
        CanMoveLeft = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            if (IsMovingRight)
            {
                CanMoveRight = false;
            }
            else
            {
                CanMoveLeft = false;
            }
        }
        else if (other.gameObject.CompareTag("Deadly")) {
            Die();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            CanMoveRight = true;
            CanMoveLeft = true;
        }
    }

    private void Die()
    {
        CanMoveRight = false;
        CanMoveLeft = false;
        //FindObjectOfType<LevelLoader>().ShowRetryScreenAfterDelay();
    }
}
