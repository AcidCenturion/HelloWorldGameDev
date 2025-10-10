using UnityEngine;

public class Pellets : MonoBehaviour
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
        //five directions it can move: left, leftUp, up, rightUp, right
        switch (direction)
        {
            case "left":
                xMovement = -1;
                yMovement = 0;
                break;
            case "leftUp":
                xMovement = -1;
                yMovement = 1;
                break;
            case "up":
                xMovement = 0;
                yMovement = 1;
                break;
            case "rightUp":
                xMovement = 1;
                yMovement = 1;
                break;
            case "right":
                xMovement = 1;
                yMovement = 0;
                break;
        }

        //sets Vector to move towards
        move = new Vector3(xMovement, yMovement, 0);
        move.Normalize();   //necessary for pellets shot at an angle
        
        Destroy(gameObject, duration);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (move * speed * Time.deltaTime);
    }

    //create method that destroys itself when it contacts collider
    void OnTriggerEnter2D(Collider2D collider)
    {
    
        Debug.Log("Object hit");
        if (collider.gameObject.CompareTag("Breakable"))
        {
            Debug.Log("wall hit");
            //Trigger in Breakable Objects always in a child object of the breakable object
            collider.gameObject.GetComponentInParent<Breakable>().DecreaseDurability(dmg);
            
        }
        else if (collider.gameObject.CompareTag("Enemy"))
        {
            //Trigger in Enemy Objects always in a child object of the enemy object
            //Makes the enemy take 1 point of damage; enemy will die once health <= 0
            //Works for Bosses too
            Debug.Log("Enemy detected");
            EnemyHealth enemyHealth = collider.gameObject.GetComponentInParent<EnemyHealth>();
            enemyHealth.takeDamage(1);
        }

        Destroy(gameObject);
    }
    

}
