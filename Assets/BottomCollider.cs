using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomCollider : MonoBehaviour
{
    private bool isTriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if(isTriggered) { return; }
        isTriggered = true;

        FindObjectOfType<CameraMovement>().AddSegmentPointToPath();
    }
}
