using System;
using System.IO;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;


public class BaseScript
{
    public static string connectionString = "URI=file:" + Application.dataPath + "/DB/UserInfo.db";

    public static void startDB()
    {
        BaseScript.createTable();
    }
    public static bool checkMyUser(string name, string password)
    {
        

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sql = String.Format("SELECT * FROM UserInfo WHERE name=\"{0}\" AND password=\"{1}\"", name,password);
                dbCmd.CommandText = sql;                
                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string mark=reader.GetString(8);
                        bool check=false;
                        if (mark == "Пройден") check = true;
                        else check = false;
                        Person person = new Person();
                        person.username=reader.GetString(1);
                        person.password=reader.GetString(2);
                        person.infoEasy = reader.GetInt32(3);
                        person.infoMedium= reader.GetInt32(4);
                        person.infoHard = reader.GetInt32(5);
                        person.bossMax = reader.GetInt32(7);
                        person.bossLevel = check;
                        person.teacher = reader.GetInt32(9);
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
    public static void createUser(Person person)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sql = String.Format("SELECT * FROM UserInfo WHERE name=\"{0}\" ", person.username);

                dbCmd.CommandText = sql;
                string mark;

                if (person.bossLevel) mark = "Пройден";
                else mark = "Не пройден";

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        reader.Close();

                        string name = person.username;
                        int easy = person.infoEasy;
                        int medium = person.infoMedium;
                        int hard = person.infoHard;
                        int  bossMax = person.bossMax;


                        dbConnection.Close();
                        

                        updateUser(easy, medium, hard, bossMax, mark, name);
                        return;
                    }
                    else
                    {
                        reader.Close();
                        dbConnection.Close();

                        string sqlNew = String.Format("INSERT INTO UserInfo(name,password,light,medium,hight,bossLevel,mark,teacher) VALUES(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\")",
                        person.username, person.password, person.infoEasy, person.infoMedium, person.infoHard, person.bossMax, mark, person.teacher);
                        dbCmd.CommandText = sqlNew;
                        Debug.Log(sqlNew);
                       

                        dbCmd.ExecuteScalar();
                       
                    }

                }              
            }

        }
    }

    public static void executeSQL(string sqlString)
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
    public static void newDataBase()
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
"teacher INTEGER NOT NULL" +
")";
        executeSQL(sql);
    }

    public static void createTable()
    {
        string path = Application.dataPath + "/DB/UserInfo.db";
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

    public static void updateUser(int first, int second, int third, int four, string last, string myName)
    {
        string sql = String.Format("UPDATE UserInfo SET light =\"{0}\",medium=\"{1}\",hight=\"{2}\", bossLevel=\"{3}\",mark=\"{4}\" WHERE name=\"{5}\";", first, second, third, four, last, myName);
        executeSQL(sql);
    }
}
