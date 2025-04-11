using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyHealth
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

    //Reduces enemy health
    void takeDamage(int damageAmount)
    {
        if (this.currentHealth > 0)
        {
            this.currentHealth -= damageAmount;
        }

        //Resets health to 0 so health is never negative
        if (this.currentHealth <= 0)
        {
            this.currentHealth = 0;
        }
    }

    //Can heal damage?
    void healDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth += damageAmount;
        }

        //Sets health back to maximum so current health never exceeds the maximum
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
