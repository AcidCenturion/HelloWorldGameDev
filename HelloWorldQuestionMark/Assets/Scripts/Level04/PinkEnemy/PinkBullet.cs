using UnityEngine;
using System.Collections;

public class PinkBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float duration = 10f;
    public GameObject player;
    public Rigidbody2D bulletRB;

    private Vector3 direction;
    //private string direction;

    //four variables for damage
    public float radius;
    public LayerMask players;
    public GameObject attackPoint;
    [SerializeField] int attackDamage;

    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");

        if (this.transform.position.x < player.transform.position.x)  //player is to the right of bullet
        {
            direction = new Vector3(1, 0, 0);
            //direction = "left";
            Debug.Log("Im left");
        }
        else if (this.transform.position.x > player.transform.position.x)  //player is to the left of bullet
        {
            direction = new Vector3(-1, 0, 0);
            //direction = "right";
            Debug.Log("Im right");
        }

        Destroy(this.gameObject, duration);
        
    }

    
    void Update()
    {
        //bulletRB.linearVelocity = direction * speed;
        transform.position = transform.position + (direction * speed * Time.deltaTime);
        
        Collider2D[] player = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, players);

        foreach(Collider2D playerGameObject in player)
        {
            Debug.Log("HitEnemy");
            playerGameObject.GetComponent<L4Health>().health -= attackDamage;
            Destroy(this.gameObject);
        }

    }
}
