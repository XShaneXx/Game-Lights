using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HowToPlay_Timer : MonoBehaviour
{
    public HowToPlay_Exit TimerStat;
    
    public float totalTime = 5f; 
    private float currentTime; 
    public TMP_Text timerText;
    private bool isTimerRunning; 

    public Instructions timeout;
    

    void Start()
    {
        currentTime = totalTime;
        UpdateTimerText();
        isTimerRunning = TimerStat.IsTimerStarted();
    }


    void Update()
    {
        isTimerRunning = TimerStat.IsTimerStarted();

        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;

            // Check if the timer has reached zero or below
            if (currentTime <= 0f)
            {
                currentTime = 0f;
                isTimerRunning = false; // Stop the timer when it reaches zero
            }

            // Updated Instructions
            if (!isTimerRunning) timeout.TimeRunsOut();

            // Update the timer text in the UI
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        int seconds = Mathf.FloorToInt(currentTime);
        int milliseconds = Mathf.FloorToInt((currentTime * 1000) % 1000);

        string timeText = string.Format("{0:00}.{1:000}", seconds, milliseconds);
        timerText.text = timeText;
    }
}
