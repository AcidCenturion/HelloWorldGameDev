using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Health
{

    [SerializeField] private int damage;
    [SerializeField] private int maxHealth;
    private int currentHealth;
    private bool isInvulnerable;

    public int entityHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
        }
    }

    public Health(int maxHealth)
    {
        this.currentHealth = maxHealth;
        this.maxHealth = maxHealth;
    }

    void takeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

    void healDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth += damageAmount;
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    

    //TODO check if the player got hit and from which direction
    //TODO give the player iframes
    //TODO add knockback to the player
}
