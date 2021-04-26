using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnInput : MonoBehaviour
{
    [SerializeField] [Tooltip("Degrees/s")] float rotationSpeed = 45f;
    [SerializeField] float speedupFactor = 1f;

    [SerializeField] private float currentRotationSpeed;
    [SerializeField] private float maxRotationSpeed = 150f;

    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        currentRotationSpeed = rotationSpeed;
    }

    void Update()
    {
        float xAxisRaw = Input.GetAxisRaw("Horizontal");
        if(xAxisRaw == -1 && player.CanMoveLeft)
        {
            transform.Rotate(Vector3.up, xAxisRaw * currentRotationSpeed * Time.deltaTime);
            player.IsMovingRight = false;
            SpeedUp();
        }
        else if (xAxisRaw == 1 && player.CanMoveRight)
        {
            transform.Rotate(Vector3.up, xAxisRaw * currentRotationSpeed * Time.deltaTime);
            player.IsMovingRight = true;
            SpeedUp();
        }
        else
        {
            currentRotationSpeed = rotationSpeed;
        }
    }

    private void SpeedUp()
    {
        if(currentRotationSpeed <= maxRotationSpeed)
        {
            currentRotationSpeed += speedupFactor * Time.deltaTime;
        }
    }
}
