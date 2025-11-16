using UnityEngine;
using System.Collections;


public class L2PlayerHealth : MonoBehaviour
{
    public Rigidbody2D rb;
    public int Health = 3;
    private Vector2 spawnLocation;
    private bool isDead = false;

    private Animator anim;
    public L2PlayerMovement playerScript;

    [Header("iFrames")]
    private SpriteRenderer spriteRend;
    [SerializeField] private float iFrameDuration;
    [SerializeField] private int numberOfFlashes;



    void Awake()
    {
        spawnLocation = transform.position;
        spriteRend = GetComponent<SpriteRenderer>();
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

        if (isDead == true)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    IEnumerator DeathAnimWait()
    {
        playerScript.enabled = false;
        anim.SetBool("isDead", true);
        isDead = true;

        yield return new WaitForSeconds(0.7f);

        rb.position = spawnLocation;
        isDead = false;
        anim.SetBool("isDead", false);
        playerScript.enabled = true;
        Health = 3;
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (Health > 0)
        {
            if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Spike"))
            {
            Health--;
            StartCoroutine(ActivateInvulnerability());
            }
            Debug.Log("health: " + Health);
        }
    }

    IEnumerator ActivateInvulnerability()
    {
        Physics2D.IgnoreLayerCollision(9, 8, true);
        
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0.4f, 0.4f, 1);
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes*2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes*2));
        }
        Physics2D.IgnoreLayerCollision(9, 8, false);
        
    }

}
