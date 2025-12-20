using System;
using UnityEngine;
using UnityEngine.UI;

// How characters are displayed:
// There will be one character game object in the canvas (base model and face)
// There will be multiple character GameObjects in the root (containing the base sprite and all sprites)
// In DisplayScene, DisplayCharacter() is called to update the character gameObject in canvas
// If no name or character is mc, then ResetCharacterDisplay() is called to clear the character from the screen
public class Character : MonoBehaviour
{
    public float defaultAffinity;
    public GameObject characterDisplay;
    public GameObject faceDisplay;
    public Sprite baseModel; 
    public Sprite[] Faces;
    enum Emotion {normal, happy, love, mad, sad};

    void Start()
    {
        // DisplayCharacterBase();
        ResetCharacterDisplay();
    }

    public bool DisplayCharacterBase()
    {
        // Checks if character display isnt active
        if (!characterDisplay.activeSelf)
        {
            characterDisplay.SetActive(true);
            faceDisplay.SetActive(true);
        }

        // Set Base Model
        if (baseModel == null)
        {
            Debug.LogError("Character: No base model provided");
            return false;
        }
        characterDisplay.GetComponent<Image>().sprite = baseModel;

        return true;
    }
    public bool DisplayCharacterFace(string emotion = "normal")
    {
        // Checks if character display isnt active
        if (!characterDisplay.activeSelf)
        {
            characterDisplay.SetActive(true);
            faceDisplay.SetActive(true);
        }

        // Get Emotion
        if (!Enum.TryParse(emotion.ToLower(), false, out Emotion emotionEnum))
        {
            Debug.LogError("Failed to find emotion for: '" + emotion + "'. Check capital and lowercase letters");
            return false;
        }

        // Set Emotion
        switch (emotionEnum)
        {
            case Emotion.happy:
                faceDisplay.GetComponent<Image>().sprite = Faces[(int)Emotion.happy];
                break;
            case Emotion.love:
                faceDisplay.GetComponent<Image>().sprite = Faces[(int)Emotion.love];
                break;
            case Emotion.mad:
                faceDisplay.GetComponent<Image>().sprite = Faces[(int)Emotion.mad];
                break;
            case Emotion.sad:
                faceDisplay.GetComponent<Image>().sprite = Faces[(int)Emotion.sad];
                break;
            default:
                faceDisplay.GetComponent<Image>().sprite = Faces[(int)Emotion.normal];
                break;

        }

        return true;
        
    }
    public void ResetCharacterDisplay()
    {
        characterDisplay.GetComponent<Image>().sprite = null;
        faceDisplay.GetComponent<Image>().sprite = null;
        
        characterDisplay.SetActive(false);
        faceDisplay.SetActive(false);
    }

    public bool isCharacterDisplayActive()
    {
        return characterDisplay.activeSelf;
    }
}
