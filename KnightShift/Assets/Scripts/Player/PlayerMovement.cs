using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;

    private Rigidbody2D rigidBody;
    private bool isGrounded;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Animator animator;
    private PlayerAnimation playerAnimation;
    private bool isRunning = false;

    private PlayerAudio playerAudio;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerAudio = GetComponent<PlayerAudio>();
    }

    public void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rigidBody.linearVelocity = new Vector2(moveInput * moveSpeed, rigidBody.linearVelocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        //플레이어 회전
        HandleRotation(moveInput);

        //애니메이션을 적용받는 움직임들
        if (playerAnimation != null)
        {
            if (Input.GetKeyDown(KeyCode.W) && isGrounded)
            {
                rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                playerAnimation.TriggerJump();
                playerAudio.PlayBoingJump();
            }

            isRunning = (moveInput != 0);
            playerAnimation.SetRunning(isRunning && isGrounded);

        }

        animator.SetBool("IsGrounded", isGrounded);

    }
    private void HandleRotation(float moveInput)
    {
        if (moveInput >= 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

}
