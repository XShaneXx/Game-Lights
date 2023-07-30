using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public bool LightPickedUp = false;
    public PlayerMovement Player;

    private void Start()
    {
        // Find and store a reference to the PlayerMovement script
        Player = FindObjectOfType<PlayerMovement>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !LightPickedUp)
        {
            LightPickedUp = true;
            Player.LightsPickedUp();
            Destroy(gameObject);
        }
    }
}
