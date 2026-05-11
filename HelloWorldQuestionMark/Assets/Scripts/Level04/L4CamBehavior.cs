using UnityEngine;

public class L4CamBehavior : MonoBehaviour
{
    public GameObject player;
    public float FollowSpeed = 2f;
    private Vector3 currentCameraPosition;
    L4StageManager l4StageManager;
    //public bool CameraShouldFollow = false;
    public bool CameraFinished = true;

    void Start()
    {
        currentCameraPosition = transform.position;
        l4StageManager = GetComponent<L4StageManager>();
    }

    
    void Update()
    {
        CameraSwitchToFollow();
        CameraSwitchToStatic();
        
    }

    void CameraSwitchToFollow()
    {
        if (!CameraFinished)
        {
            //CameraShouldFollow = true;

            Vector3 newPos = new Vector3(player.transform.position.x, currentCameraPosition.y, -10f);

            if (newPos.x > transform.position.x)
            {
                transform.position = Vector3.Lerp(transform.position, newPos, FollowSpeed * Time.deltaTime);   
            }    
            
        }
        
    }

    void CameraSwitchToStatic()
    {
        if (!CameraFinished)
        {
            switch (l4StageManager.stageNumber)
            {
                case 0: 
                transform.position = new Vector3(0, 0, -10f);
                CameraFinished = true;
                break;

                case 1:
                if (transform.position.x >= 18)
                    {
                        Debug.Log("camera is static now");
                        transform.position = new Vector3(18, 0, -10f);
                        CameraFinished = true; //time to start next stage
                        //l4StageManager.stageIsCompleted = false;
                        
                    }
                break;

                case 2:
                if (transform.position.x >= 36)
                    {
                        transform.position = new Vector3(36, 0, -10f);
                        //l4StageManager.stageIsCompleted = false;
                        CameraFinished = true; //time to start next stage
                    }
                break;

                case 3:
                if (transform.position.x >= 54)
                    {
                        transform.position = new Vector3(54, 0, -10f);
                        //l4StageManager.stageIsCompleted = false;
                        CameraFinished = true; //time to start next stage
                    }
                break;

            }
        
        }
        
    }
}
