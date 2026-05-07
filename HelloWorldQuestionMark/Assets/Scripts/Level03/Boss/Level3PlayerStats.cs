using TMPro;
using UnityEngine;

public class Level3PlayerStats : MonoBehaviour
{
    [Header("Player Stats")]
    public TextMeshProUGUI PlayerHealthText;
    [SerializeField] private int health = 100;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int damage = 10;
    [SerializeField] private int defense = 0;
    [SerializeField] private float defendReduction = 0.5f;
    [Header("Action Debuffs")]
    [SerializeField] private int GlareDamageDebuff = 2;
    [SerializeField] private int ComplimentDefendDebuff = 1;

    // Companion Variables
    [Header("Companion Variables")]
    [SerializeField] private int CompanionThreshold = 60;
    [SerializeField] private float rowanAttackBonus = 1.5f;
    [SerializeField] private int perriDefend = 3;
    [SerializeField] private int geminiHealthAdd = 10;
    [SerializeField] private int AllOutAttackDamage = 20;
    public bool isRowanCompanion = false;
    public bool isGeminiCompanion = false;
    public bool isPerriCompanion = false;
    public bool isAllCompanion = false;
    public bool isNoneCompanion = false;

    private Level3PlayerControls playerControls;
    void Start()
    {
        playerControls = this.GetComponent<Level3PlayerControls>();
        if (playerControls == null)
        {
            Debug.LogError("Level3PlayerStats: No Level3PlayerControls component found on the GameObject.");
        }

        // delay so AffinityHolder has time to load
        Invoke(nameof(UpdateCharacterBooleans), 1f);

        PlayerHealthText.text = "Player Health: " + health.ToString();
    }

    // Companion Functions
    public void IncreaseDamage()
    {
        damage = (int)(damage * rowanAttackBonus);
    }
    public void IncreaseDefense()
    {
        defense = perriDefend;
    }
    public void AddHealth()
    {
        health += geminiHealthAdd;
        if (health > maxHealth) health = maxHealth;
        PlayerHealthText.text = "Player Health: " + health.ToString();
    } 

    // Getters
    public int GetDamage()
    {
        return damage;
    }
    public int GetHealth()
    {
        return health;
    }
    public int GetGlareDamageDebuff()
    {
        return GlareDamageDebuff;
    }
    public int GetComplimentDefendDebuff()
    {
        return ComplimentDefendDebuff;
    }
    public int GetAllOutAttackDamage()
    {
        return AllOutAttackDamage;
    }

    /*
        Damage calculated by player's defense
        Reducted Damage if guarding
        Updates UI

        Returns Damage taken
    */
    public int TakeDamage(int amount)
    {
        int damage = playerControls.isGuarding() ?
            Mathf.RoundToInt((amount - defense) * defendReduction) : 
            amount - defense;
        if (damage < 0) 
            damage = 0;
        if (health - damage < 0)
            damage = health;

        health -= damage;
        if (health <= 0)
        {
            health = 0;

            // Handle player death
        }

        PlayerHealthText.text = "Player Health: " + health.ToString();

        return damage;
    }

    /*
        Checks Affinity of all possible companions
    */
    void UpdateCharacterBooleans()
    {
        if (AffinityHolder.Instance == null)
        {
            Debug.LogWarning("No AffinityHolder Instance");
        }

        if (AffinityHolder.Instance.RowanAffinity >= CompanionThreshold)
            isRowanCompanion = true;

        if (AffinityHolder.Instance.GeminiAffinity >= CompanionThreshold)
            isGeminiCompanion = true;

        if (AffinityHolder.Instance.PerriAffinity >= CompanionThreshold)
            isPerriCompanion = true;

        if (isRowanCompanion && isGeminiCompanion && isPerriCompanion)
            isAllCompanion = true;

        if (!isRowanCompanion && !isGeminiCompanion && !isPerriCompanion)
            isNoneCompanion = true;
        
    }

}
