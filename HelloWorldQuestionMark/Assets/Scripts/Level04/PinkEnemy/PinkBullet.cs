using UnityEngine;

public class PinkBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject player;
    public Rigidbody2D bulletRB;

    private Vector3 direction;

    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();

        if (transform.position.x < player.transform.position.x)  //player is to the right of bullet
        {
            Vector3 direction = new Vector3(-1, 0, 0);
            Debug.Log("Im left");
        }
        else if (transform.position.x > player.transform.position.x)  //player is to the left of bullet
        {
            Vector3 direction = new Vector3(1, 0, 0);
            Debug.Log("Im right");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        bulletRB.linearVelocity = direction * speed;
        transform.position = transform.position + (direction * speed * Time.deltaTime);
    }
}
