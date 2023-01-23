using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Vector2 targetPos;
    public float Yincrement;
    public float Xincrement;
    public float speed;
    public float maxHeight;
    public float minHeight;
    public float minX;
    public float maxX;
    public int health = 5;
    public Text healthDisplay;
    public Text scoreDisplay;
    public float myTime = 1.5f;

    private float time = 0f;
    const float constTime = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
       // PlayerRB = GetComponent<Rigidbody>();

        targetPos = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (time > 0f)
        {
            // Subtract the difference of the last time the method has been called
            time -= Time.deltaTime;
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);


        if(Input.GetKeyDown(KeyCode.W) && transform.position.y < maxHeight && time <= 0)
        {
            time = constTime;
            targetPos =new Vector2(transform.position.x,transform.position.y+Yincrement );

        }
        else if (Input.GetKeyDown(KeyCode.S) && transform.position.y >minHeight && time <= 0)
        {

            time = constTime;
            targetPos = new Vector2(transform.position.x, transform.position.y - Yincrement);
        }
        else if (Input.GetKeyDown(KeyCode.D) && transform.position.x < maxX && time <= 0)
        {

            time = constTime;
            targetPos = new Vector2(transform.position.x + Xincrement, transform.position.y);
        }
        else if (Input.GetKeyDown(KeyCode.A) && transform.position.x > minX && time <= 0)
        {

            time = constTime;
            targetPos = new Vector2(transform.position.x - Xincrement, transform.position.y);
        }
    }


    public void UpButton()
    {

        if (transform.position.y < maxHeight && time <= 0)
        {
            time = 0.15f;
            targetPos = new Vector2(transform.position.x, transform.position.y + Yincrement);
        }

    }
    public void DownButton()
    {

        if (transform.position.y > minHeight && time <= 0)
        {
            time = 0.15f;
            targetPos = new Vector2(transform.position.x, transform.position.y - Yincrement);
        }

    }
    public void RightButton()
    {

        if (transform.position.x < maxX && time <= 0)
        {
            time = 0.15f;
            targetPos = new Vector2(transform.position.x + Xincrement, transform.position.y);
        }

    }
    public void LeftButton()
    {
        if (transform.position.x > minX && time <= 0)
        {
            time = 0.15f;
            targetPos = new Vector2(transform.position.x - Xincrement, transform.position.y);
        }

    }

}
        
