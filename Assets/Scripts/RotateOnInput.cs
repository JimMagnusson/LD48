using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnInput : MonoBehaviour
{

    [SerializeField] [Tooltip("Degrees/s")] float rotationSpeed = 45f;

    void Update()
    {
        transform.Rotate(Vector3.up, Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime);
    }
}
