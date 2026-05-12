using UnityEngine;

public class BossShockwave : MonoBehaviour
{
    private GameObject player;
    private Vector3 direction;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float duration = 5f;


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
    }
}
