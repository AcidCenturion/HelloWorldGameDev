using UnityEngine;
using System.Collections.Generic;

public class L2RedEnemy : MonoBehaviour
{
    public List<GameObject> waypoints;
    public float speed = 3.0f;
    int index = 0;

    void Start()
    {
        
    }

    
    void Update()
    {
        Vector2 destination = waypoints[index].transform.position;
        Vector2 newPos = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        transform.position = newPos;

        float distance = Vector2.Distance(transform.position, destination);

        if (distance <= 0.05f)
        {
            if (index < waypoints.Count-1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
        }
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
