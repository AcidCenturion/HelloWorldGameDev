using System.Collections;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private int lastNumber;
    private int newNumber = -1;
    public float ChaseSpeed = 5f;
    private Vector3 playerPos;

    private bool ChaseIsRangeOfPlayer = false;

    private Vector3 facingDirection;
    public float ChargeUpDuration = 1.5f;
    public float CooldownDuration = 2.5f;
    public float jumpforce = 20.0f;
    private bool isPunching = false;
    private bool isCharging = false;

    private bool isShockWaving = false;
    public GameObject shockwaveObject;
    private float WaitBeforeShockwave = 1.5f;
    private float WaitAfterShockwave = 2.5f;

    void Start()
    {
        player = GameObject.Find("L4Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            newNumber++;
            Debug.Log(newNumber);
        }
        PickAttack(newNumber);
        
    }

    int MakeRandomNumber()
    {
        int newNumber = Random.Range(0, 2);
        while (newNumber == lastNumber)
        {
            newNumber = Random.Range(0, 2);
        }
        lastNumber = newNumber;
        return newNumber;
    }

    void PickAttack(int newNumber)
    {
        switch (newNumber)
        {
            case 0:
            ChasePlayer();
            break;

            case 1:
            ChargePunch();
            break;

            case 2:
            ShockWave();
            break;
        }
    }

    void ChasePlayer()
    {

        if (transform.position.x < player.transform.position.x)  //if boss is to the left of the player
        {
            playerPos = new Vector3(player.transform.position.x - 2.0f, player.transform.position.y, player.transform.position.z);
        }
        else if (transform.position.x > player.transform.position.x)  //if boss is to the right of the player
        {
            playerPos = new Vector3(player.transform.position.x + 2.0f, player.transform.position.y, player.transform.position.z);
        }

        float step = ChaseSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, playerPos, step);

        if (Mathf.Approximately(Vector2.Distance(transform.position, playerPos), 0))
        {
            ChaseIsRangeOfPlayer = true;
        }

        //attack

    }

    void ChargePunch()
    {
        if (!isPunching)
        {
            if (transform.position.x < player.transform.position.x)  //if boss is to the left of the player
            {
                playerPos = new Vector3(player.transform.position.x - 3.0f, player.transform.position.y, player.transform.position.z);
                facingDirection = transform.right;
            }
            else if (transform.position.x > player.transform.position.x)  //if boss is to the right of the player
            {
                playerPos = new Vector3(player.transform.position.x + 3.0f, player.transform.position.y, player.transform.position.z);
                facingDirection = -transform.right;
            }
        
            float step = ChaseSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, playerPos, step);
        }
        

        if (Mathf.Approximately(Vector2.Distance(transform.position, playerPos), 0) && !isPunching)
        {   
            StartCoroutine(Punching());
        }
        
    }

    IEnumerator Punching()
    {
        isPunching = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(ChargeUpDuration);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.AddForce(facingDirection * jumpforce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(CooldownDuration);
        isPunching = false;
    }


    void ShockWave()
    {
        if (!isShockWaving)
        {
            if (transform.position.x < player.transform.position.x)  //if boss is to the left of the player
            {
                playerPos = new Vector3(player.transform.position.x - 6.0f, player.transform.position.y, player.transform.position.z);
                facingDirection = transform.right;
            }
            else if (transform.position.x > player.transform.position.x)  //if boss is to the right of the player
            {
                playerPos = new Vector3(player.transform.position.x + 6.0f, player.transform.position.y, player.transform.position.z);
                facingDirection = -transform.right;
            }
        
            float step = ChaseSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, playerPos, step);
        }

        if (Mathf.Approximately(Vector2.Distance(transform.position, playerPos), 0) && !isShockWaving)
        {
            StartCoroutine(Shockwaving());
        }
    }

    IEnumerator Shockwaving()
    {
        isShockWaving = true;
        yield return new WaitForSeconds(WaitBeforeShockwave);
        Vector3 temp = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        Instantiate(shockwaveObject, temp, transform.rotation);
        //Debug.Log("shockwaved");
        yield return new WaitForSeconds(WaitAfterShockwave);
        //Debug.Log("waitover");
        isShockWaving = false;
    }


}
