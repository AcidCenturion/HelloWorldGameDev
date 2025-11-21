using UnityEngine;

public class CameraZoneEnemyCollision : MonoBehaviour
{

    public GameObject player;
    
    Collider2D cameraZoneCollider;
    Collider2D playerCollider;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraZoneCollider = (Collider2D)this.gameObject.GetComponentAtIndex(3);
        playerCollider = player.GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(cameraZoneCollider, playerCollider, true);
        
    }

   
}
