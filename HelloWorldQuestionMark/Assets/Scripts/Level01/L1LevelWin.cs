using UnityEngine;

public class L1LevelWin : MonoBehaviour
{
    public EnemyHealth enemyHealth;

    public GameObject player;
    public GameObject cutsceneText;
    public PlayerControls controlsScript;
    public ShootPellets shootScript;

    private bool inCutscene = false;
    public bool WinCutsceneFinished = false;
    private bool triggersOnce = false;

    public Artifact artifact;

    void Start()
    {
        cutsceneText.SetActive(false);
        artifact.gameObject.SetActive(false);
    }

    void Update()
    {
        if (enemyHealth.bossIsDead == true && !triggersOnce)
        {
            triggersOnce = true;
            inCutscene = true;
            controlsScript.enabled = false;
            shootScript.enabled = false;
            cutsceneText.SetActive(true);
        }

        if (inCutscene)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                WinCutsceneFinished = true;
                DisableCutscene();
                shootScript.enabled = true;
                //IntroDialogue.Stop();
                inCutscene = false;
            }
        }

        if (artifact.artifactCollected == true)
        {
            Debug.Log("COLLECTED");
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
}
