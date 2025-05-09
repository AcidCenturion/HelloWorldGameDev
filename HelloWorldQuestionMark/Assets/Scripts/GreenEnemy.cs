using UnityEngine;

public class GreenEnemy : MonoBehaviour
{
    private bool isMovingLeft;
    public EnemyHealth enemyHealth;
    private SpriteRenderer spriteRenderer;

    [Header("Patrol Points")]
    [SerializeField] private Transform RightPatrolPoint;
    [SerializeField] private Transform LeftPatrolPoint;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Speed")]
    [SerializeField] private float enemySpeed;
    [SerializeField] public int health;

    void Start()
    {
        enemyHealth = new EnemyHealth(health);
        spriteRenderer = enemy.GetComponent<SpriteRenderer>();
    }

    void Update ()
    {
        if (isMovingLeft)
        {
            // If the enemy is left of the patrol point and moving left it keeps moving left
            if (enemy.position.x >= LeftPatrolPoint.position.x)
            {
                Movement(-1);
            }
            else
            {
                ChangeDirection();
            }
        }

        // If the enemy is right of the patrol point and moving right it keeps moving right
        else
        {
             if (enemy.position.x <= RightPatrolPoint.position.x)
             {
                 Movement(1);
             }
             else
             {
                ChangeDirection();
             }
         }

    }

    void Movement (int direction)
    {
        enemy.position = new Vector2(enemy.position.x + Time.deltaTime * direction * enemySpeed, enemy.position.y);
    }

    void ChangeDirection ()
    {

        // Changes the direction the enemy moves
        isMovingLeft = !isMovingLeft;
        if (isMovingLeft)
        {
            enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            enemy.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
