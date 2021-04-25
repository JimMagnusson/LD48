using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void SetSpeedHandler();

public class SetSpeedOnTrigger : MonoBehaviour
{

    public event SetSpeedHandler SetSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (SetSpeed != null)
            {
                SetSpeed();
            }
        }
    }
}
