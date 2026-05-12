using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
    Create functions and pass them to buttons
*/
public class Level3PlayerControls : MonoBehaviour
{
    // Enums
    public enum PlayerState {Idle, Guard}

    public enum ButtonState {Default, Action}

    // States
    private ButtonState currentButtonState = ButtonState.Default;
    private PlayerState currentPlayerState = PlayerState.Idle;
    private bool hasCalledForHelp = false;

    // References
    [Header("Button References")]
    public GameObject ButtonContainer;
    public GameObject DefaultButtonsContainer;
    public GameObject ActionButtonsContainer;
    [Header("Other References")]
    public GameObject DialogueBox;
    public GameObject Boss;
    private Level3Boss bossScript;
    private Level3PlayerStats playerStats;

    // Other
    List<string> dialogue = new List<string>();
    

  void Start()
  {
    if (!Boss) {
        Debug.LogError("Boss reference not set in Level3PlayerControls!");
    } else
    {
        bossScript = Boss.GetComponent<Level3Boss>();
        if (!bossScript)
        {
            Debug.LogError("Boss script not found on Boss GameObject!");
        }
    }
    playerStats = GetComponent<Level3PlayerStats>();
    if (!playerStats)
    {
        Debug.LogError("PlayerStats script not found on Player GameObject!");
    }
  }

    //Default Button functions
    public void HandlePunch()
    {
        dialogue.Add("You run up to King Circle and give him your best punch!");
        int damageTaken = bossScript.TakeDamage(playerStats.GetDamage());
        dialogue.Add($"King Circle takes {damageTaken} DAMAGE.");
        EndTurn();
    }
    public void HandleCallForHelp()
    {
        dialogue.Add("Called for Help!");
        if (!hasCalledForHelp)
        {
            // do regular logic
            Debug.Log("Hasnt called for help yet");
            if (playerStats.isNoneCompanion)
            {
                // tell dialogue that you have not made friends w anyone
                dialogue.Add("Rowan, Perri, and Gemini heard your cries!");
                dialogue.Add("...");
                dialogue.Add("They all turned their heads away?");
            } else
            {
                if (playerStats.isRowanCompanion)
                {
                    // increase attack
                    dialogue.Add("Rowan heard your cries.");
                    dialogue.Add("ATTACK increased.");
                    playerStats.IncreaseDamage();

                } 
                if (playerStats.isGeminiCompanion)
                {
                    // add health
                    dialogue.Add("Gemini heard your cries.");
                    dialogue.Add("HEALTH increased.");
                    playerStats.AddHealth();
                    // update health player healthbar
                } 
                if (playerStats.isPerriCompanion)
                {
                    // increase defense 
                    dialogue.Add("Perri heard your cries.");
                    dialogue.Add("DEFENSE increased.");
                    playerStats.IncreaseDefense();
                }
                
            }

            hasCalledForHelp = true;
        } else
        {
            dialogue.Add("You already called for help!");
        }

        EndTurn();
    }
    public void HandleGuard()
    {
        if (currentPlayerState == PlayerState.Idle)
        {
            currentPlayerState = PlayerState.Guard;
            dialogue.Add("You take a stance.");
        } 
        EndTurn();
    }

    // Action Button functions
    public void HandleObserve()
    {
        // just does random dialouge 
        dialogue.Add("You gaze at King Circle.");
        dialogue.Add("He gazes back");
        dialogue.Add("You blush a little");
        EndTurn();
    }
    public void HandleSpare()
    {
        if (!playerStats.isAllCompanion || !hasCalledForHelp)
        {
            dialogue.Add("Rowan, Perri, and Gemini all aren't here! We can't attack all at once!");
            EndTurn();
            return;
        }

        // All out attack
        dialogue.Add("All out attack!!");
        int damageTaken = bossScript.TakeDamage(playerStats.GetAllOutAttackDamage());
        dialogue.Add($"King Circle takes {damageTaken} DAMAGE.");
        EndTurn();
    }
    public void HandleCompliment()
    {
        dialogue.Add("You compliment the King's good looks.");
        dialogue.Add("King Circle is caught off guard!");
        dialogue.Add("King Circle's DEFENSE lowered!");

        bossScript.DebuffDefense();
        EndTurn();
    }
    public void HandleGlare()
    {
        dialogue.Add("You glare at King Circle!");
        dialogue.Add("King Circle is intimidated.");
        dialogue.Add("King Circle's ATTACK lowered!");

        bossScript.DebuffDamage();
        EndTurn();
    }



    
    // Cleanup functions
    /*
        Reset player state to default
        Sets button state to default
        Player no longer guarding
    */
    public void ResetPlayerState()
    {
        currentPlayerState = PlayerState.Idle;
        currentButtonState = ButtonState.Default;
    }
    /*
        Called after player finishes turn
        Mainly after player attacks 
        BEFORE dialogue box appears and player presses it to continue to boss turn
    */
    void EndTurn()
    {
        // uncomment when done testing
        ButtonContainer.SetActive(false);
        DialogueBox.SetActive(true);
        DialogueBox.GetComponent<Level3DialogueBossBox>().FillDialogue(dialogue);
        DialogueBox.GetComponent<Level3DialogueBossBox>().NextDialogue();
        // bossScript.BossTurn();
        // StartPlayerTurn(); // remove this after dialogue implementation
    }
    /*
        Called after boss finishes turn
        Mainly after boss attacks and player presses the dialogue box to continue
    */
    public void StartPlayerTurn()
    {
        if (currentButtonState == ButtonState.Action)
        {
            ToggleButtonLayout();
        }
        ResetPlayerState();
        ButtonContainer.SetActive(true);
        dialogue.Clear();

    }


  
    /*
        Toggle between default and action button layouts
    */
    public void ToggleButtonLayout()
    {
        if (currentButtonState == ButtonState.Default)
        {
            DefaultButtonsContainer.SetActive(false);
            ActionButtonsContainer.SetActive(true);
            currentButtonState = ButtonState.Action;
        } else
        {
            DefaultButtonsContainer.SetActive(true);
            ActionButtonsContainer.SetActive(false);
            currentButtonState = ButtonState.Default;
        }
    }

    public bool isGuarding()
    {
        return currentPlayerState == PlayerState.Guard;
    }

    public void SetButtonsActive(bool active)
    {
        ButtonContainer.SetActive(active);
    }
     
}
