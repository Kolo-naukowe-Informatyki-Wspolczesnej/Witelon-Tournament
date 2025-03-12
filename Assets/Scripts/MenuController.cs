using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused = false;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void CompleteReset()
    {
        CommandExecutor exec = FindObjectOfType<CommandExecutor>();
        Timer timer = FindObjectOfType<Timer>();
        timer.ResetTimer();
        exec.ResetLevel();
        exec.winPanel.SetActive(false);
        Resume();
    }
}
