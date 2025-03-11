using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;



public class Timer : MonoBehaviour
{

    private bool timerActive = false;
    private float currentTime = 0.0f;

    [SerializeField] private TMP_Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(timerActive)
        {
            currentTime = currentTime + Time.deltaTime;
        }

        TimeSpan time = TimeSpan.FromSeconds(currentTime);

        int minutes = (int)time.Minutes;
        int seconds = (int)time.Seconds;
        int milli = (int)time.Milliseconds;

        text.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milli);
        
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }
}
