using UnityEngine;

public class L2PurpleEnemy : MonoBehaviour
{
    public GameObject player;
    public float speed = 2.0f;
    private float distToPlayer;

    void Start()
    {
        
    }

    void Update()
    {
        distToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distToPlayer < 3.5)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        //Debug.Log(distToPlayer);
    }

    //killed if hit with a sword
    void OnTriggerEnter2D(Collider2D other)
    { 
        if(other.CompareTag("Sword"))
        {
            Destroy(this.gameObject);
        }

    } 
}
