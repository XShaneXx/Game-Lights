using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{
    public TMP_Text instructions;
    private bool leftclicked = false;
    private bool rightclicked = false;
    private bool spaceclicked = false;

    public PlayerMovement lightpickedup;
    public HowToPlay_Exit zclicked;

    private bool timeisout = false;
    
    // Start is called before the first frame update
    void Start()
    {
        instructions.text = "Use Left & Right Arrow Keys to move";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) leftclicked = true;

        if (Input.GetKeyDown(KeyCode.RightArrow)) rightclicked = true;

        if (leftclicked & rightclicked) instructions.text = "Good! Now use Space or Up Arrow Key to Jump!!";

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) spaceclicked = true;

        if (spaceclicked) instructions.text = "Great! Now you have to collected the light!!";

        if (lightpickedup.lightStat()) instructions.text = "You got the light! Now explore the maze! \n When ready get to the door and press [Z]!!";

        if (zclicked.IsTimerStarted()) instructions.text = "<--- Quick!! Get to the door and press [Z] before the time runs out!!";

        if (timeisout) instructions.text = "Time runs out but is okay it just a tutorials! \n Exit the door and try again before you get to the real level!!";
    }

    public void TimeRunsOut()
    {
        timeisout = true;
    }
}
