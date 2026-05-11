using UnityEngine;
using System.Collections;

public class L4StageManager : MonoBehaviour
{
    public GameObject greenEnemy;
    public GameObject redEnemy;
    public GameObject pinkEnemy;
    public GameObject player;

    L4CamBehavior l4CamBehavior;

    public int stageNumber = 0;
    public float FollowSpeed = 2f;
    private Vector3 currentCameraPosition;
    public bool stageIsCompleted = true;


    void Start()
    {
        l4CamBehavior = GetComponent<L4CamBehavior>();
        currentCameraPosition = transform.position;
    }

    void Update()
    {
        CheckIfStageComplete();
        
    }

    void SpawnEnemies(GameObject enemyType)
    {
        float randomSign = (Random.value < 0.5f) ? -0.1f : 1.1f;
        Vector3 SpawnLocation = new Vector3(randomSign, Random.value, 0);
        //Vector3 WorldPointSpawnLocation = Camera.main.ViewportToWorldPoint(SpawnLocation);
        Instantiate(enemyType, Camera.main.ViewportToWorldPoint(SpawnLocation), Quaternion.identity);
    }

    void ManageEnemiesInStage()
    {
        switch(stageNumber)
        {
            case 1:
            SpawnEnemies(greenEnemy);
            SpawnEnemies(greenEnemy);
            SpawnEnemies(greenEnemy);
            break;

            case 2:
            SpawnEnemies(redEnemy);
            SpawnEnemies(redEnemy);
            SpawnEnemies(redEnemy);
            break;

            case 3:
            SpawnEnemies(pinkEnemy);
            SpawnEnemies(pinkEnemy);
            SpawnEnemies(pinkEnemy);
            break;

            case 4:
            Debug.Log("boss spawn time");
            break;

        }
    }

    void CheckIfStageComplete()
    {
        if (GameObject.FindWithTag("Enemy") == null && l4CamBehavior.CameraFinished && stageIsCompleted)
        {
            Debug.Log("Start!");
            stageNumber++;
            Debug.Log(stageNumber);
            stageIsCompleted = false;
            ManageEnemiesInStage();
        }
        else if(GameObject.FindWithTag("Enemy") == null && l4CamBehavior.CameraFinished)
        {
            l4CamBehavior.CameraFinished = false;
            stageIsCompleted = true;
        }
    }
}


    


