using UnityEngine;
using System.Collections;


public class L2PlayerHealth : MonoBehaviour
{
    public Rigidbody2D rb;
    public int Health = 3;
    private Vector2 spawnLocation;

    private Animator anim;
    public L2PlayerMovement playerScript;



    void Awake()
    {
        spawnLocation = transform.position;
        //Debug.Log("spawn loc: " + spawnLocation);
    }
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerScript = GetComponent<L2PlayerMovement>();
    }

    void Update()
    {
        
        if (Health == 0)
        {
            StartCoroutine(DeathAnimWait());
        }
    }

    IEnumerator DeathAnimWait()
    {
        playerScript.enabled = false;
        rb.linearVelocity = Vector2.zero;
        anim.SetBool("isDead", true);

        yield return new WaitForSeconds(1.0f);

        rb.position = spawnLocation;
        anim.SetBool("isDead", false);
        playerScript.enabled = true;
        Health = 3;
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Health --;
        }
        //Debug.Log("health: " + Health);
    }

}
