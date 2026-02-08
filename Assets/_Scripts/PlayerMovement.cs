using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Gravity")]
    [SerializeField] float baseGravity = 2f;
    [SerializeField] float maxFallSpeed = 18f;
    [SerializeField] float fallSpeedMultiplier = 2f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Echo Wave")]
    public GameObject echoWavePrefab;
    public int maxEchoCharges = 3;
    public int currentEchoCharges;
    public TextMeshProUGUI echoCounterText;

    bool isGrounded;

    float move;

    Transform currentSpawnPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentEchoCharges = maxEchoCharges;
    }

    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");

        // Jump
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        Gravity();
        
        // Flip sprite
        if (move != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryUseEcho();
        }

        echoCounterText.text = $"X {currentEchoCharges:00}";
    }

    void FixedUpdate()
    {
        // Horizontal movement
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);
    }

    void Gravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier; // Faster Fall
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isJumping", false);
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isJumping", true);
        }
    }

    void TryUseEcho()
    {
        if (currentEchoCharges <= 0)
        {
            Debug.Log("No echo charges left!");
            return;
        }

        currentEchoCharges--;

        Instantiate(echoWavePrefab, transform.position, Quaternion.identity);

        Debug.Log("Echo used. Remaining: " + currentEchoCharges);
    }

    // 🔋 CALLED BY BATTERIES
    public void AddEchoCharge(int amount)
    {
        currentEchoCharges = Mathf.Clamp(
            currentEchoCharges + amount,
            0,
            maxEchoCharges
        );
    }

    // Optional upgrade
    public void IncreaseMaxEcho(int amount)
    {
        maxEchoCharges += amount;
        currentEchoCharges = maxEchoCharges;
    }

    public void SetSpawnPoint(Transform spawn)
    {
        currentSpawnPoint = spawn;
    }

    public void Respawn()
    {
        transform.position = currentSpawnPoint.position;
    }
}
