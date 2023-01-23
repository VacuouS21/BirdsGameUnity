using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    public Text textTimer;
    public Text textRandomString;
    public InputField inputNumber;
    public Text buttonOk;
    public Text textMyBall;

    private bool clickButton = false;
    private bool checkAnswer = true;
    private int bossBall = 0;
    private int loseBall = 0;

    public float myTime = 20;
    public int answer;
    bool check = true;

    public GameObject menu;
    public GameObject pane;

    public Text textTrue;
    public Text textFalse;
    public Text textMark;
    // Start is called before the first frame update
    void Start()
    {
        textTimer.text = myTime.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        if (myTime <= 0)
        {

            if (check)
            {
                Time.timeScale = 0f;
                pane.SetActive(true);
                menu.SetActive(true);
                textTrue.text = textTrue.text + " " + bossBall.ToString();
                textFalse.text = textFalse.text + " " + loseBall.ToString();

                if (bossBall >= 50)
                {
                    if (bossBall / loseBall >= 80) textMark.text = textMark.text + " 5";
                    else if (bossBall / loseBall >= 70) textMark.text = textMark.text + " 4";
                    else if (bossBall / loseBall >= 50) textMark.text = textMark.text + " 3";
                    else textMark.text = textMark.text + " 2";

                }
                else textMark.text = textMark.text + " 2";
                check = false;
            }
        }

        if (Input.GetKey(KeyCode.Return))
        {
            buttonTimer();
        }
        if (clickButton)
        {
            myTime -= Time.deltaTime;
            textTimer.text = myTime.ToString("F2");

        }
        if (clickButton && checkAnswer)
        {
            checkAnswer = false;
            buttonOk.text = "Ок";
            int firstNumber = Random.Range(2, 10);
            int secondNumber = Random.Range(2, 10);

            answer = firstNumber * secondNumber;

            textRandomString.text = firstNumber + " * " + secondNumber;


        }
    }
    public void buttonTimer()
    {

        if (!clickButton)
        {
            buttonOk.text = "Начать";
            clickButton = true;
        }
        else
        {
            if (inputNumber.text == "")
            {
                Debug.Log("Ошибка");

            }
            else
            {
                //input.text;
                Debug.Log("Успешно");
                if (int.Parse(inputNumber.text) == answer)
                {
                    checkAnswer = true;
                    bossBall += 1;
                    textMyBall.text = bossBall.ToString();
                }
                else
                {
                    loseBall++;
                }
            }
        }
        inputNumber.text = "";
        inputNumber.ActivateInputField();
    }
    public void inMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
   
}
