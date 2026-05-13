using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level3Boss : MonoBehaviour
{

    [Header("Stats")]
    [SerializeField] private int Health;
    private int maxHealth;
    [SerializeField] private int Damage;
    private int maxDamage;
    [SerializeField] private int Defense;
    private int maxDefense;

    [Header("References")]
    [SerializeField] private GameObject DialogueBox;
    private Level3DialogueBossBox dialogueBox;
    [SerializeField] private GameObject Player;
    private Level3PlayerStats playerStats;
    [SerializeField] private TextMeshProUGUI BossHealth;
    [SerializeField] private GameObject MusicManager;
    [Header("Music")]
    [SerializeField] private int LowHealthMusicThreshold = 20;
    private bool playingLowHealthMusic = false;

    // Other vars
    private List<string> dialogue = new List<string>();

  void Start()
  {
    if (!Player) {
        Debug.LogError("Player reference not set in Level3Boss!");
    } else
    {
        playerStats = Player.GetComponent<Level3PlayerStats>();
        if (!playerStats)
        {
            Debug.LogError("PlayerStats script not found on Player GameObject!");
        }
    }

    if (!BossHealth) {
        Debug.LogError("BossHealth TextMeshProUGUI reference not set in Level3Boss!");
    } else
    {
        BossHealth.text = $"Boss Health: {Health}";
    }

    if (!DialogueBox)
    {
        Debug.LogError("Dialogue Boss reference not set");
    } else
    {
        dialogueBox = DialogueBox.GetComponent<Level3DialogueBossBox>();
    }

    maxHealth = Health;
    maxDamage = Damage;
    maxDefense = Defense;
  }

  public void BossTurn()
    {
        dialogue.Clear();
        // for now, just deals damage
        DealDamage();
        EndTurn();
    }


    // Turn Functions
    public void DealDamage()
    {
        int damageTaken = playerStats.TakeDamage(Damage);
        // Debug.Log($"Boss deals {Damage} damage to the player!");
        dialogue.Add("King Circle sprints and hits you with a flying kick!");
        dialogue.Add($"King Circle deals {damageTaken} DAMAGE to the player.");
    }

    // Player Functions
    public int TakeDamage(int damageAmount)
    {
        // update health
        int damageTaken = Mathf.Max(damageAmount - Defense, 0);
        Health -= damageTaken;
        BossHealth.text = $"Boss Health: {Health}";

        // update music
        if (Health <= LowHealthMusicThreshold && !playingLowHealthMusic)
        {
            MusicManager.GetComponent<Level3BossSceneMusic>().PlayLevel3BossMusic(Level3BossMusic.BOSS_LOW);
            playingLowHealthMusic = true;
        }

        if (Health <= 0)
        {
            EndGame();
        }

        return damageTaken;
    }

    // Companion Setters
    public void DebuffDamage()
    {
        Damage -= playerStats.GetGlareDamageDebuff();
        Debug.Log($"Boss's damage decreased by {playerStats.GetGlareDamageDebuff()}! Current damage: {Damage}");
    }
    public void DebuffDefense()
    {
        Defense -= playerStats.GetComplimentDefendDebuff();
        Debug.Log($"Boss's defense decreased by {playerStats.GetComplimentDefendDebuff()}! Current defense: {Defense}");
    }

    // Cleanup Functions
    void EndTurn()
    {
        DialogueBox.SetActive(true);
        dialogueBox.FillDialogue(dialogue);
        dialogueBox.NextDialogue();
    }
    void EndGame()
    {
        Debug.Log("Boss defeated! You win!");
        
        // Updates dialogue box to show victory message, once it reaches the end, idk??
    }

    public void ResetStats()
    {
        Health = maxHealth;
        Defense = maxDefense;
        Damage = maxDamage;
        BossHealth.text = $"Boss Health: {Health}";
    }
}
