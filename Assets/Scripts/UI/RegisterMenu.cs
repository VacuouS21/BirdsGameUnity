using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterMenu : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private InputField inputName;
    [SerializeField] private InputField inputPassword;
    private string myName;
    private string myPassword;
    public void Start()
    {
        inputName.ActivateInputField();
    }
    // Start is called before the first frame update
    public void goToTheMainMenu()
    {
        if (inputName.text == "" || inputPassword.text=="")
        {
            Debug.Log("Ошибка");
        }
        else
        {
            //input.text;

            Debug.Log("Успешно");
            myName = inputName.text;
            myPassword = inputPassword.text;
            UserStorage user = new UserStorage(myName,myPassword);
            GlobalParametrs.userStorage = user;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }

    }
   
    public void singlGoToMainMenu()
    {
        UserStorage user = new UserStorage("Ученик", "123");
        GlobalParametrs.userStorage = user;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
