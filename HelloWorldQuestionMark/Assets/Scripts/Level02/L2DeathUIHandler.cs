using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using UnityEngine.SceneManagement;


public class L2DeathUIHandler : MonoBehaviour
{
    private UIDocument _document;
    public L2PlayerHealth healthScript;
    public CamScript camScript;

    private VisualElement rootElement;    
    private Button _respawnButton;
    private Button _mainMenuButton;

    private bool hasDied = false;
    public bool hasClickedRespawn = false;

    void Start()
    {
        _document = GetComponent<UIDocument>();
        rootElement = _document.rootVisualElement.Q<VisualElement>("Container");

        _respawnButton = _document.rootVisualElement.Q("RespawnButton") as Button;
        _mainMenuButton = _document.rootVisualElement.Q("MainMenuButton") as Button;

        _respawnButton.RegisterCallback<ClickEvent>(OnRespawnClick);
        _mainMenuButton.RegisterCallback<ClickEvent>(OnMainMenuClick);

        rootElement.style.display = DisplayStyle.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthScript.Health <= 0)
        {
            if (hasDied == false)
            {
                hasDied = true;
                ShowDeathUI();   
            }
        }
        else
        {
            hasDied = false;
        }
    }

    void ShowDeathUI()
    {
        StartCoroutine(waitABit());
    }

    IEnumerator waitABit()
    {
        yield return new WaitForSeconds(0.7f);
        Time.timeScale = 0f;
        rootElement.style.display = DisplayStyle.Flex;
    }

    private void OnRespawnClick(ClickEvent evt)
    {
        hasClickedRespawn = true;
        rootElement.style.display = DisplayStyle.None;
        Time.timeScale = 1f;
        Debug.Log("respawn button works");

        camScript.RespawnMoveCamera();  
    }

    private void OnMainMenuClick(ClickEvent evt)
    {
        SceneManager.LoadScene(2); //should be main menu scene
        Debug.Log("mainumenu button works");       
    }

    private void OnDisable()
    {
        _respawnButton.UnregisterCallback<ClickEvent>(OnRespawnClick);
        _mainMenuButton.UnregisterCallback<ClickEvent>(OnMainMenuClick);
    }
}
