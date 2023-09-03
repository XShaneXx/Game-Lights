using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Status")]
    public float moveSpeed = 5f; // Adjust this value to control player movement speed
    public float jumpForce = 10f; // Adjust this value to control player jump force

    [Header("Ground")]
    public Transform groundCheck; // Create an empty GameObject as a child of the player and assign it to this variable
    public LayerMask groundLayer; // Set the ground layer in the Inspector to properly detect if the player is on the ground
    private bool isGrounded = false;
    private float groundCheckRadius = 0.1f; // Adjust this value to control the size of the ground check sphere

    [Header("Player Object")]
    public Transform eye1Transform;
    public Vector3 initialSpawnPosition;
    private Rigidbody2D rb;

    [Header("Lights Object")]
    public bool LightPickedUp = false;

    [Header("Spotlight Effect")]
    public Material spotlightMaterial;
    public GameObject spotlightObject;
    public bool spotlightIsON = true;

    private bool GameIsPause = false;
    public GameObject pauseScene;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        eye1Transform = transform.Find("Eye1");
        initialSpawnPosition = transform.position;
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

        //Eye Position
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector3 newPosition = eye1Transform.localPosition;
            newPosition.x = 0.3f;
            eye1Transform.localPosition = newPosition;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector3 newPosition = eye1Transform.localPosition;
            newPosition.x = -0.3f;
            eye1Transform.localPosition = newPosition;
        }

        //Spotlight Position
        if (LightPickedUp)
        {
            Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);
            spotlightMaterial.SetVector("_SpotlightCenter", new Vector4(screenPos.x, screenPos.y, 0, 0));
        }

        //Spotlight test
        if (spotlightIsON && Input.GetKeyDown(KeyCode.O))
        {
            spotlightObject.SetActive(false);
            spotlightIsON = false;
        } else if (!spotlightIsON && Input.GetKeyDown(KeyCode.O))
        {
            spotlightObject.SetActive(true);
            spotlightIsON = true;
        }

        //Pause
        if (Input.GetKeyDown(KeyCode.Escape) && !GameIsPause)
        {
            pauseScene.SetActive(true);
            GameIsPause = true;
            Time.timeScale = 0f;
        } else if (Input.GetKeyDown(KeyCode.Escape) && GameIsPause)
        {
            pauseScene.SetActive(false);
            GameIsPause = false;
            Time.timeScale = 1f;
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

    public Vector3 DoorPosition()
    {
        return initialSpawnPosition;
    }

    public bool lightStat()
    {
        return LightPickedUp;
    }
}
