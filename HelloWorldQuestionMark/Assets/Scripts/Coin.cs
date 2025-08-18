using UnityEngine;

public class Coin : MonoBehaviour
{

    private Rigidbody2D rb;

    //Moves sword in random direction
    void Start()
    {
        Vector2 vel = new Vector2(Random.Range(-4, 4), Random.Range(-4, 4));
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = vel;
    }

    void Update()
    {

    }

    //Destroys coin if touched by player
    void OnTriggerEnter2D(Collider2D other)
    { 
        if(other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }

    }
}
