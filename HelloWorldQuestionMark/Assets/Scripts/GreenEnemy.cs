using UnityEngine;

public class GreenEnemy : MonoBehaviour
{
    private bool isMovingLeft;
    private EnemyHealth enemyHealth;
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
        // Creates a new EnemyHealth
        enemyHealth = enemy.GetComponent<EnemyHealth>();
        enemyHealth.Init(health);

        spriteRenderer = enemy.GetComponent<SpriteRenderer>();
    }

    void Update ()
    {
        // If the enemy is left of the patrol point and moving left it keeps moving left else moves right
        if (isMovingLeft)
        {
            if (enemy.position.x >= LeftPatrolPoint.position.x)
            {
                Movement(-1);
            }
            else
            {
                ChangeDirection();
            }
        }

        // If the enemy is right of the patrol point and moving right it keeps moving right else moves left
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

        // Changes the direction the enemy faces
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
