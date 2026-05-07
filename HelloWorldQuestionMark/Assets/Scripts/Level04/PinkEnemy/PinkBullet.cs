using UnityEngine;
using System.Collections;

public class PinkBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float duration = 10f;
    public GameObject player;
    public Rigidbody2D bulletRB;

    private Vector3 direction;

    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();

        if (transform.position.x < player.transform.position.x)  //player is to the right of bullet
        {
            direction = new Vector3(1, 0, 0);
            Debug.Log("Im left");
        }
        else if (transform.position.x > player.transform.position.x)  //player is to the left of bullet
        {
            direction = new Vector3(-1, 0, 0);
            Debug.Log("Im right");
        }

        Destroy(this.gameObject, duration);
        
    }

    
    void Update()
    {
        //bulletRB.linearVelocity = direction * speed;
        transform.position = transform.position + (direction * speed * Time.deltaTime);
    }

    IEnumerator InterspaceAttack()
    {
        yield return new WaitForSeconds(2.0f);

    }
}
