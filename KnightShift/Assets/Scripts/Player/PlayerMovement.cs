using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;

    private Rigidbody2D rigidBody;
    private bool isGrounded;

    private float moveInput;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public LayerMask platformLayer;

    private Animator animator;
    private PlayerAnimation playerAnimation;
    private bool isRunning = false;

    private PlayerAudio playerAudio;

    public bool lookingRight = true;

    private PlayerHealth playerHealth;

    private bool isJumping = false;
    public float jumpingTime = 0.5f;
    public float FixOnGroundRecoveryTime = 0.2f;

    [Header("FixOnGround Settings")]
    public float groundDistanceThreshold = 0.3f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerAudio = GetComponent<PlayerAudio>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void HandleMovement()
    {
        if (playerHealth.isAlive)
        {
            HorizontalMove();
            HandleRotation(moveInput);
            PerformJump();
            StepDownPlatform();
        }
        IsOnGround();
        FixOnGround();
    }
    private void HandleRotation(float moveInput)
    {
        if (moveInput > 0)
        {
            lookingRight = true;
        }
        else if (moveInput < 0)
        {
            lookingRight = false;
        }
        SwitchRotation();
    }
    private void SwitchRotation()
    {
        if (lookingRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void PerformJump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            Debug.Log("W pressed");
            SetIsJumping();
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            playerAnimation.TriggerJump();
            //playerAudio.PlayBoingJump();
            SoundManager.Instance.PlaySFX(SFXType.Jump);
            Invoke("SetIsNotJumping", FixOnGroundRecoveryTime);
            StartCoroutine(DisablePlatformCollision());
        }
    }

    private void HorizontalMove()
    {
        moveInput = Input.GetAxis("Horizontal");
        if(moveInput != 0)
        {
            //Debug.Log($"moving horizontally");
        }
        rigidBody.linearVelocity = new Vector2(moveInput * moveSpeed, rigidBody.linearVelocity.y);
        isRunning = (moveInput != 0);
        playerAnimation.SetRunning(isRunning && isGrounded);
    }

    private void IsOnGround()
    {
        bool wasGrounded = isGrounded;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer)
            || Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, platformLayer);
        animator.SetBool("IsGrounded", isGrounded);


    }

    private void StepDownPlatform()
    {
        if (Input.GetKeyDown(KeyCode.S) && isGrounded)
        {
            isJumping = true;
            StartCoroutine(DisablePlatformCollision());
        }
    }

    private IEnumerator DisablePlatformCollision()
    {
        // ���� �÷��̾ ���� ���̾�� Platform ���̾� ���� �浹�� ��
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Platform"), true);
        //Debug.Log("�÷��� �浹 ��Ȱ��ȭ");

        yield return new WaitForSeconds(jumpingTime); // �ٽ� �浹 Ȱ��ȭ

        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Platform"), false);
        //Debug.Log("�÷��� �浹 ����");
    }

    // ���ο��� ��� �� �� ������� ���� �ִ� �޼ҵ�
    private void FixOnGround()
    {
        if (!isGrounded && !isJumping)
        {
            // ������� �Ÿ� ����
            RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistanceThreshold * 2f, groundLayer | platformLayer);

            if (hit.collider != null)
            {
                float distanceToGround = hit.distance;

                if (distanceToGround <= groundDistanceThreshold)
                {
                    // ����� �ſ� ������ �ٿ����� ����
                    rigidBody.AddForce(Vector2.down * 10000f);
                }
            }
        }
    }


    private void SetIsJumping()
    {
        isJumping = true;
    }
    private void SetIsNotJumping()
    {
        isJumping = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistanceThreshold);
    }

}
