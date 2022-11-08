using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearController : MonoBehaviour
{
    public int damage = 1;
    public float speed;
    private void Update()
    {

        transform.Translate(Vector2.left * speed);


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.CompareTag("False"))
        {
            collision.GetComponent<PlayerController>().health -= damage;
            SpawnerControler.deleteObstacle();
        }
       else if(collision.CompareTag("Player") && gameObject.CompareTag("True"))
        {
            SpawnerControler.deleteObstacle();
        }
      
    }
}
