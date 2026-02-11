using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerRespawn playerRespawn;
    public int maxHealth = 3;
    private int currentHealth;
    public float invulnTimerMax = 1;
    private float invulnTimerVal;

    public AudioSource playerHurtSound;
    
    void Start()
    {
        currentHealth = maxHealth;
        playerRespawn = GetComponent<PlayerRespawn>();
    }

    void FixedUpdate()
    {
        if (invulnTimerVal > 0)
        {
            invulnTimerVal -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage, Current health: " + currentHealth);
        playerHurtSound.Play();

        if (currentHealth <= 0)
        {
            playerRespawn.Respawn();
            // Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player is dead");
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        Debug.Log("Health Reset to: " + currentHealth);
    }

    // Takes damage when colliding with enemies
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && invulnTimerVal <= 0)
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponentInParent<EnemyHealth>();
            Debug.Log("Player took damage: " + enemyHealth.entityDamage);
            TakeDamage(enemyHealth.entityDamage);
            invulnTimerVal = invulnTimerMax;
        }
    }

    public int GetHealth()
    {
        return this.currentHealth;
    }

}
