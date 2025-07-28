using UnityEngine;

public class FirstVoicelineTrigger : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;
    [Header("Temp Variables")]
    bool isActivated = false;
    bool reachedLocation = false;
    public Transform destination; //use empty game object's Transform for this
    private float timer;
    public float pauseAfterReached = 1;
    public AudioSource voiceline;

    void OnEnable()
    {
        Debug.Log("Enabled");
        GetComponent<BoxCollider2D>().enabled = true;
        voiceline = GetComponent<AudioSource>();
        isActivated = false;
        reachedLocation = false;

        AudioSource[] audiolist = GetComponents<AudioSource>();
        voiceline = audiolist[0];
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //if player hasnt activated it
        if (!isActivated)
        {
            return;
        }

        if (reachedLocation)
        {
            timer += Time.deltaTime;

            //Boss pauses before starting battle, time for voicelines
            if (timer < pauseAfterReached)
            {
                return;
            }

            player.GetComponent<PlayerControls>().enabled = true;
            player.GetComponent<ShootPellets>().enabled = true;

            //set this script to false
            GetComponent<PreBossCutScene>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            //Destroy(gameObject);
        }
        

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isActivated = true;

            //player unable to move
            player.GetComponent<PlayerControls>().enabled = false;
            player.GetComponent<ShootPellets>().enabled = false;
            rb.linearVelocity = Vector2.zero;
        }
    }
}
//Note When Reset, restart normalBGM and stop bossMusic
