using UnityEngine;

public class Lvl2OldManCutscene : MonoBehaviour
{

    public GameObject player;
    public GameObject cutsceneText;
    private AudioSource swordEquip;

    private bool inCutscene = false;
    private bool oldManCutsceneFinished = false;

    void Start()
    {
        swordEquip = GetComponent<AudioSource>();
        cutsceneText.SetActive(false);
    }

    void Update()
    {
        if (inCutscene)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                {
                    oldManCutsceneFinished = true;
                    DisableCutscene();
                }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!oldManCutsceneFinished)
        {
            if (col.gameObject == player || col.gameObject.CompareTag("Player"))
            {
                inCutscene = true;
                player.GetComponent<L2PlayerMovement>().enabled = false;
                player.GetComponent<L2PlayerShoot>().enabled = false;
                player.GetComponent<Rigidbody2D>().linearVelocityX = 0;
                player.GetComponent<Rigidbody2D>().linearVelocityY = 0;
                cutsceneText.SetActive(true);
            }
        }
    }

    void DisableCutscene()
    {
        if (oldManCutsceneFinished)
        {
            inCutscene = false;
            cutsceneText.SetActive(false);
            player.GetComponent<L2PlayerMovement>().enabled = true;
            player.GetComponent<L2PlayerShoot>().enabled = true;
            swordEquip.Play();
        }
    }
}
