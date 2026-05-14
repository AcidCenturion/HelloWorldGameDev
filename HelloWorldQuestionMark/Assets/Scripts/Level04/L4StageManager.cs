using UnityEngine;
using System.Collections;

public class L4StageManager : MonoBehaviour
{
    public L4CutsceneManager l4CutsceneManager;

    public GameObject greenEnemy;
    public GameObject redEnemy;
    public GameObject pinkEnemy;
    public GameObject bossEnemy;
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
        if (!l4CutsceneManager.inCutscene)
        {
            CheckIfStageComplete();    
        }
        
        
    }

    void SpawnEnemies(GameObject enemyType)
    {
        float randomSide = (Random.value < 0.5f) ? -0.05f : 1.05f;
        float randomHeight = (Random.Range(0.0f, 0.4f));
        Vector3 SpawnLocation = new Vector3(randomSide, randomHeight, 0);
        //Vector3 WorldPointSpawnLocation = Camera.main.ViewportToWorldPoint(SpawnLocation);
        Instantiate(enemyType, new Vector3(Camera.main.ViewportToWorldPoint(SpawnLocation).x, Camera.main.ViewportToWorldPoint(SpawnLocation).y, 0), Quaternion.identity);
    }

    void ManageEnemiesInStage()
    {
        switch(stageNumber)
        {
            case 1:
            // SpawnEnemies(bossEnemy);    //USE FOR TESTING BOSS
            SpawnEnemies(greenEnemy);
            SpawnEnemies(greenEnemy);
            l4CutsceneManager.whichCutscene++;
            break;

            case 2:
            SpawnEnemies(greenEnemy);
            SpawnEnemies(redEnemy);
            SpawnEnemies(redEnemy);
            l4CutsceneManager.whichCutscene++;
            break;

            case 3:
            SpawnEnemies(greenEnemy);
            SpawnEnemies(redEnemy);
            SpawnEnemies(pinkEnemy);
            SpawnEnemies(pinkEnemy);
            l4CutsceneManager.whichCutscene++;
            break;

            case 4:
            SpawnEnemies(bossEnemy);
            l4CutsceneManager.whichCutscene++;
            break;

        }
    }

    void CheckIfStageComplete()
    {
        if (GameObject.FindWithTag("Enemy") == null && l4CamBehavior.CameraFinished && stageIsCompleted )
        {
            if (stageNumber == 0)
            {
                //Debug.Log("Start game !");    
            }
            else
            {
                //Debug.Log("Start!");
            }
            stageNumber++;
            //Debug.Log(stageNumber);
            stageIsCompleted = false;
            ManageEnemiesInStage();
            
        }
        else if(GameObject.FindWithTag("Enemy") == null && l4CamBehavior.CameraFinished)
        {
            if (stageNumber < 4)
            {
                l4CamBehavior.CameraFinished = false;
                stageIsCompleted = true;
                
            }
            if (stageNumber == 4)
            {
                l4CutsceneManager.inCutscene = true;
            }
        
               
        }
    }
}


    


