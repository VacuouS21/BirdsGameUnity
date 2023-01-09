using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuScript : MonoBehaviour
{
    private GameObject Levels;
    public Text welcomeName;
    public Text lightLevel;
    public Text mediumLevel;
    public Text hardlLevel;
    public Text bossLevel;
    public Text bossMark;
     void Start()
    {
        welcomeName.text = "Добро пожаловать, "+GlobalParametrs.userStorage.Name;
        lightLevel.text = "Лёгкий: "+GlobalParametrs.userStorage.Easy.ToString();
        mediumLevel.text = "Средний: " + GlobalParametrs.userStorage.Medium.ToString();
        hardlLevel.text = "Тяжёлый: " + GlobalParametrs.userStorage.Hard.ToString();
        bossLevel.text = "Контрольная: " + GlobalParametrs.userStorage.BossMax.ToString();
        bossMark.text = "В разработке";
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitGames()
    {
        Debug.Log("Игра закрылась");
        Application.Quit();
    }

    public void ViewInfo(GameObject gameObject)
    {
        Levels = gameObject;
        Levels.SetActive(!Levels.activeSelf);

    }
}
