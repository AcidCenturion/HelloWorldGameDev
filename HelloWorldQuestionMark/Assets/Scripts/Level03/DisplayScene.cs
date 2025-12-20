using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//TODO
// FINISH PLACEHOLDERS OF ALL THE CHARACTERS

public class DisplayScene : MonoBehaviour
{
    public GameObject sceneManager;
    private Scene currScene;
    private Scene prevScene = null;

    public GameObject text;
    public GameObject charName;

    // Image Variables
    public GameObject backgroundImage;
    private Sprite newBackground = null;
    public Sprite[] images;

    // Text Display
    public float textDelay = 0.05f;
    private Coroutine typeRoutine = null;

    // Character Display Objects; Add more if needed
    public GameObject characterPlaceHolder;
    public GameObject purpleCharacter;
    public GameObject redCharacter;
    public GameObject greenCharacter;
    public GameObject kingCircleCharacter;
    public GameObject hermitCharacter;
    
    void Start()
    {
        // Finds all necessary objects
        sceneManager = sceneManager == null ? GameObject.Find("SceneManager") : sceneManager;
        text = text == null ? GameObject.Find("textbox_text") : text;
        charName = charName == null ? GameObject.Find("name_text") : charName;
    }   

    // Public Function called in LoadScene everytime a new scene is called
    public void UpdateScene()
    {
        currScene = sceneManager.GetComponent<LoadScene>().currentScene;

        // Updates text
        if (typeRoutine != null) StopCoroutine(typeRoutine);        // Stops text printer from overflowing from prev scene
        typeRoutine = StartCoroutine(TypeMessage(currScene.text));  // Starts text printer for curr scene

        // Updates Name
        charName.GetComponent<TextMeshProUGUI>().text = currScene.name;

        // Remove dialogue options from past scene
        if (prevScene != null && prevScene.options != null && prevScene.options.Length > 0)
        {
            if (!sceneManager.GetComponent<Choices>().HideChoice()) 
                Debug.LogError("DisplayScene: Failed to clear choices");
        }

        // Display dialogue options if needed
        if (currScene.options != null && currScene.options.Length > 0)
        {
            if (!sceneManager.GetComponent<Choices>().DisplayChoice(currScene.options.Length)) 
                Debug.LogError("Display Scene Failed to display choices");
        }

        // Update Background image if needed
        if (currScene.location != null) UpdateImage(currScene.location);

        GameObject character = findCharacter(currScene.name);
        // Update base model if currently none or changed to new character
        if (character != null && (prevScene == null || (prevScene.name != currScene.name)))
        {
            // Load character
            if (!character.GetComponent<Character>().DisplayCharacterBase())
                Debug.LogError("Failed to load base model of: " + currScene.name);
        }

        // Update face
        if (prevScene != null && character != null)
        {
            if (currScene.emotion == null)
            {
                // Load default face
                if (!character.GetComponent<Character>().DisplayCharacterFace())
                    Debug.LogError("Failed to load character face of: default face");
            }
            else
            {
                // Load requested face
                if (!character.GetComponent<Character>().DisplayCharacterFace(currScene.emotion))
                    Debug.LogError("Failed to load character face of: " + currScene.emotion);
            }
        }

        // Character with no portrait speaking --> turn off character display
        if (character == null)
        {
            // Turn off character display if its not alr off
            if (redCharacter.GetComponent<Character>().isCharacterDisplayActive()) // Doesn't have to be red, just need an object that can use the methods
                redCharacter.GetComponent<Character>().ResetCharacterDisplay();
        }

        // Update selected button in Controls.cs
        sceneManager.GetComponent<Controls>().turnOffSelectedButton();

        prevScene = currScene;
    }

    // Updates Background Image
    void UpdateImage(string location)
    {
        // Find location in available images
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].name == location)
            {
                newBackground = images[i];
            }
        }

        // Failed to find
        if (newBackground == null)
        {
            Debug.Log("Failed to find the location: "+location);
            return;
        }

        // Update background sprite
        backgroundImage.GetComponent<SpriteRenderer>().sprite = newBackground;
        newBackground = null;
    }

    // Prints character message character by character
    private IEnumerator TypeMessage(string message)
    {
        //Gets text object in editor
        TextMeshProUGUI textObj = text.GetComponent<TextMeshProUGUI>();

        // Resets text
        textObj.text = "";

        // Slowly adds each character in it
        foreach (char c in message)
        {
            textObj.text += c;
            yield return new WaitForSeconds(textDelay);
        }
    }

    private GameObject findCharacter(string name)
    {
        // Browse through character array and look for name??
        switch (name.ToLower())
        {
            case "red":
            case "rowan":
                return redCharacter;
            case "purple":
            case "perri":
                return purpleCharacter;
            case "green":
            case "gemini":
                return greenCharacter;
            case "hermit":
            case "old hermit":
            case "professor hermit":
                return hermitCharacter;
            case "king circle":
                return kingCircleCharacter;
            case "placeholder":
                return characterPlaceHolder;
        }
        return null;
    }
}
