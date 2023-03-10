using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public EndOfDayEvent EodEvent;
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    public UnityEvent<float> OnTick;
    float cachedTime = 100;

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        cachedTime = timeRemaining;
    }
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
                OnTick.Invoke(timeRemaining/cachedTime);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                EodEvent.DayEnded = true;
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
