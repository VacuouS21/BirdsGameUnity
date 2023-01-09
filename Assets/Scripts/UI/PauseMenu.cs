using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused=false;
    public GameObject pauseMenuUI;
    // Start is called before the first frame update
    private GearController[] startNow;
    private float mySpeed;

  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
     
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        startNow = FindObjectsOfType<GearController>();
        if (startNow.Length!=0)
        {
            mySpeed = startNow[0].speed;
        }

        foreach (var sn in startNow)
            sn.speed=0;
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        startNow = FindObjectsOfType<GearController>();
        foreach (var sn in startNow)
            sn.speed = mySpeed;
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
