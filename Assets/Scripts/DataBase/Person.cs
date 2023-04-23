using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//infoEasy":0,"infoMedium":1,"infoHard":0,"bossLevel":true,"bossMax":0,"teacher"
public class Person 
{
    public long id;
    public string username;
    public string password;
    public int infoEasy;
    public int infoMedium;
    public int infoHard;
    public bool bossLevel;
    public int bossMax;
    public long teacher;
    public Person(string username, string password, int infoEase, int infoMedium, int infoHard, bool bossLevel, int bossMax, long teacher)
    {
        this.username = username;    
        this.password = password;
        this.infoEasy= infoEase;
        this.infoHard= infoHard ;
        this.infoMedium= infoMedium;
        this.bossMax= bossMax ;
        this.bossLevel = bossLevel;
        this.teacher = teacher;
    }
}
