using UnityEngine;

public class FirstVoicelineDialogue : MonoBehaviour
{
    public GameObject player;
    public GameObject cutsceneText;
    private AudioSource IntroDialogue;
    public PlayerControls controlsScript;
    public ShootPellets shootScript;

    private bool inCutscene = false;
    private bool FirstDialogueCutsceneFinished = false;

    void Start()
    {
        // controlsScript.enabled = false;
        // shootScript.enabled = false;
        IntroDialogue = GetComponent<AudioSource>();
        cutsceneText.SetActive(false);
    }

    void Update()
    {
        if (inCutscene)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                {
                    FirstDialogueCutsceneFinished = true;
                    DisableCutscene();
                    shootScript.enabled = true;
                    IntroDialogue.Stop();
                }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!FirstDialogueCutsceneFinished)
        {
            // if (col.gameObject == player || col.gameObject.CompareTag("Player"))
            // {
                inCutscene = true;
                controlsScript.enabled = false;
                shootScript.enabled = false;
                //player.GetComponent<Rigidbody2D>().linearVelocityX = 0;
                //player.GetComponent<Rigidbody2D>().linearVelocityY = 0;
                cutsceneText.SetActive(true);
                IntroDialogue.Play();
            // }
        }
    }

    void DisableCutscene()
    {
        if (FirstDialogueCutsceneFinished)
        {
            inCutscene = false;
            cutsceneText.SetActive(false);
            controlsScript.enabled = true;
            shootScript.enabled = true;
            
        }
    }
}
