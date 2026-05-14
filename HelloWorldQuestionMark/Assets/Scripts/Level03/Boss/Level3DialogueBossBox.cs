using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3DialogueBossBox : MonoBehaviour
{
    List<string> dialogue = new List<string>();
    private int currDialogue = 0;

    public TextMeshProUGUI text;
    private bool isPlayerTurn = false;
    public GameObject Player;
    private Level3PlayerControls player;
    private Level3PlayerStats playerStats;
    public GameObject Boss;
    private Level3Boss boss;
    public GameObject GameOverUI;
    public GameObject MusicManager;

    bool isBossDefeated = false;

  void Start()
  {
    if (!Player)
    {
        Debug.LogWarning("No player object referenced");
    } else
    {
        player = Player.GetComponent<Level3PlayerControls>();
        playerStats = Player.GetComponent<Level3PlayerStats>();
    }

    if (!Boss)
    {
        Debug.LogWarning("No Boss Object referenced");
    } else
    {
        boss = Boss.GetComponent<Level3Boss>();
    }

    StartGame();

  }

   public void StartGame()
    {
        boss.ResetStats();
        playerStats.ResetStats();

        // intro to fight
    dialogue.Add("KING CIRCLE challenges you to a fight!");
    dialogue.Add("Prepare to fight!");
    NextDialogue();
    GameOverUI.SetActive(false);
    MusicManager.GetComponent<Level3BossSceneMusic>().PlayLevel3BossMusic(Level3BossMusic.REGULAR);

    }

    public bool GetIsBossDefeated()
    {
        return isBossDefeated;
    }
    public void SetIsBossDefeated(bool value)
    {
        isBossDefeated = value;
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
            // check if boss defeated
            if (isBossDefeated)
            {
                SceneManager.LoadScene("Level3");
                return;
            }

            // check if this is player death
            if (playerStats.GetHealth() <= 0)
            {
                GameOverUI.SetActive(true);
                isPlayerTurn = false;
                return;
            }

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
