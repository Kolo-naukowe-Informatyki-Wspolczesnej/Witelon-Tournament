using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;



public class Timer : MonoBehaviour
{

    private bool timerActive = false;
    private float currentTime = 0.0f;

    public TimeSpan time;

    [SerializeField] private TMP_Text inGameTimer;
    [SerializeField] private TMP_Text postGameTimer;
    


    // Update is called once per frame
    void Update()
    {

        if(timerActive)
        {
            currentTime = currentTime + Time.deltaTime;
        }

        time = TimeSpan.FromSeconds(currentTime);

        int minutes = (int)time.Minutes;
        int seconds = (int)time.Seconds;
        int milli = (int)time.Milliseconds;

        inGameTimer.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milli);
        
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;

        time = TimeSpan.FromSeconds(currentTime);

        int minutes = (int)time.Minutes;
        int seconds = (int)time.Seconds;
        int milli = (int)time.Milliseconds;

        postGameTimer.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milli);
    }

    public void ResetTimer()
    {
        currentTime = 0.0f;
    }
}
