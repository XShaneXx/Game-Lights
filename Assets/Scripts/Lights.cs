using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public bool LightPickedUp = false;
    public PlayerMovement Player;
    
    public GameObject spotlightObject; // GameObject with the spotlight shader material
    private Renderer spotlightRenderer;

    private void Start()
    {
        Player = FindObjectOfType<PlayerMovement>();

        if (spotlightObject)
            spotlightRenderer = spotlightObject.GetComponent<Renderer>();
        
        UpdateSpotlightPosition();
    }

    private void UpdateSpotlightPosition()
    {
        if (spotlightRenderer)
        {
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
            spotlightRenderer.sharedMaterial.SetVector("_SpotlightCenter", new Vector4(viewportPos.x, viewportPos.y, 0, 0));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !LightPickedUp)
        {
            LightPickedUp = true;
            Player.LightsPickedUp();
            UpdateSpotlightPosition();
            Destroy(gameObject);
        }
    }
}

