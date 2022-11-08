using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Strategy
{
    MyObstacle obstacle;
    GameObject point;
    GameObject model;
    public GameObject spawner()
    {
        return obstacle.spawn(point,model); 
    }

    public void setObstacle(MyObstacle obstacle,GameObject point, GameObject model)
    {
        this.obstacle = obstacle;
        this.point = point;
        this.model = model;
    }
    // Start is called before the first frame update 
}

 public interface MyObstacle{
   public GameObject spawn(GameObject point,GameObject model);
}



public class TrueObstacle : MonoBehaviour,MyObstacle
{

    GameObject MyObstacle.spawn(GameObject point,GameObject model)
    {
        model.tag = "True";        
        return Instantiate(model, point.transform.position, Quaternion.identity);
    }
}
public class FalseObstacle : MonoBehaviour, MyObstacle
{
    public GameObject spawn(GameObject point, GameObject model)
    {
        model.tag = "False";
        return Instantiate(model, point.transform.position, Quaternion.identity); ;
    }
}