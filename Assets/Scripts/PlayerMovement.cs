using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this value to control player movement speed
    public float jumpForce = 10f; // Adjust this value to control player jump force
    public Transform groundCheck; // Create an empty GameObject as a child of the player and assign it to this variable
    public LayerMask groundLayer; // Set the ground layer in the Inspector to properly detect if the player is on the ground

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private float groundCheckRadius = 0.1f; // Adjust this value to control the size of the ground check sphere

    public bool LightPickedUp = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check if the player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Player movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        float moveVelocity = moveInput * moveSpeed;
        rb.velocity = new Vector2(moveVelocity, rb.velocity.y);

        // Player jump
        if (isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        // Draw the ground check circle in the Unity editor for visualization
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    public void LightsPickedUp()
    {
        LightPickedUp = true;
    }

    public bool PickUpLightCondition()
    {
        return LightPickedUp;
    }
}
