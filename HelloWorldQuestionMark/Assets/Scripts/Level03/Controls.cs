using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    // Buttons
    public GameObject Option1; // up arrow
    public GameObject Option2; // left arrow
    public GameObject Option3; // right arrow
    public GameObject Option4; // down arrow
    private GameObject selectedButton = null;

    // Visual
    public Color selectedColor;
    public Color defaultColor;

    public GameObject sceneManager;

    void Start()
    {
        sceneManager = sceneManager ? sceneManager : GameObject.Find("SceneManager");

        // Set all buttons to default color
        if (Option1.activeSelf) Option1.GetComponent<Image>().color = defaultColor;   
        if (Option2.activeSelf) Option2.GetComponent<Image>().color = defaultColor;
        if (Option3.activeSelf) Option3.GetComponent<Image>().color = defaultColor;
        if (Option4.activeSelf) Option4.GetComponent<Image>().color = defaultColor;

        // Maybe Disable all buttons initially
        // Then let Choices script load them in
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space))
        {

            // Call choices script function to make choice
            int choice = -1;
            if (selectedButton == Option1) choice = 0;
            else if (selectedButton == Option2) choice = 1;
            else if (selectedButton == Option3) choice = 2;
            else if (selectedButton == Option4) choice = 3;

            // If didnt choose option, but required to; uncomment later when Choices is done
            // if (selectedButton == null && Option1.activeSelf) return;

            sceneManager.GetComponent<LoadScene>().ChooseNextScene(choice);
            
            // Reset selected button
            if (selectedButton) selectedButton.GetComponent<Image>().color = defaultColor;
            selectedButton = null;
            return;
        }

        // Selects button if any present
        if (!Option1.activeSelf) return;

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            // Does option exist
            if (!Option1.activeSelf) return;

            // Update selected visual
            if (selectedButton != null) selectedButton.GetComponent<Image>().color = defaultColor;
            selectedButton = Option1;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            // Does option exist
            if (!Option2.activeSelf) return;

            // Update selected visual
            if (selectedButton != null) selectedButton.GetComponent<Image>().color = defaultColor;
            selectedButton = Option2;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            // Does option exist
            if (!Option3.activeSelf) return;

            // Update selected visual
            if (selectedButton != null) selectedButton.GetComponent<Image>().color = defaultColor;
            selectedButton = Option3;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            // Does option exist
            if (!Option4.activeSelf) return;

            // Update selected visual
            if (selectedButton != null) selectedButton.GetComponent<Image>().color = defaultColor;
            selectedButton = Option4;
        }
        if (selectedButton != null) selectedButton.GetComponent<Image>().color = selectedColor;

    }
}
