using UnityEngine;

public class ShootPellets : MonoBehaviour
{
    //Script assigned to player game object

    [Header("Directional Input Keys")]
    [SerializeField] private KeyCode left = KeyCode.LeftArrow;
    [SerializeField] private KeyCode right = KeyCode.RightArrow;
    [SerializeField] private KeyCode up = KeyCode.UpArrow;
    [SerializeField] private KeyCode shoot = KeyCode.Space;

    //Pellets are shot to the right by default, changeable via input keys
    private string direction = "right";
    private int rotation = 270;
    public GameObject pellet;
    private Vector2 spawnLocation;
    public float spawnDistance;

    //Cooldown Variables
    private float lastTimeShot = 0;
    [SerializeField] private float cooldown = 0.5f;
    private AudioSource sound;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        DefaultDirection();
        
        //sets spawn location incase player never touched directional keys
        spawnLocation = new Vector2(transform.position.x + spawnDistance, transform.position.y);
    }
    void Update()
    {
        
        //changes direction based on input keys
        ChangeDirection();
        if (Input.GetKeyDown(shoot))
        {
            Shoot();
        }
    }

    private void ChangeDirection()
    {
        float temp = Mathf.Sqrt(Mathf.Pow(spawnDistance, 2) + Mathf.Pow(spawnDistance, 2)) / 2;
        if (Input.GetKey(left) && Input.GetKey(up))
        {
            direction = "leftUp";
            rotation = 315;
            spawnLocation = new Vector2(transform.position.x - temp, transform.position.y + temp);
        }
        else if (Input.GetKey(right) && Input.GetKey(up))
        {
            direction = "rightUp";
            rotation = 225;
            spawnLocation = new Vector2(transform.position.x + temp, transform.position.y + temp);
        }
        else if (Input.GetKey(left))
        {
            direction = "left";
            rotation = 0;
            spawnLocation = new Vector2(transform.position.x - spawnDistance, transform.position.y);
        }
        else if (Input.GetKey(up))
        {
            direction = "up";
            rotation = 270;
            spawnLocation = new Vector2(transform.position.x, transform.position.y + spawnDistance);
        }
        else if (Input.GetKey(right))
        {
            direction = "right";
            rotation = 180;
            spawnLocation = new Vector2(transform.position.x + spawnDistance, transform.position.y);
        }
        else
        {
            switch (direction)
            {
                case "leftUp":
                    spawnLocation = new Vector2(transform.position.x - temp, transform.position.y + temp);
                    break;
                case "rightUp":
                    spawnLocation = new Vector2(transform.position.x + temp, transform.position.y + temp);
                    break;
                case "left":
                    spawnLocation = new Vector2(transform.position.x - spawnDistance, transform.position.y);
                    break;
                case "up":
                    spawnLocation = new Vector2(transform.position.x, transform.position.y + spawnDistance);
                    break;
                case "right":
                    spawnLocation = new Vector2(transform.position.x + spawnDistance, transform.position.y);
                    break;
            }
        }
        //Debug.Log(direction);

    }

    private void Shoot()
    {
        if (Time.time < lastTimeShot + cooldown)
        {
            return;
        }

        //Instantiates pellet with direction
        GameObject pel = Instantiate(pellet, spawnLocation, Quaternion.Euler(0,0,rotation));
        pel.GetComponent<Pellets>().direction = this.direction;
        sound.Play();
        Debug.Log(spawnLocation);

        //starts cooldown
        lastTimeShot = Time.time;
    }

    private void DefaultDirection()
    {
        direction = "right";
        rotation = 180;
        spawnLocation = new Vector2(transform.position.x + spawnDistance, transform.position.y);
    }
}