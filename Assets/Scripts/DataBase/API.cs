
using Newtonsoft.Json;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class API: BaseScript
{

    private const string URL_BASE= "http://89.208.198.228:8080/";
    private const string URL_USER= "http://89.208.198.228:8080/user/";
    private const string URL_TEACHER= "http://89.208.198.228:8080/teacher/";

    public EnumLogin errorMessage;
    public Person person { get; private set; }

    public IEnumerator GetData_Coroutine(string name, string password)
    {
        string url = URL_USER+"name/"+ name;

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError)
            {
              errorMessage=EnumLogin.ERROR_CONNECTION;

            }
            else if (request.isHttpError)
            {
                errorMessage = EnumLogin.NOT_FOUND;
            }
            else
            {
                errorMessage = EnumLogin.TRUE_PERSON;
                person = JsonUtility.FromJson<Person>(request.downloadHandler.text);
            }
        }
    }
    IEnumerator PutData_Coroutine(Person person)
    {
        string url = URL_USER + name;
        using (UnityWebRequest request = UnityWebRequest.Put(url, JsonUtility.ToJson(person)))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError)
            {


            }
            else if (request.isHttpError)
            {
            }

            else
            {
                string json = request.downloadHandler.text;
                person = JsonUtility.FromJson<Person>(json);
            }
        }
    }

    //Проверка пароля.
    private bool autorizationUser(string inputPassword)
    {
        if (inputPassword == person.password) return true;
        else return false;
    }
    //Проверка имени
    private bool autentificationUser(string inputName)
    {
        if (inputName == person.username) return true;
        else return false;
    }

    //Правильность ввода, сравнение с данными сервера.
    public void checkUser(string name,string password)
    {
        if (autentificationUser(name))
        {

            if (autorizationUser(password))
            {
                errorMessage = EnumLogin.TRUE_PERSON;
                //Создание или обновление данных пользователя в локальной БД.
                createUser(this.person);
                GlobalParametrs.userStorage = new UserStorage(this.person);                
            }
            else
            {
                errorMessage = EnumLogin.WRONG_PASSWORD;
            }

        }
        else
        {
            errorMessage = EnumLogin.NOT_FOUND;
        }

    }
}
