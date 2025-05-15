using UnityEngine;

public class PreBossCutScene : MonoBehaviour
{
    public GameObject boss;
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
    void Start()
    {
        rollPoints[0] = boss.transform;
        voiceline = GetComponent<AudioSource>();
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
            //GetComponent<PreBossCutScene>().enabled = false;
            Destroy(gameObject);
        }
        else
        {
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, rollPoints[rollPointIndex].position, Time.deltaTime * rollSpeed);
            boss.transform.Rotate(boss.transform.rotation.x, boss.transform.rotation.y,  Time.deltaTime * rollSpeed * rotationSpeed);

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
    }
  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
        Debug.Log("COLLIDED");
        isActivated = true;

        //boss is set active but unable to move
        boss.SetActive(true); //not working...
        boss.GetComponent<BossBehavior>().enabled = false;
        boss.GetComponent<BossBehavior>().sr = boss.GetComponent<SpriteRenderer>();
        //boss.GetComponent<BossBehavior>().enemyHealth = boss.GetComponent<EnemyHealth>();
        //boss.GetComponent<BossBehavior>().enemyHealth.Init(boss.GetComponent<BossBehavior>().health, boss.GetComponent<BossBehavior>().damage);

        //player unable to move
        player.GetComponent<PlayerControls>().enabled = false;
        player.GetComponent<ShootPellets>().enabled = false;
    }
  }
}
