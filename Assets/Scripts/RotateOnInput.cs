using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnInput : MonoBehaviour
{
    [SerializeField] [Tooltip("Degrees/s")] float rotationSpeed = 45f;

    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        float xAxisRaw = Input.GetAxisRaw("Horizontal");
        if(xAxisRaw == -1 && player.CanMoveLeft)
        {
            transform.Rotate(Vector3.up, xAxisRaw * rotationSpeed * Time.deltaTime);
            player.IsMovingRight = false;
        }
        else if (xAxisRaw == 1 && player.CanMoveRight)
        {
            transform.Rotate(Vector3.up, xAxisRaw * rotationSpeed * Time.deltaTime);
            player.IsMovingRight = true;
        }
    }
}
