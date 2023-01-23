using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChoose : MonoBehaviour
{
    public void Light()
    {
        SceneManager.LoadScene(4);
    }
    public void Medium()
    {
        SceneManager.LoadScene(5);
    }
    public void Hight()
    {
        SceneManager.LoadScene(6);
    }
    public void Boss()
    {
        SceneManager.LoadScene(3);
    }
    public void Back()
    {
        SceneManager.LoadScene(1);
    }
}
