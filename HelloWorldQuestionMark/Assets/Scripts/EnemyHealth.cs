using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    private int maxHealth;
    private int currentHealth;
    private int damage;
    private bool isInvulnerable;

    public bool bossIsDead = false;

    const int LEVEL3_SCENE_INDEX = 6;


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

    public int entityDamage
    {
        get
        {
            return this.damage;
        }

        set
        {
            this.damage = value;
        }
    }

    // Initializes health values
    public void Init(int health, int damage)
    {
        this.currentHealth = health;
        this.maxHealth = health;
        this.damage = damage;
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
            if (this.gameObject.name == "L2BossEnemy")
            {
                bossIsDead = true;
                SceneManager.LoadScene(LEVEL3_SCENE_INDEX);
                Destroy(gameObject);
            } else if (this.gameObject.name == "Boss")
            {
                SceneManager.LoadScene("Level2");
            }
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
