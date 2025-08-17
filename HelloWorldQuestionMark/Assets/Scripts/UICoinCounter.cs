using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICoinCounter : MonoBehaviour
{
    //public string bonk;
    public TMP_Text textElement;

    L2PlayerMovement playerScript;
    public GameObject Player;

    private int CoinCounterHere;
    

    void Start()
    {
        playerScript = Player.GetComponent<L2PlayerMovement>();
        CoinCounterHere = playerScript.CoinCounter;
    }

    // Update is called once per frame
    void Update()
    {
        textElement.text = CoinCounterHere.ToString() + " coins";
        if (playerScript.CoinCounter > CoinCounterHere)
        {
            //Debug.Log("coin got");
            CoinCounterHere = playerScript.CoinCounter;
        }
        else
        {
            CoinCounterHere = playerScript.CoinCounter;
        }
    }
}  

