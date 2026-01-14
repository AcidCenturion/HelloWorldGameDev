using UnityEngine;

public class PreBossCutScene : MonoBehaviour
{
    public GameObject boss;
    public Transform bossSpawn;
    public GameObject player;
    public float rollSpeed;
    public float rotationSpeed = 25f;
    public Transform[] rollPoints;
    int rollPointIndex = 0;
    [Header("Temp Variables")]
    public bool isActivated = false;
    bool reachedLocation = false;
    public Transform destination; //use empty game object's Transform for this
    private float timer;
    public float pauseAfterReached = 1;
    public AudioSource voiceline;
    public bool BossCutsceneFinished = false;
    public AudioSource bossMusic;
    public AudioSource bossEncounterMusic;
    public GameObject NormalBGM;
    public GameObject cutsceneText;
    public EnemyHealth enemyHealth;

    void OnEnable()
    {
        Debug.Log("Enabled");
        GetComponent<BoxCollider2D>().enabled = true;
        boss.SetActive(false);
        boss.transform.position = bossSpawn.position;
        rollPoints[0] = boss.transform;
        voiceline = GetComponent<AudioSource>();
        isActivated = false;
        reachedLocation = false;
        rollPointIndex = 0;

        AudioSource[] audiolist = GetComponents<AudioSource>();
        voiceline = audiolist[0];
        bossMusic = audiolist[1];
        bossEncounterMusic = audiolist[2];

        //Makes sure boss music is always paused
        bossMusic.Pause();

        cutsceneText.SetActive(false);

    }

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
            EnableBoss();

            //set this script to false
            GetComponent<PreBossCutScene>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            //Destroy(gameObject);

        }
        else
        {
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, rollPoints[rollPointIndex].position, Time.deltaTime * rollSpeed);
            boss.transform.Rotate(boss.transform.rotation.x, boss.transform.rotation.y, Time.deltaTime * rollSpeed * rotationSpeed);

            if (boss.transform.position == rollPoints[rollPointIndex].position)
            {
                rollPointIndex++;
            }

            if (rollPointIndex >= rollPoints.Length)
            {
                reachedLocation = true;
                boss.transform.rotation = Quaternion.identity;

                // Pause background music
                NormalBGM.GetComponent<AudioSource>().Pause();

                // Play boss voiceline
                voiceline.Play();
                
                // Play boss intro music
                bossEncounterMusic.Play();

            }

        }

    }

    //After showcase, method called after dialogue is finished
    public void EnableBoss()
    {
        boss.GetComponent<BossBehavior>().enabled = true;
        bossEncounterMusic.Stop();
        bossMusic.Play();
        
        cutsceneText.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isActivated = true;

            //boss is set active but unable to move
            boss.SetActive(true);
            boss.GetComponent<BossBehavior>().enabled = false;

            //player unable to move
            player.GetComponent<PlayerControls>().enabled = false;
            player.GetComponent<ShootPellets>().enabled = false;
            player.GetComponent<Rigidbody2D>().linearVelocityX = 0;
            player.GetComponent<Rigidbody2D>().linearVelocityY = 0;

            //cutscene text appears
            cutsceneText.SetActive(true);
        }
    }

    void BossDies()
    {
        bossMusic.Stop();
        NormalBGM.GetComponent<AudioSource>().Pause();
    }

}
//Note When Reset, restart normalBGM and stop bossMusic
