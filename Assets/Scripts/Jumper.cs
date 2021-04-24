using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] float jumpSpeed = 2f;

    private bool canJump = false;
    private Rigidbody rigidBody;
    [SerializeField] private bool isGrounded = false;

    float distanceToGround;

   void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Jump();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Vector3 jumpVelocityToAdd = new Vector3(0f, jumpSpeed, 0f);
            rigidBody.velocity += jumpVelocityToAdd;
        }
    }
}
