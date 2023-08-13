using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public PlayerMovement Player;
    public bool canExit;
    public Vector3 DoorPosition;
    public bool positionChanged = false;
    public Transform DoorCurrentPosition;

    public bool TimerStart = false;

    public GameObject spotlightObject; // GameObject with the spotlight shader material

    private Renderer spotlightRenderer;

    private void Start()
    {
        // Find and store a reference to the PlayerMovement script
        Player = FindObjectOfType<PlayerMovement>();

        DoorPosition = Player.DoorPosition();

        if (spotlightObject)
            spotlightRenderer = spotlightObject.GetComponent<Renderer>();
        
        UpdateSpotlightPosition();
    }

    private void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (canExit && !positionChanged)
            {
                DoorCurrentPosition.position = DoorPosition;
                positionChanged = true;
                TimerStart = true;

            } else if (canExit && positionChanged)
            {
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
        }

        UpdateSpotlightPosition();
    }

    private void UpdateSpotlightPosition()
    {
        if (spotlightRenderer)
        {
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
            spotlightRenderer.sharedMaterial.SetVector("_SpotlightCenter2", new Vector4(viewportPos.x, viewportPos.y, 0, 0));
        }
    }

    public bool IsTimerStarted()
    {
        return TimerStart;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Player.PickUpLightCondition())
        {
            canExit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player") && Player.PickUpLightCondition())
        {
            canExit = false;
        }
    }
}
