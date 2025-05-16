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
    bool isActivated = false;
    bool reachedLocation = false;
    public Transform destination; //use empty game object's Transform for this
    private float timer;
    public float pauseAfterReached = 1;
    public AudioSource voiceline;
    public AudioSource bossMusic;
    public GameObject NormalBGM;

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

        //Makes sure boss music is always paused
        bossMusic.Pause();

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
                voiceline.Play();
            }

        }

    }

    //After showcase, method called after dialogue is finished
    public void EnableBoss()
    {
        boss.GetComponent<BossBehavior>().enabled = true;
        NormalBGM.GetComponent<AudioSource>().Pause();
        bossMusic.Play();
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
        }
    }
}
//Note When Reset, restart normalBGM and stop bossMusic
