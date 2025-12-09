using UnityEngine;

public class L2PlayerShoot : MonoBehaviour
{
    
    private Vector2 moveDirection;
    private string direction;
    private int rotation;
    private float spawnDistance;
    private Vector2 spawnLocation;
    public GameObject sword;
    private AudioSource fireSword; 

    private float lastTimeShot = 0;
    [SerializeField] private float cooldown = 0.5f;
    
    void Start()
    {
        fireSword = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessInputs();
        setSpawnDist();
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        //Debug.Log("aaaa" + moveDirection);
        //Debug.Log("moveDirection: " + moveDirection);
        //Debug.Log("direction: " + direction);
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void setSpawnDist()
    {
        if (moveDirection.x == 0 && moveDirection.y == 1)
        {
            direction = "north";
            rotation = 270;
            spawnDistance = 1;
            spawnLocation = new Vector2(transform.position.x, transform.position.y + spawnDistance);
        }
        if (moveDirection.x == 1 && moveDirection.y == 0)
        {
            direction = "east";
            rotation = 180;
            spawnDistance = 1;
            spawnLocation = new Vector2(transform.position.x + spawnDistance, transform.position.y);
        }
        if (moveDirection.x == 0 && moveDirection.y == -1)
        {
            direction = "south";
            rotation = 90;
            spawnDistance = 1;
            spawnLocation = new Vector2(transform.position.x, transform.position.y - spawnDistance);
        }
        if (moveDirection.x == -1 && moveDirection.y == 0)
        {
            direction = "west";
            rotation = 0;
            spawnDistance = 1;
            spawnLocation = new Vector2(transform.position.x - spawnDistance, transform.position.y);
        }
        if (moveDirection.x > 0 && moveDirection.y > 0)
        {
            direction = "northeast";
            rotation = 225;
            spawnDistance = 1.414f;
            spawnLocation = new Vector2(transform.position.x + spawnDistance, transform.position.y + spawnDistance);
        }
        if (moveDirection.x > 0 && moveDirection.y < 0)
        {
            direction = "southeast";
            rotation = 135;
            spawnDistance = 1.414f;
            spawnLocation = new Vector2(transform.position.x + spawnDistance, transform.position.y - spawnDistance);
        }
        if (moveDirection.x < 0 && moveDirection.y < 0)
        {
            direction = "southwest";
            rotation = 45;
            spawnDistance = 1.414f;
            spawnLocation = new Vector2(transform.position.x - spawnDistance, transform.position.y - spawnDistance);
        }
        if (moveDirection.x < 0 && moveDirection.y > 0)
        {
            direction = "northwest";
            rotation = 315;
            spawnDistance = 1.414f;
            spawnLocation = new Vector2(transform.position.x - spawnDistance, transform.position.y + spawnDistance);
        }
        
    }

    private void Shoot()
    {
        if (Time.time < lastTimeShot + cooldown)
        {
            return;
        }

        //Instantiates pellet with direction
        GameObject pel = Instantiate(sword, spawnLocation, Quaternion.Euler(0,0,rotation));
        pel.GetComponent<Sword>().direction = this.direction;
        
        //plays sound effect
        fireSword.Play();

        //starts cooldown
        lastTimeShot = Time.time;
    }
}
