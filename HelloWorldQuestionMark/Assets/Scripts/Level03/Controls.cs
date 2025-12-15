using UnityEngine;
using UnityEngine.EventSystems;
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
    private ColorBlock standardColors;

    public GameObject sceneManager;

    void Start()
    {
        sceneManager = sceneManager ? sceneManager : GameObject.Find("SceneManager");

        // Sets specific color object
        standardColors = Option1.GetComponent<Button>().colors;
        standardColors.normalColor = defaultColor;
        standardColors.highlightedColor = selectedColor;

        // Set specific colors for button
        if (Option1.activeSelf) Option1.GetComponent<Button>().colors = standardColors;   
        if (Option2.activeSelf) Option2.GetComponent<Button>().colors = standardColors;
        if (Option3.activeSelf) Option3.GetComponent<Button>().colors = standardColors;
        if (Option4.activeSelf) Option4.GetComponent<Button>().colors = standardColors;
        
        // Maybe Disable all buttons initially in start??
        // Then let Choices script load them in??
        
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space))
        {
            // Calls function if selection doesn't exist
            // Only for scenes where options do not exist
            // prevents double ChooseNextScenes
            if (selectedButton == null) sceneManager.GetComponent<LoadScene>().ChooseNextScene(-1);

            // Removes selected visualizer
            selectedButton = null;

            return;
        }

        // Selects button if any present
        if (!Option1.activeSelf) return;

        // Selects button on input
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            // Does option exist
            if (!Option1.activeSelf) return;

            selectedButton = Option1;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            // Does option exist
            if (!Option2.activeSelf) return;

            selectedButton = Option2;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            // Does option exist
            if (!Option3.activeSelf) return;

            selectedButton = Option3;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            // Does option exist
            if (!Option4.activeSelf) return;

            selectedButton = Option4;
        }

        // Select chosen option
        if (selectedButton) EventSystem.current.SetSelectedGameObject(selectedButton);

    }

    // Public function for DisplayScene
    // Called everytime a new scene is displayed
    // Lets Keyboard & Mouse controls not clash with each other
    public void turnOffSelectedButton()
    {
        // deselects any option
        EventSystem.current.SetSelectedGameObject(null);
        selectedButton = null;
    }
}
