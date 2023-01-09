using System;
using System.IO;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;
using System.Data.Common;

public class UserStorage
{
    private int id;
    private string name;
    private string password;
    private int easy;
    private int medium;
    private int hard;
    private int bossReady;
    private string bossMax;
    private int teacher;

    private string connectionString;
    private static string DBPath;
    //private static SqliteConnection connection;
    // private static SqliteCommand command;
    private string fileName;
    private bool checkInternet = false;
   
    private void createUser(string myName, string myPassword)
    {

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sql = String.Format("SELECT * FROM UserInfo WHERE name=\"{0}\" AND password=\"{1}\"", myName, myPassword);

                dbCmd.CommandText = sql;
                bool checkName = false;
                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Debug.Log(reader.GetValue(1));
                        Debug.Log(reader.GetValue(2));
                        // Взять всю инфу.
                        easy = reader.GetInt32(3);
                        medium = reader.GetInt32(4);
                        hard = reader.GetInt32(5);
                        bossReady = reader.GetInt32(6);
                        bossMax = reader.GetString(7);
                        teacher = reader.GetInt32(8);
                        Debug.Log(name + " " + password);


                        dbConnection.Close();
                        reader.Close();
                        return;
                    }
                    else
                    {
                        checkName = true;
                    }

                }
                if (checkName)
                {
                    string sqlNew = String.Format("INSERT INTO UserInfo(name,password,light,medium,hight,bossLevel,mark,teacher) VALUES(\"{0}\",\"{1}\",0,0,0,0,'Не пройден',0)", myName, myPassword);
                    dbCmd.CommandText = sqlNew;
                    Debug.Log(sqlNew);
                    dbCmd.ExecuteScalar();
                    dbConnection.Close();

                }
            }

        }

    }

    public UserStorage(string name, string password)
    {
        this.password = password;
        this.name = name;                               
        connectionString = "URI=file:" + Application.dataPath + "/DB/UserInfo.db";
        createTable();
        createUser(name, password);
        // createUser(name);
    }
    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string Password { get => password; set => password = value; }
    public int Easy { get => easy; set => easy = value; }
    public int Medium { get => medium; set => medium = value; }
    public int Hard { get => hard; set => hard = value; }
    public int BossReady { get => bossReady; set => bossReady = value; }
    public string BossMax { get => bossMax; set => bossMax = value; }
    public int Teacher{ get => teacher; set => teacher= value; }


    private EnumLogin checkUser(string name, string password)
    {
       
        if (autentificationUser(name))
        {
            if(autorizationUser(name, password))
            {
                return EnumLogin.TRUEPERSON;
            }
            else
            {
                return EnumLogin.WRONGPASSWORD;
            }

        }
        else
        {

            return EnumLogin.NOTFOUND;
        }

    }
    private bool autorizationUser(string name, string password)
    {
        return true;
    }
    private bool autentificationUser(string nameUser)
    {

        return true;
    }


    private void createTable()
    {
        string path = Application.dataPath + "/DB/UserInfo.db";
        this.fileName = path;
        FileInfo fileInfo = new FileInfo(path);
        if (fileInfo.Exists)
        {
            Console.WriteLine($"Имя файла: {fileInfo.Name}");
            Console.WriteLine($"Время создания: {fileInfo.CreationTime}");
            Console.WriteLine($"Размер: {fileInfo.Length}");
            newDataBase();
        }
        else
        {
            fileInfo.Create();
            newDataBase();
        }
    }

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
"mark TEXT NOT NULL," +
"teacher INTEGER NOT NULL"+
")";
        executeSQL(sql);
       // string sqlNew = String.Format("INSERT INTO UserInfo(name,password,light,medium,hight,bossLevel,mark,teacher) VALUES(\"{0}\",\"{1}\",0,0,0,0,'Не пройден',0)", "kostyn67", "123123");
        //executeSQL(sqlNew);
    }
    public void executeSQL(string sqlString)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
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
    public void updateUser(int first, int second, int third, int four, string last, string myName)
    {
        string sql = String.Format("UPDATE UserInfo SET light =\"{0}\",medium=\"{1}\",hight=\"{2}\", bossLevel=\"{3}\",mark=\"{4}\" WHERE name=\"{5}\";", first, second, third, four, last, myName);
        executeSQL(sql);
    }
}