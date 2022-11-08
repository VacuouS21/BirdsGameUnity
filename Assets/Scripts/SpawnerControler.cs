using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class SpawnerControler : MonoBehaviour
{
    public GameObject[] obstacleVariants;
    public GameObject modelObstacle;
    public float timeBtwSpawn;
    public float startTimeBtwSpawn;
    public float decreaseTime;
    public float minTime = 0.65f;

    public int minIntRand;
    public int maxIntRand;
    
    public Text randomString;

    private static Queue<GameObject> cloneObjsList = new Queue<GameObject>();
    private EnemiesFactory enemiesFactory;
    private static Queue<string> randomNumber=new Queue<string>();
    private int firstTrueNumber;
    private int secondTrueNumber;
    private int numberTrue;
    private int numberFalseFirst;
    private int numberFalseSecond;
    // Start is called before the first frame update
    void Start()
    {
        enemiesFactory = new EnemiesFactory(minIntRand,maxIntRand);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (timeBtwSpawn <= 0)
        {
            createNewNumbers();
            randomString.text = randomNumber.Dequeue();

            int rand = UnityEngine. Random.Range(0, obstacleVariants.Length);
            bool checkFirst = true;
            for (int i = 0; i < obstacleVariants.Length; i++)
            {

                //Rand number for True obstacle   
               
                GameObject newObstacle = null;
                //Go to factory True or False number
                if (i == rand) newObstacle = enemiesFactory.createObstacle(ObType.TRUE, obstacleVariants[i], modelObstacle, numberTrue);

                else if (checkFirst) {
                    newObstacle = enemiesFactory.createObstacle(ObType.FALSE, obstacleVariants[i], modelObstacle,numberFalseFirst); 
                    checkFirst= false;
                }
                else
                {
                    newObstacle = enemiesFactory.createObstacle(ObType.FALSE, obstacleVariants[i], modelObstacle,numberFalseSecond);
                }
                
                cloneObjsList.Enqueue(newObstacle);
            }
            timeBtwSpawn = startTimeBtwSpawn;
            if (startTimeBtwSpawn > minTime)
            {
                startTimeBtwSpawn -= decreaseTime;
            }
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }

   public static void deleteObstacle()
    {
        for(int i = 0; i < 3; i++)
        {
            Destroy(cloneObjsList.Dequeue());
            
        }
        
    }
    public void createNewNumbers()
    {
        firstTrueNumber = UnityEngine.Random.Range(minIntRand, maxIntRand);
        secondTrueNumber = UnityEngine.Random.Range(minIntRand, maxIntRand);
        randomNumber.Enqueue(firstTrueNumber + "x" + secondTrueNumber);

        numberTrue = firstTrueNumber * secondTrueNumber;
        while (numberTrue == numberFalseFirst)
        {
            numberFalseFirst = UnityEngine.Random.Range(secondTrueNumber * 2, maxIntRand * minIntRand + 1);
        }
        while (numberTrue == numberFalseSecond || numberFalseFirst==numberFalseSecond)
        {
            numberFalseSecond = UnityEngine.Random.Range(secondTrueNumber * 2, maxIntRand * minIntRand + 1);
        }
    }
}
