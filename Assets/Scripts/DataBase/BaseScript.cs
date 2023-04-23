using Mono.Data.Sqlite;
using System;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using UnityEngine;


public class BaseScript:MonoBehaviour
{
    public static string CONNECTION_STRING = "URI=file:" + Application.dataPath + "/DB/UserInfo.db";

    //������ ������ � ��
    public void startDB()
    {
        Debug.Log(CONNECTION_STRING);
        createTable();
    }


    //�������� �� ������������� ������ � ��������� ��
    public bool checkMyUser(string name, string password)
    {
        using (IDbConnection dbConnection = new SqliteConnection(CONNECTION_STRING))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                dbCmd.CommandText = String.Format("SELECT * FROM UserInfo WHERE name=\"{0}\" AND password=\"{1}\"", name, password);
                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Person person = new Person(reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetBoolean(6), reader.GetInt32(7), reader.GetInt32(9));
                        UserStorage user = new UserStorage(person);
                        GlobalParametrs.userStorage = user;
                        dbConnection.Close();
                        reader.Close();
                        return true;
                    }
                    else
                    {
                        dbConnection.Close();
                        reader.Close();
                        return false;
                    }

                }
            }

        }
    }
    //��� ���������� ������������ � ��������� ��, ������ ����� ������������� ������� ��� � �������� ��.
    public void createUser(Person person)
    {
        using (IDbConnection dbConnection = new SqliteConnection(CONNECTION_STRING))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                dbCmd.CommandText = String.Format("SELECT * FROM UserInfo WHERE name=\"{0}\" ", person.username);
                
                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    //���� � ��, ��������� � �������� ����
                    if (reader.Read())
                    {
                        Debug.Log("������������ " + person.username + " ���� � ��");
                        reader.Close();
                        dbConnection.Close();
                        updateUser(person.infoEasy, person.infoMedium, person.infoHard, person.bossMax, person.bossLevel, person.username);
                        return;
                    }
                    //��� � ��,���������.
                    else
                    {
                        Debug.Log("������������ " + person.username + " ���������� � ��");
                        reader.Close();
                        dbConnection.Close();
                        dbCmd.CommandText = String.Format("INSERT INTO UserInfo(name,password,light,medium,hight,bossLevel,mark,teacher) VALUES(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\")",
                        person.username, person.password, person.infoEasy, person.infoMedium, person.infoHard, person.bossMax, person.bossLevel, person.teacher);
                        dbCmd.ExecuteScalar();

                    }

                }
            }

        }
    }
    //����� ��� ���������� sql ��������.
    public void executeSQL(string sqlString)
    {
        using (IDbConnection dbConnection = new SqliteConnection(CONNECTION_STRING))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                dbCmd.CommandText = sqlString;
                Debug.Log(sqlString);
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }
    //������ ��� �������� ��
    public void newDataBase()
    {
        string sql = "CREATE TABLE IF NOT EXISTS UserInfo(" +
"id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
"name TEXT NOT NULL ," +
"password TEXT NOT NULL," +
"light INTEGER NOT NULL, " +
"medium INTEGER NOT NULL," +
"hight INTEGER NOT NULL, " +
"bossLevel INTEGER NOT NULL," +
"bossCheck BOOLEAN NOT NULL," +
"teacher INTEGER NOT NULL" +
")";
        executeSQL(sql);
    }

    //�������� �� ������� ����� � ������ ������� �������� ��
    public  void createTable()
    {
        string path = Application.dataPath + "/DB/UserInfo.db";
        FileInfo fileInfo = new FileInfo(path);
        if (fileInfo.Exists)
        {
            Console.WriteLine($"��� �����: {fileInfo.Name}");
            Console.WriteLine($"����� ��������: {fileInfo.CreationTime}");
            Console.WriteLine($"������: {fileInfo.Length}");
        }
        else
        {
            fileInfo.Create();
        }
        newDataBase();

    }
    //����� ��� ���������� ������������ � ��
    public  void updateUser(int light, int medium, int hight, int bossMax, bool bossCheck, string myName)
    {
        executeSQL(String.Format("UPDATE UserInfo SET light =\"{0}\",medium=\"{1}\",hight=\"{2}\", bossLevel=\"{3}\",bossCheck=\"{4}\" WHERE name=\"{5}\";", light, medium, hight, bossMax, bossCheck, myName));
    }
}
