using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayScene : MonoBehaviour
{
    public GameObject sceneManager;
    private Scene currScene;

    public GameObject text;
    public GameObject charName;

    // Image Variables
    public GameObject backgroundImage;
    private Sprite newBackground = null;
    public Sprite[] images;

    // Text Display
    public float textDelay = 0.05f;
    private Coroutine typeRoutine = null;



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

        if (currScene.options != null && currScene.options.Length > 0)
        {
            // Call function from choices.cs??
            // Sets a number of options as ACTIVE
            // Remaining options are set as INACTIVE
        }

        // Update Background image if needed
        if (currScene.location != null) updateImage(currScene.location);

        // Update Character image if needed

        // Update selected button in Controls.cs
        sceneManager.GetComponent<Controls>().turnOffSelectedButton();
    }

    // Updates Background Image
    void updateImage(string location)
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
}
