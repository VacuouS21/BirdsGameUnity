using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerCode : MonoBehaviour
{
    // Start is called before the first frame update
    public int score=0;
    public int health;
    public Text scoreDisplay;
    public Text healthDisplay;
    void Update()
    {
        scoreDisplay.text = score.ToString();
        healthDisplay.text = health.ToString();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("True"))
        {
            score++;
        }
        if (other.CompareTag("False"))
        {
            health--;
        }
    }
    }
