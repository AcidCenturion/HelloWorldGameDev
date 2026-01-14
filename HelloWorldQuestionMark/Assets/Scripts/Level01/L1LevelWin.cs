using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class L1LevelWin : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public BGMmanager bGMmanager;

    public GameObject player;
    public GameObject cutsceneText;
    public PlayerControls controlsScript;
    public ShootPellets shootScript;
    private AudioSource levelCompleteSound;

    private bool inCutscene = false;
    public bool WinCutsceneFinished = false;
    private bool BossDiesTriggersOnce = false;
    private bool ArtifactCollectedTriggersOnce = false;

    public Artifact artifact;

    public float lerpDuration = 4.0f;

    public Image panel;
    private Coroutine _EndFade;

    private bool fadeFinished = true;

    void Start()
    {
        levelCompleteSound = GetComponent<AudioSource>();

        cutsceneText.SetActive(false);
        artifact.gameObject.SetActive(false);
    }

    void Update()
    {
        // Starts cutscene once boss dies
        if (enemyHealth.bossIsDead == true && !BossDiesTriggersOnce)
        {
            BossDiesTriggersOnce = true;
            inCutscene = true;
            controlsScript.enabled = false;
            shootScript.enabled = false;
            cutsceneText.SetActive(true);
        }

        // Ends cutscene when player presses E
        if (inCutscene)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                WinCutsceneFinished = true;
                DisableCutscene();
                shootScript.enabled = true;
                inCutscene = false;
            }
        }

        // Fades to white when artifact is collected
        if (artifact.artifactCollected == true && !ArtifactCollectedTriggersOnce)
        {
            ArtifactCollectedTriggersOnce = true;
            levelCompleteSound.Play();
            Debug.Log("Artifact collected");
            StartCoroutine(FadeToWhite());
        }

    }

    void DisableCutscene()
    {
        if (WinCutsceneFinished)
        {
            inCutscene = false;
            cutsceneText.SetActive(false);
            controlsScript.enabled = true;
            shootScript.enabled = true;
            artifact.gameObject.SetActive(true);
        }
    }

    [System.Obsolete]
    IEnumerator FadeToWhite()
    {
        float startValue = 0f;
        float endValue = 1f;
        float timeElapsed = 0;

        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, startValue);

        while (timeElapsed < lerpDuration)
        {
            float newAlpha = Mathf.Lerp(startValue, endValue, timeElapsed/lerpDuration);
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, newAlpha);
            
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, endValue);
        
        bGMmanager.StopAllSceneAudio();
        SceneManager.LoadScene(2);
    }

}
