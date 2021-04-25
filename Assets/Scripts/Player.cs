using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool IsMovingRight { get; set; }
    public bool CanMoveRight { get; private set; }
    public bool CanMoveLeft { get; private set; }

    [SerializeField] float jumpSpeed = 2f;
    [SerializeField] private bool isGrounded = false;

    private bool canJump = true;
    private bool hasLost = false;
    private Rigidbody rigidBody;

    void Start()
    {
        IsMovingRight = false;
        CanMoveRight = true;
        CanMoveLeft = true;
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Jump();
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
            Lose();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
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

    public void Lose()
    {
        Debug.Log("You have lost");
        if(hasLost) { return; }
        hasLost = true;
        CanMoveRight = false;
        CanMoveLeft = false;
        canJump = false;
        FindObjectOfType<ScoreManager>().SaveHighScore();
        StartCoroutine(FindObjectOfType<LevelLoader>().WaitAndLoadNextScene());
    }

    private void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded && canJump)
        {
            Vector3 jumpVelocityToAdd = new Vector3(0f, jumpSpeed, 0f);
            rigidBody.velocity += jumpVelocityToAdd;
        }
    }
}
