using UnityEngine;

public class Sword : MonoBehaviour
{

    public string direction;    //direction initialized in ShootPellets Script
    private int xMovement;
    private int yMovement;
    Vector3 move;

    [Header("Stats")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float duration = 5f;

    void Start()
    {
        //eight directions it can move (compass directions)
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

    //moves sword
    void Update()
    {
        transform.position = transform.position + (move * speed * Time.deltaTime);
    }

    //breaks sword if hits any collider
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
        else if (collider.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
            
        }
        else if (collider.gameObject.CompareTag("Target"))
        {
            Destroy(this.gameObject);
            
        }
    }
    

}
