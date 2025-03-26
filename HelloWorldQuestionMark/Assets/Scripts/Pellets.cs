using UnityEngine;

public class Pellets : MonoBehaviour
{
    private GameObject player;
    public string direction;    //direction initialized in ShootPellets Script
    private int xMovement;
    private int yMovement;
    Vector3 move;

    [Header("Stats")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private int dmg = 1;
    [SerializeField] private float duration = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
    
        if (collider.gameObject.CompareTag("Breakable"))
        {
            collider.gameObject.GetComponent<Breakable>().DecreaseDurability(dmg);
        }
        else if (collider.gameObject.CompareTag("Enemy"))
        {
            //Kills enemy, subject to change
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.CompareTag("Boss"))
        {
            //decrease boss health by dmg amount(??)
        }

        Destroy(gameObject);
    }

}
