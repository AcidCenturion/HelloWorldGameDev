using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private int damage;
    [SerializeField] private int maxHealth;
    private int currentHealth;
    private bool isInvulnerable;

    public int entityHealth
    {
        get
        {
            return this.currentHealth;
        }

        set
        {
            this.currentHealth = value;
        }
    }

    // Initializes health values
    public void Init(int health)
    {
        currentHealth = health;
        maxHealth = health;
    }

    // Reduces enemy health
    public void takeDamage(int damageAmount)
    {
        if (this.currentHealth > 0)
        {
            this.currentHealth -= damageAmount;
        }

        // Debug check to see that the Enemy is taking the corect amount of damage
        Debug.Log("Enemy took damage: " + this.currentHealth);

        // Destroys the enemy if it runs out of health
        if (this.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Can heal damage?
    public void healDamage(int damageAmount)
    {
        if (this.currentHealth > 0)
        {
            this.currentHealth += damageAmount;
        }

        // Sets health back to maximum so current health never exceeds the maximum
        if (this.currentHealth > this.maxHealth)
        {
            this.currentHealth = this.maxHealth;
        }
    }
}
