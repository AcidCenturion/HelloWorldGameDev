using UnityEngine;

public class BossShockwave : MonoBehaviour
{
    private GameObject player;
    private Vector3 direction;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float duration = 5f;

    //four variables for damage
    public float radius;
    public LayerMask players;
    public GameObject attackPoint;
    [SerializeField] int attackDamage;


    void Start()
    {
    
        player = GameObject.Find("L4Player");
    
        if (transform.position.x < player.transform.position.x)  //player is to the right of bullet
        {
            direction = new Vector3(1, 0, 0);
            //Debug.Log("Im left");
        }
        else if (transform.position.x > player.transform.position.x)  //player is to the left of bullet
        {
            direction = new Vector3(-1, 0, 0);
            //Debug.Log("Im right");
        }

        Destroy(this.gameObject, duration);
    }

    void Update()
    {
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
