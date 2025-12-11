using UnityEngine;

public class CamScript : MonoBehaviour
{
    public PlayerScript targetScript;
    public GameObject player;
    public Collider2D playerCollider2D;
    private string quadrant = "South";
    private Vector3 CameraPosition;
    private Vector3 supposedPos;
    public float speed = 5f;

    public float spawnCameraPositionX;
    public float spawnCameraPositionY;

    void Start()
    {
        Camera mainCamera = Camera.main;
        CameraPosition = this.transform.position;
        supposedPos = CameraPosition;
    }

    void Update()
    {

        if (player != null && Camera.main != null)
        {
            //Put player's worldPos into a vector3; Turn worldPos coords into viewport coords vector
            Vector3 worldPos = player.transform.position;
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(worldPos);
            float xComponent = viewportPos.x;
            float yComponent = viewportPos.y;
            
            // Debug.Log("Viewport Pos: " + viewportPos);
            // Debug.Log("X: " + xComponent);
            // Debug.Log("Y: " + yComponent);

            //figure out where in viewport the player is, and assign a quadrant
            if (xComponent > 0.4 && xComponent < 0.6 && yComponent < 0.2)
            {
                quadrant = "South";
            }
            else if (xComponent > 0.4 && xComponent < 0.6 && yComponent > 0.8)
            {
                quadrant = "North";
            }
            else if (xComponent < 0.2 && yComponent > 0.4 && yComponent < 0.6)
            {
                quadrant = "West";
            }
            else if (xComponent > 0.8 && yComponent > 0.4 && yComponent < 0.6)
            {
                quadrant = "East";
            }
            //Debug.Log(quadrant);
            //Debug.Log(targetScript.hasEnteredTrigger);
            
            //If player hits trigger, move supposedCamera position in direction dictated by quadrant string
            if (targetScript.hasEnteredTrigger)
            {
                if (quadrant == "North")
                {
                    supposedPos.y += 9;
                    CameraPosition = supposedPos;
                }
                else if (quadrant == "South")
                {
                    supposedPos.y -= 9;
                    CameraPosition = supposedPos;
                }
                else if (quadrant == "West")
                {
                    supposedPos.x -= 14;
                    CameraPosition = supposedPos;
                }
                else if (quadrant == "East")
                {
                    supposedPos.x += 14;
                    CameraPosition = supposedPos;
                }
            }
        }

        //Move the camera with Lerp
        transform.position = Vector3.Lerp(transform.position, supposedPos, speed * Time.deltaTime);
        targetScript.OnTriggerEnter2D(playerCollider2D);
        // Debug.Log("camPos: " + CameraPosition);
        // Debug.Log("supPos: " + supposedPos);
    }

    public void RespawnMoveCamera()
    {
        supposedPos = new Vector3(spawnCameraPositionX, spawnCameraPositionY, transform.position.z);
        Debug.Log("works");
    }
}
    