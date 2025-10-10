using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;



    //Moves coin in random direction
    void Start()
    {
        Vector2 vel = new Vector2(Random.Range(-4, 4), Random.Range(-4, 4));
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = vel;

        anim = GetComponent<Animator>();
    }

    void Update()
    {

    }

    //Destroys coin if touched by player
    void OnTriggerEnter2D(Collider2D other)
    { 
        if(other.CompareTag("Player"))
        {
            anim.SetBool("collected", true);
            
            StartCoroutine(breakCo());
        }

    }

    IEnumerator breakCo()
    {
        yield return new WaitForSeconds(0.1f);

        Destroy(this.gameObject);
    }
}
