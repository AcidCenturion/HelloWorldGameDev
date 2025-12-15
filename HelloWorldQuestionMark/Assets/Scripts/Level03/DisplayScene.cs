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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneManager = sceneManager == null ? GameObject.Find("SceneManager") : sceneManager;
        text = text == null ? GameObject.Find("textbox_text") : text;
        charName = charName == null ? GameObject.Find("name_text") : charName;

        UpdateScene();
    }   

    public void UpdateScene()
    {
        currScene = sceneManager.GetComponent<LoadScene>().currentScene;

        // Updates text and name
        text.GetComponent<TextMeshProUGUI>().text = currScene.text;
        charName.GetComponent<TextMeshProUGUI>().text = currScene.name;

        if (currScene.options != null && currScene.options.Length > 0)
        {
            // Call function from choices.cs
        }

        // Update Background image if needed
        if (currScene.location != null) updateImage(currScene.location);

        // Update Character image if needed

        // Update selected button in Controls.cs
        sceneManager.GetComponent<Controls>().turnOffSelectedButton();
    }

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
}
