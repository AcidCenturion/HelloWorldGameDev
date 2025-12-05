using TMPro;
using UnityEngine;

public class DisplayScene : MonoBehaviour
{
    public GameObject sceneManager;
    private Scene currScene;

    public GameObject text;
    public GameObject charName;
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
    }
}
