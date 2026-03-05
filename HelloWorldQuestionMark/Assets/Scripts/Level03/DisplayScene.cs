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

    // Location Variables
    public GameObject backgroundLocation;
    private Sprite newBackground = null;
    public Sprite[] locations;

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

    // Audio
    public GameObject musicManager;
    public AudioSource typeSFX;

    void Start()
    {
        // Finds all necessary objects
        sceneManager = sceneManager == null ? GameObject.Find("SceneManager") : sceneManager;
        text = text == null ? GameObject.Find("textbox_text") : text;
        charName = charName == null ? GameObject.Find("name_text") : charName;
        backgroundLocation = backgroundLocation == null ? GameObject.Find("visualNovelBackground") : backgroundLocation;
        musicManager = musicManager == null ? GameObject.Find("MusicManager") : musicManager;
    }   

    // Public Function called in LoadScene everytime a new scene is called
    public void UpdateScene()
    {
        currScene = sceneManager.GetComponent<LoadScene>().getCurrentScene();

        // Updates text
        if (typeRoutine != null) StopCoroutine(typeRoutine);        // Stops text printer from overflowing from prev scene
        typeRoutine = StartCoroutine(TypeMessage(currScene.text));  // Starts text printer for curr scene

        // Updates Name
        charName.GetComponent<TextMeshProUGUI>().text = currScene.name != null ? currScene.name : "";

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

        // Update Background Location if needed
        if (currScene.location != null) UpdateLocation(currScene.location);

        GameObject character = currScene.name != null ? findCharacter(currScene.name) : null;
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

        // Update BGM
        if (currScene.bgm != null) UpdateBGM(currScene.bgm);

        // Update selected button in Controls.cs
        sceneManager.GetComponent<Controls>().turnOffSelectedButton();

        prevScene = currScene;
    }

    // Updates Background Location
    void UpdateLocation(string location)
    {
        // Find location in available locations
        for (int i = 0; i < locations.Length; i++)
        {
            if (locations[i].name == location)
            {
                newBackground = locations[i];
            }
        }

        // Failed to find
        if (newBackground == null)
        {
            Debug.Log("Failed to find the location: "+location);
            return;
        }

        // Update background sprite
        backgroundLocation.GetComponent<SpriteRenderer>().sprite = newBackground;
        newBackground = null;
    }

    // Prints character message character by character
    private IEnumerator TypeMessage(string message)
    {
        //Gets text object in editor
        TextMeshProUGUI textObj = text.GetComponent<TextMeshProUGUI>();

        // Resets text
        textObj.text = "";
        
        // Italicizes text if no name is given; assuming thats the player's thoughts
        if (currScene.name == null) textObj.text += "<i>";

        // Slowly adds each character in it
        foreach (char c in message)
        {
            textObj.text += c;
            if (typeSFX != null) typeSFX.Play();
            yield return new WaitForSeconds(textDelay);
        }

        if (currScene.name == null) textObj.text += "</i>";

        typeRoutine = null;

    }

    // Public function to find character GameObject based on name
    // Also used by Controls to update affinity
    public GameObject findCharacter(string name)
    {
        // Browse through character array and look for name??
        switch (name.ToLower())
        {
            case "red":
            case "rowan":
            case "student called rowan":
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
            case "bus driver":
                return hermitCharacter;
            case "king circle":
                return kingCircleCharacter;
            case "placeholder":
                return characterPlaceHolder;
        }
        return null;
    }

    public bool isTyping()
    {
        return typeRoutine != null;
    }

    public void FullDisplayText()
    {
        if (typeRoutine != null)
        {
            // Stops coroutine and fully displays text
            StopCoroutine(typeRoutine);
            text.GetComponent<TextMeshProUGUI>().text = currScene.name != null ? currScene.text : $"<i>{currScene.text}</i>";
            typeRoutine = null;
        }
    }

    private void UpdateBGM(string bgm)
    {
        switch (bgm.ToLower())
        {
            case "morning":
                musicManager.GetComponent<MusicManager>().PlayBGM(Level3BGM.Morning);
                break;
            case "noon":
                musicManager.GetComponent<MusicManager>().PlayBGM(Level3BGM.Noon);
                break;
            case "night":
                musicManager.GetComponent<MusicManager>().PlayBGM(Level3BGM.Night);
                break;
            default:
                Debug.LogError("Failed to find BGM: " + bgm);
                break;
        }
    }
}
