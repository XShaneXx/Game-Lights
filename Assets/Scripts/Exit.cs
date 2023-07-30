using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public PlayerMovement Player;

    private void Start()
    {
        // Find and store a reference to the PlayerMovement script
        Player = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        // Check if the player has collided with the exit and the light has been picked up
        if (collision.CompareTag("Player") && Player.PickUpLightCondition())
        {
            // Load the next scene in the build order
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
