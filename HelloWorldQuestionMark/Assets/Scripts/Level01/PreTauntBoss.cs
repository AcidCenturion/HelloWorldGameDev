using UnityEngine;

public class PreTauntBoss : MonoBehaviour
{
    //references
    public GameObject player;
    public GameObject bossStandIn;
    public GameObject Wall;
    public GameObject Ceiling;
    public AudioSource voiceline;
    public AudioSource bossEncounterMusic;
    public GameObject backgroundMusic;
    public bool pauseBackgroundMusic = true;

    //conditions
    private bool playerInTrigger = false;
    public bool EnterByFalling = false;
    public bool EnterByRolling = false;
    private bool reachedEndPosition = false;
    private bool reReachedStartPosition = false;
    private bool voicelineActivated = false;

    //locations
    private Vector3 endPosition;
    private Vector3 startPosition;
    public float rightOffset;
    public float upOffset;

    //Movement speed
    public float speed = 1f;
    public float rotationSpeed = 10f;

    void Start()
    {
        // ensures that only one of the entry options is selected
        if (!EnterByFalling && !EnterByRolling)
        {
            Debug.LogError("PreTauntBoss: Please check either EnterByFalling OR EnterByRolling to true");
        }
        else if (EnterByFalling && EnterByRolling)
        {
            Debug.LogError("PreTauntBoss: Please check either EnterByFalling OR EnterByRolling to true, not both");
        }

        //sets start and end position based off entry type
        endPosition = bossStandIn.transform.position;
        startPosition = new Vector3(
            EnterByRolling && !EnterByFalling ? endPosition.x + rightOffset : endPosition.x,
            EnterByFalling && !EnterByRolling ? endPosition.y + upOffset : endPosition.y,
            endPosition.z
        );

        bossStandIn.transform.position = startPosition;

        //ensures the boss isn't visible until player steps into trigger
        bossStandIn.SetActive(false);

        backgroundMusic = backgroundMusic ? backgroundMusic : GameObject.Find("BackgroundMusic");
    }

    // Update is called once per frame
    void Update()
    {

        if (reachedEndPosition)
        {
            //play audio file, once audio finished boss moves back, this gameObject is set inactive
            if (!voiceline.isPlaying && voicelineActivated)
            {
                if (EnterByFalling && !reReachedStartPosition)
                {
                    //falls back to start position
                    Fall(false);

                    //if boss returned to start position
                    if (bossStandIn.transform.position.y >= startPosition.y)
                    {
                        reReachedStartPosition = true;
                        Disable();
                    }
                }
                else if (EnterByRolling && !reReachedStartPosition)
                {
                    //rolls back to start position
                    Roll(false);

                    //if boss returned to start position
                    if (bossStandIn.transform.position.x >= startPosition.x)
                    {
                        reReachedStartPosition = true;
                        Disable();
                    }
                }

            }
            
        }
        else if (playerInTrigger)
        {
            if (EnterByFalling)
            {
                if (bossStandIn.transform.position.y > endPosition.y)
                {
                    Fall();
                }
                else
                {
                    reachedEndPosition = true;

                    // Update Music
                    if (pauseBackgroundMusic) {
                        // Pause background music
                        backgroundMusic.GetComponent<AudioSource>().Pause();
                        
                        // Play boss encounter music
                        if (bossEncounterMusic) bossEncounterMusic.Play();
                    }
                    
                    if (!voicelineActivated)
                    {
                        voicelineActivated = true;
                        voiceline.Play();
                    }
                }
            }
            else if (EnterByRolling)
            {
                if (bossStandIn.transform.position.x != endPosition.x)
                {
                    Roll();
                }
                else
                {
                    // Update Music
                    if (pauseBackgroundMusic) {
                        // Pause background music
                        backgroundMusic.GetComponent<AudioSource>().Pause();
                        
                        // Play boss encounter music
                        if (bossEncounterMusic) bossEncounterMusic.Play();
                    }

                    reachedEndPosition = true;

                    if (!voicelineActivated)
                    {
                        voicelineActivated = true;
                        voiceline.Play();
                    }
                    
                }
            }

        }
    }
    private void Disable()
    {
        Debug.Log("Disabled");

        if (pauseBackgroundMusic) {
            if (bossEncounterMusic) bossEncounterMusic.Stop();
            if (backgroundMusic) backgroundMusic.GetComponent<AudioSource>().Play();
        }
        bossStandIn.SetActive(false);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        playerInTrigger = false;
        player.GetComponent<PlayerControls>().enabled = true;
        player.GetComponent<ShootPellets>().enabled = true;
        Wall.SetActive(false);
        Ceiling.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == player || col.gameObject.CompareTag("Player"))
        {
            playerInTrigger = true;
            bossStandIn.SetActive(true);

            //disables player movement
            player.GetComponent<PlayerControls>().enabled = false;
            player.GetComponent<ShootPellets>().enabled = false;
        }
    }

    void Fall(bool isEntering = true)
    {
        if (isEntering)
        {
            bossStandIn.transform.position = Vector3.MoveTowards(bossStandIn.transform.position, endPosition, Time.deltaTime * speed);
        }
        else
        {
            bossStandIn.transform.position = Vector3.MoveTowards(bossStandIn.transform.position, startPosition, Time.deltaTime * speed);
        }

    }

    void Roll(bool isEntering = true)
    {
        if (isEntering)
        {
            bossStandIn.transform.position = Vector3.MoveTowards(bossStandIn.transform.position, endPosition, Time.deltaTime * speed);
            bossStandIn.transform.Rotate(transform.rotation.x, transform.rotation.y, Time.deltaTime * rotationSpeed);
        }
        else
        {
            bossStandIn.transform.position = Vector3.MoveTowards(bossStandIn.transform.position, startPosition, Time.deltaTime * speed);
            bossStandIn.transform.Rotate(transform.rotation.x, transform.rotation.y, -Time.deltaTime * rotationSpeed);
        }
       
    }
}
