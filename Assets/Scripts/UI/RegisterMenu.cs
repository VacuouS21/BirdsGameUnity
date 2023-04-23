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
        //������ ����
        startDB();
        //�������� input
        inputName.ActivateInputField();
    }
    //������ �����
    public void goToTheMainMenu()
    {
        if (inputName.text == "" || inputPassword.text=="")
        {
            Debug.Log("���� ������ ��������.");
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
                //������������ �����
                case EnumLogin.TRUE_PERSON:
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

                    break;
                    //������������ ���
                case EnumLogin.NOT_FOUND:
                    errorStatusWindow("������ ������������ �� ����������");
                    break;
                    //�������� � �������� �����������
                case EnumLogin.ERROR_CONNECTION:
                    errorStatusWindow("�������� � �����������. ���������� ��������� ����");
                    break;

                case EnumLogin.WRONG_PASSWORD:
                    errorStatusWindow("�������� ������");
                    break;
            }
           

        }

    }
   
    //��������� ����.
    public void singlGoToMainMenu()
    {
        Person personSingl = new Person("�����", "123123", 0, 0, 0, false, 0, 0);
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
