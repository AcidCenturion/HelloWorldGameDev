using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
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

    public EnemyHealth(int maxHealth)
    {
        this.currentHealth = maxHealth;
        this.maxHealth = maxHealth;
    }

    // Reduces enemy health
    public bool takeDamage(int damageAmount)
    {
        if (this.currentHealth > 0)
        {
            this.currentHealth -= damageAmount;
            return false;
        }

        // Destroys the enemy if it runs out of health
        if (this.currentHealth <= 0)
        {
            return true;
        }

        return false;
    }

    // Can heal damage?
    public void healDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth += damageAmount;
        }

        // Sets health back to maximum so current health never exceeds the maximum
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
