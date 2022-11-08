using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesFactory
{
    private int minRand, maxRand;
    Strategy stratagy = new Strategy();
    public EnemiesFactory(int minRand,int maxRand)
    {

    }
    //set point from Spawner and Type
    public GameObject createObstacle(ObType obType, GameObject point, GameObject modelObstacle,int number)
    {

        GameObject ob = null;
        //Enemies or not
        switch (obType)
        {
            //set point! for Strategy
            case ObType.FALSE:
                stratagy.setObstacle(new FalseObstacle(),point,modelObstacle);
                ob=stratagy.spawner();
                break;

            case ObType.TRUE:
                stratagy.setObstacle(new TrueObstacle(), point,modelObstacle);
                ob=stratagy.spawner();
                break;
                
        }
        ob.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().text = number.ToString();
        return ob;
    }
}
public enum ObType
{
    TRUE,
    FALSE
}