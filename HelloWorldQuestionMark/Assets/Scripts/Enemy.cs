using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private Transform enemy;
    [SerializeField] public int health = 1;
    [SerializeField] public int damage = 1;
    [SerializeField] public float enemySpeed;

    private EnemyHealth enemyHealth;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Creates a new EnemyHealth
        enemyHealth = enemy.GetComponent<EnemyHealth>();
        enemyHealth.Init(health, damage);

        spriteRenderer = enemy.GetComponent<SpriteRenderer>();
    }

}