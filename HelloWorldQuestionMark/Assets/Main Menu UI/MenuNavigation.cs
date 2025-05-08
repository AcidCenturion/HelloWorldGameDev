using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject CreditsScreen;

    public void ShowCredits()
    {
        StartScreen.SetActive(false);
        CreditsScreen.SetActive(true);
    }

    public void BackToMenu()
    {
        CreditsScreen.SetActive(false);
        StartScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting..."); // Won't quit in unity - testing purposes
    }

}
