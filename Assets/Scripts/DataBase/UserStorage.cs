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
    private bool bossReady;
    private int bossMax;
    private long teacher;
 
    public UserStorage(Person person)
    {
        name = person.username;
        password = person.password;
        easy = person.infoEasy;
        medium = person.infoMedium;
        hard = person.infoHard;
        bossReady = person.bossLevel;
        bossMax = person.bossMax;
        teacher = person.teacher;        
    }
    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public int Easy { get => easy; set => easy = value; }
    public int Medium { get => medium; set => medium = value; }
    public int Hard { get => hard; set => hard = value; }
    public bool BossReady { get => bossReady; set => bossReady = value; }
    public int BossMax { get => bossMax; set => bossMax = value; }

    /*    private void forNewUser()
        {
            createTable();
            connectionString = "URI=file:" + Application.dataPath + "/DB/UserInfo.db";
        }
    */


}