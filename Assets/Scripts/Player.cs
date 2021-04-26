using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool IsMovingRight { get; set; }
    public bool CanMoveRight { get; private set; }
    public bool CanMoveLeft { get; private set; }

    [SerializeField] float jumpSpeed = 2f;
    [SerializeField] float deathrattleSpeed = 1f;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private AudioClip jumpSound = null;
    [SerializeField] private AudioClip landSound = null;

    private bool canJump = true;
    private bool hasLost = false;
    private Rigidbody rigidBody;
    private AudioSource audioSource;
    private ScoreManager scoreManager;

    void Start()
    {
        IsMovingRight = false;
        CanMoveRight = true;
        CanMoveLeft = true;
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        scoreManager = FindObjectOfType<ScoreManager>();
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
        else if (other.gameObject.CompareTag("Collectible"))
        {
            Collectible collectible = other.gameObject.GetComponent<Collectible>();
            scoreManager.AddGemToScore(collectible.GetGemType());
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
            if(!isGrounded)
            {
                isGrounded = true;
                audioSource.PlayOneShot(landSound);
            }
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
        if(hasLost) { return; }
        hasLost = true;
        CanMoveRight = false;
        CanMoveLeft = false;
        canJump = false;
        Deathrattle();
        FindObjectOfType<ScoreManager>().SaveHighScore();

        FindObjectOfType<GameOverController>().ShowGameOverMenu();
    }

    private void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded && canJump)
        {
            Vector3 jumpVelocityToAdd = new Vector3(0f, jumpSpeed, 0f);
            rigidBody.velocity += jumpVelocityToAdd;
            audioSource.PlayOneShot(jumpSound);
        }
    }

    private void Deathrattle()
    {
        Vector3 jumpVelocityToAdd = new Vector3(0f, deathrattleSpeed, 0f);
        rigidBody.velocity = jumpVelocityToAdd;
    }
}
