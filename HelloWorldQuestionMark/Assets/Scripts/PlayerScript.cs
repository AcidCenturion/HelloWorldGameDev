using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int somePublicVariable = 5;
    public bool hasEnteredTrigger = false;

    //figure out when player collides with a cameraMoveTrigger; set the hasEnteredTrigger bool to true
    public void OnTriggerEnter2D(Collider2D other)      
    {
        if (other.CompareTag("CameraMoveZone"))
        {
            //Debug.Log("Collided!!!");
            hasEnteredTrigger = true;

        }
        else
        {
            //Debug.Log("Nope");
            hasEnteredTrigger = false;
        }
    }

}
