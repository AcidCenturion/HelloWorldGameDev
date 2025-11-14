using UnityEngine;
using System.Collections.Generic;

public class L2RedEnemy : Enemy
{
    public List<GameObject> waypoints;
    int index = 0;
    
    void Update()
    {
        Vector2 destination = waypoints[index].transform.position;
        Vector2 newPos = Vector2.MoveTowards(transform.position, destination, this.enemySpeed * Time.deltaTime);
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
}
