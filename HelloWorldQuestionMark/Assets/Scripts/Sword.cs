using UnityEngine;

public class Sword : MonoBehaviour
{

    public string direction;    //direction initialized in ShootPellets Script
    private int xMovement;
    private int yMovement;
    Vector3 move;

    [Header("Stats")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private int dmg = 1;
    [SerializeField] private float duration = 5f;

    void Start()
    {
        //eight  directions it can move (compass directions)
        switch (direction)
        {
            case "west":
                xMovement = -1;
                yMovement = 0;
                break;
            case "south":
                xMovement = 0;
                yMovement = -1;
                break;
            case "north":
                xMovement = 0;  
                yMovement = 1;
                break;
            case "east":
                xMovement = 1;
                yMovement = 0;
                break;
            case "northeast":
                xMovement = 1;
                yMovement = 1;
                break;
            case "northwest":
                xMovement = -1;
                yMovement = 1;
                break;
            case "southwest":
                xMovement = -1;
                yMovement = -1;
                break;
            case "southeast":
                xMovement = 1;
                yMovement = -1;
                break;
        }

        //sets Vector to move towards
        move = new Vector3(xMovement, yMovement, 0);
        move.Normalize();   //necessary for pellets shot at an angle
        
        Destroy(gameObject, duration);
    }

    void Update()
    {
        transform.position = transform.position + (move * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject != null)
        {
            Destroy(this.gameObject);
        }
    }
    
    
    
    
    
    
    
    //create method that destroys itself when it contacts collider
    // void OnTriggerEnter2D(Collider2D collider)
    // {
    
    //      Debug.Log("Object hit");
    //     if (collider.gameObject.CompareTag("Breakable"))
    //     {
    //         Debug.Log("wall hit");
    //         //Trigger in Breakable Objects always in a child object of the breakable object
    //         collider.gameObject.GetComponentInParent<Breakable>().DecreaseDurability(dmg);
            
    //     }
    //     else if (collider.gameObject.CompareTag("Enemy"))
    //     {
    //         //Trigger in Enemy Objects always in a child object of the enemy object
    //         //Makes the enemy take 1 point of damage; enemy will die once health <= 0
    //         //Works for Bosses too
    //          Debug.Log("Enemy detected");
    //         EnemyHealth enemyHealth = collider.gameObject.GetComponentInParent<EnemyHealth>();
    //         enemyHealth.takeDamage(1);
    //     }

    //     Destroy(gameObject);
    // }
    

}
