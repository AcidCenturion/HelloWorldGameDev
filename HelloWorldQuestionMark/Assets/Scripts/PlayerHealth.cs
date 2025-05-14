using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerRespawn playerRespawn;
    public int maxHealth = 3;
    private int currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        playerRespawn = GetComponent<PlayerRespawn>();
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        Debug.Log("Player took damage, Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            playerRespawn.Respawn();
            // Die();
        }
    }

    public void Die() {
        Debug.Log("Player is dead");
    }

    public void ResetHealth() {
        currentHealth = maxHealth;
        Debug.Log("Health Reset to: " + currentHealth);
    }

    // Takes damage when colliding with enemies
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponentInParent<EnemyHealth>();
            Debug.Log("Player took damage: " + enemyHealth.entityDamage);
            TakeDamage(enemyHealth.entityDamage);
        }
    }

}
