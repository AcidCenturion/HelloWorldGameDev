using UnityEngine;

public class L2PurpleEnemy : Enemy
{
    public GameObject player;
    private float distToPlayer;

    void Update()
    {
        distToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distToPlayer < 3.5)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, this.enemySpeed * Time.deltaTime);
        }
        //Debug.Log(distToPlayer);
    }
}
