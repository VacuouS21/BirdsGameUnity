using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterMenu : API
{
    // Start is called before the first frame update

    [SerializeField] private InputField inputName;
    [SerializeField] private InputField inputPassword;
    private string myName;
    private string myPassword;
    private UserStorage user;
    public GameObject error;
    public Text textError;




    public void Start()
    {
        //Создаём бащу
        startDB();
        //Активный input
        inputName.ActivateInputField();
    }
    //Кнопка войти
    public void goToTheMainMenu()
    {
        if (inputName.text == "" || inputPassword.text=="")
        {
            Debug.Log("Ввод пустых значений.");
        }
        else
        {
            
            myName = inputName.text;
            myPassword = inputPassword.text;
            
            GetData(myName,myPassword);

            if (errorMessage.Equals(EnumLogin.TRUE_PERSON))
            {
                checkUser(myName,myPassword);
            }
            switch (errorMessage)
            {
                //Подверждение юзера
                case EnumLogin.TRUE_PERSON:
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

                    break;
                    //Неправильное имя
                case EnumLogin.NOT_FOUND:
                    errorStatusWindow("Такого пользователя не существует");
                    break;
                    //Проблемы с интернет соединением
                case EnumLogin.ERROR_CONNECTION:
                    errorStatusWindow("Проблемы с соединением. Попробуйте одиночную игру");
                    break;

                case EnumLogin.WRONG_PASSWORD:
                    errorStatusWindow("Неверный пароль");
                    break;
            }
           

        }

    }
   
    //Одиночная игра.
    public void singlGoToMainMenu()
    {
        Person personSingl = new Person("Игрок", "123123", 0, 0, 0, false, 0, 0);
        UserStorage user = new UserStorage(personSingl);
        GlobalParametrs.userStorage = user;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void GetData(string name,string password) => StartCoroutine(GetData_Coroutine(name,password));
   

    private void errorStatusWindow(string message)
    {
        textError.text=message;
        error.SetActive(true);
    }
    public void buttonOkError()
    {
        error.SetActive(false);
    }
}
