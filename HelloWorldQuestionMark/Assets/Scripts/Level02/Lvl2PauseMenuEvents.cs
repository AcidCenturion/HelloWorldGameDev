using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Lvl2PauseMenuEvents : MonoBehaviour
{
    private UIDocument _document;
    public UIDocument _SettingsDocument;
    public UIDocument _ControlsDocument;

    private VisualElement rootElement;    
    private VisualElement SettingsRootElement;
    private VisualElement ControlsRootElement;
    private Button _resumeButton;
    private Button _settingsButton;
    private Button _mainMenuButton;
    private Button _controlsButton;
    private Button _backButton;
    private Button _backButtonControls;

    private bool isPaused = false;
    private bool inSettings = false;
    private bool inControls = false;

    public AudioMixer audioMixer;
    public Slider musicSlider;



    void Start()
    {
        _document = GetComponent<UIDocument>();
        rootElement = _document.rootVisualElement.Q<VisualElement>("Container");
        SettingsRootElement = _SettingsDocument.rootVisualElement.Q<VisualElement>("Container");
        ControlsRootElement = _ControlsDocument.rootVisualElement.Q<VisualElement>("Container");

        _resumeButton = _document.rootVisualElement.Q("ResumeButton") as Button;
        _settingsButton = _document.rootVisualElement.Q("SettingsButton") as Button;
        _mainMenuButton = _document.rootVisualElement.Q("MainMenuButton") as Button;
        _controlsButton = _SettingsDocument.rootVisualElement.Q("ControlsButton") as Button;
        _backButton = _SettingsDocument.rootVisualElement.Q("BackButton") as Button;
        _backButtonControls = _ControlsDocument.rootVisualElement.Q("BackButton") as Button;

        musicSlider = _SettingsDocument.rootVisualElement.Q<Slider>("MusicSlider");

        _resumeButton.RegisterCallback<ClickEvent>(OnResumeClick);
        _settingsButton.RegisterCallback<ClickEvent>(OnSettingsClick);
        _mainMenuButton.RegisterCallback<ClickEvent>(OnMainMenuClick);
        _controlsButton.RegisterCallback<ClickEvent>(OnControlsClick);
        _backButton.RegisterCallback<ClickEvent>(OnBackClick);
        _backButtonControls.RegisterCallback<ClickEvent>(OnBackControlsClick);

        musicSlider.RegisterValueChangedCallback(OnVolumeChanged);

        rootElement.style.display = DisplayStyle.None;
        SettingsRootElement.style.display = DisplayStyle.None;
        ControlsRootElement.style.display = DisplayStyle.None;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                if (inSettings)
                {
                    if (inControls)
                    {
                        inControls = false;
                        ControlsRootElement.style.display = DisplayStyle.None;
                    }
                    else
                    {
                        inSettings = false;
                        SettingsRootElement.style.display = DisplayStyle.None;   
                    }
                }
                else
                {
                    ResumeGame();
                }
            }
            else
            {
                PauseGame();
            }
        }
    }


    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        
        rootElement.style.display = DisplayStyle.Flex;
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1.0f;

        rootElement.style.display = DisplayStyle.None;
    }



    private void OnResumeClick(ClickEvent evt)
    {
        isPaused = false;
        Time.timeScale = 1.0f;

        rootElement.style.display = DisplayStyle.None;
        
        Debug.Log("resume button works");
    }

    private void OnSettingsClick(ClickEvent evt)
    {
        inSettings = true;
        SettingsRootElement.style.display = DisplayStyle.Flex;
        Debug.Log("setting button works");        
    }

    private void OnMainMenuClick(ClickEvent evt)
    {
        SceneManager.LoadScene(2); //should be main menu scene
        Debug.Log("mainumenu button works");       
    }

    private void OnControlsClick(ClickEvent evt)
    {
        inControls = true;
        ControlsRootElement.style.display = DisplayStyle.Flex;
        Debug.Log("controls button works");       
    }

    private void OnBackClick(ClickEvent evt)
    {
        inSettings = false;
        SettingsRootElement.style.display = DisplayStyle.None;
        Debug.Log("back button works");       
    }

    private void OnBackControlsClick(ClickEvent evt)
    {
        inControls = false;
        ControlsRootElement.style.display = DisplayStyle.None;
        Debug.Log("back ctrl button works");       
    }

    private void OnDisable()
    {
        _resumeButton.UnregisterCallback<ClickEvent>(OnResumeClick);
        _settingsButton.UnregisterCallback<ClickEvent>(OnSettingsClick);
        _mainMenuButton.UnregisterCallback<ClickEvent>(OnMainMenuClick);
        _controlsButton.UnregisterCallback<ClickEvent>(OnControlsClick);
        _backButton.UnregisterCallback<ClickEvent>(OnBackClick);
        _backButtonControls.UnregisterCallback<ClickEvent>(OnBackControlsClick);

        musicSlider.UnregisterValueChangedCallback(OnVolumeChanged);
    }

    private void OnVolumeChanged(ChangeEvent<float> evt)
    {
        audioMixer.SetFloat("MusicVolume", (Mathf.Log10(evt.newValue) * 12) - 14f); //idk why this looks so weird. i dislike log scales and i aint changing it. it does work.
    }
}
