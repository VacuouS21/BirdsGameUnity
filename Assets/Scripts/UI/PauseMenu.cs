using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused=false;
    public GameObject pauseMenuUI;
    public GameObject player;
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
        {
            sn.speed = 0;
            sn.gameObject.SetActive(false);
        }
        player.SetActive(false);  
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        //startNow = FindObjectsOfType<GearController>();
        foreach (var sn in startNow)
        {
            sn.speed = mySpeed;
            sn.gameObject.SetActive(true);
        }
        player.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
