using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level3DialogueBossBox : MonoBehaviour
{
    List<string> dialogue = new List<string>();
    private int currDialogue = 0;

    public TextMeshProUGUI text;
    private bool isPlayerTurn = false;
    public GameObject Player;
    private Level3PlayerControls player;
    public GameObject Boss;
    private Level3Boss boss;

  void Start()
  {
    if (!Player)
    {
        Debug.LogWarning("No player object referenced");
    } else
    {
        player = Player.GetComponent<Level3PlayerControls>();
    }

    if (!Boss)
    {
        Debug.LogWarning("No Boss Object referenced");
    } else
    {
        boss = Boss.GetComponent<Level3Boss>();
    }

    // intro to fight
    dialogue.Add("This is boss fight");
    dialogue.Add("Prepare to fight");
    NextDialogue();

  }


    /*
        Used after every player/boss action
    */
    public void FillDialogue(List<string> newDialogue)
    {
        Debug.Log("Filled new dialogue");
        dialogue.Clear();
        dialogue = newDialogue;
        currDialogue = 0;
    }

    /*
        Button Function
        Iterates to the next dialouge
        Starts new turn if reached end of list
    */
    public void NextDialogue()
    {
        if (dialogue == null || dialogue.Count <= 0)
        {
            Debug.Log("No dialogue list or its 0");
            return;
        }
        Debug.Log("DialogueCount is " + dialogue.Count + " and curr is " + currDialogue);

        // checks if already reached last one
        if (currDialogue >= dialogue.Count)
        {
            Debug.Log("Reached last dialogue");
            text.text = "";
            currDialogue = 0;
            StartNextTurn();
            return;
        }

        text.text = dialogue[currDialogue];
        currDialogue += 1;
    }

    void StartNextTurn()
    {
        if (isPlayerTurn)
        {
            // boss 
            isPlayerTurn = !isPlayerTurn;
            boss.BossTurn();
        } else
        {
            player.StartPlayerTurn();
            isPlayerTurn = !isPlayerTurn;
            gameObject.SetActive(false);
        }

    }

    
}
