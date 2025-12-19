using UnityEngine;
using System;
using System.IO;
using TMPro;

public class Choices : MonoBehaviour
{
    public GameObject[] choices;
    public GameObject sceneManager;

    void Start()
    {
        // Sets all choices Inactive
        for (int i = 0; i < choices.Length; i ++)
        {
            choices[i].SetActive(false);
        }

        sceneManager = sceneManager != null ? sceneManager : GameObject.Find("sceneManager");
    }

    public bool DisplayChoice(int choiceNum)
    {
        // Get Choices array
        Option[] options = sceneManager.GetComponent<LoadScene>().currentScene.options;
        if (options.Length == 0)
        {
            Debug.LogError("Choices: Failed to get options");
            return false;
        }

        // Sets active the amount of choices available & alters text
        for (int i = 0; i < options.Length; i++)
        {
            choices[i].SetActive(true);
            choices[i].GetComponentInChildren<TextMeshProUGUI>().text = options[i].text;
        }

        return true;
    }
    public bool HideChoice()
    {
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].SetActive(false);
        }

        return true;
    }
}
