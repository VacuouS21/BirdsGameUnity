using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterMenu : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private InputField inputName;
    [SerializeField] private InputField inputPassword;
    private string myName;
    private string myPassword;
    private UserStorage user;
    public Person person;
    public GameObject error;
    public Text textError;

    private bool autorizationUser(string inputPassword, string realPassword)
    {
        if (inputPassword == realPassword) return true;
        else return false;
    }
    private bool autentificationUser(string inputName, string realName)
    {
        if (inputName == realName) return true;
        else return false;
    }
    public void checkUser()
    {
        Debug.Log("Проверка пользователя" + this.person.username + " а введено" + inputName.text);
        if (autentificationUser(this.myName,this.person.username))
        {
           
            if (autorizationUser(this.myPassword,this.person.password))
            {
                Debug.Log("Авторизация");
                BaseScript.createUser(this.person);
                user = new UserStorage(this.person);
                GlobalParametrs.userStorage = user;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                errorStatusWindow("Неверный пароль");
            }

        }
        else
        {
            Debug.Log("ИДИ НАХУЙ"+this.person.username);
            //errorStatusWindow("Неверное имя");
        }

    }

    public void Start()
    {
        BaseScript.startDB();
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
            
            myName = inputName.text;
            myPassword = inputPassword.text;
            
            GetData(myName,myPassword);
            //Debug.Log("&&"+this.person.username);
            

        }

    }
   
    public void singlGoToMainMenu()
    {
        /*Person personSingl = personForSingl();
        UserStorage user = new UserStorage(personSingl);
        GlobalParametrs.userStorage = user;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);*/
    }

    private void GetData(string name,string password) => StartCoroutine(GetData_Coroutine(name,password));
    IEnumerator GetData_Coroutine(string name,string password)
    {
        string url = "http://89.208.222.66:8080/user/name/"+name;
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError)
            {
                if(BaseScript.checkMyUser(name, password))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else
                {
                    Debug.Log(request.error);
                    errorStatusWindow("Возникли проблемы с интернетом");
                }
                
            }
            else if (request.isHttpError)
            {
                errorStatusWindow("Неверное имя");
            }
            else
            {
                string json = request.downloadHandler.text;

                this.person = JsonUtility.FromJson<Person>(json);
                Debug.Log(this.person.username + " " + person.infoEasy + " " + person.infoMedium);
                checkUser();
            }
        }
    }

    private void errorStatusWindow(string message)
    {
        textError.text=message;
        error.SetActive(true);
    }
    public void buttonOkError()
    {
        error.SetActive(false);
    }
    private Person personForSingl()
    {
        Person personSingl = new Person();
        person.username = "Игрок";
        person.password = "123123";
        person.infoEasy = 0;
        person.infoMedium = 0;
        person.infoHard = 0;
        person.bossMax = 0;
        person.bossLevel= false;
        person.teacher = 0;
        return personSingl;
    }

}
