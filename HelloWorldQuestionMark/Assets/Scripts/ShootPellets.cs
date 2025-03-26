using UnityEngine;

public class ShootPellets : MonoBehaviour
{
    //Script assigned to player game object

    [Header("Directional Input Keys")]
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;
    [SerializeField] private KeyCode up;
    [SerializeField] private KeyCode shoot;

    private string direction;
    private int rotation = 0;
    public GameObject pellet;
    private Vector2 spawnLocation;
    public float spawnDistance;

    //Cooldown Variables
    private float lastTimeShot = 0;
    [SerializeField] private float cooldown = 0.5f;

    void Update()
    {
        ChangeDirection();
        if (Input.GetKeyDown(shoot))
        {
            Shoot();
        }
    }

    private void ChangeDirection()
    {
        if (Input.GetKey(left) && Input.GetKey(up))
        {
            direction = "leftUp";
            rotation = 45;
            float temp = Mathf.Sqrt(Mathf.Pow(spawnDistance, 2) + Mathf.Pow(spawnDistance, 2)) / 2;
            spawnLocation = new Vector2(transform.position.x - temp, transform.position.y + temp);
        }
        else if (Input.GetKey(right) && Input.GetKey(up))
        {
            direction = "rightUp";
            rotation = -45;
            float temp = Mathf.Sqrt(Mathf.Pow(spawnDistance, 2) + Mathf.Pow(spawnDistance, 2)) / 2;
            spawnLocation = new Vector2(transform.position.x + temp, transform.position.y + temp);
        }
        else if (Input.GetKey(left))
        {
            direction = "left";
            rotation = 90;
            spawnLocation = new Vector2(transform.position.x - spawnDistance, transform.position.y);
        }
        else if (Input.GetKey(up))
        {
            direction = "up";
            rotation = 0;
            spawnLocation = new Vector2(transform.position.x, transform.position.y + spawnDistance);
        }
        else if (Input.GetKey(right))
        {
            direction = "right";
            rotation = 270;
            spawnLocation = new Vector2(transform.position.x + spawnDistance, transform.position.y);
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
        //Debug.Log(spawnLocation);

        //starts cooldown
        lastTimeShot = Time.time;
    }

}