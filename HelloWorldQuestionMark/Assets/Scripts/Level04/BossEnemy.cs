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

    private Vector3 facingDirection;
    [SerializeField] private float ChargeUpDuration = 1.5f;
    [SerializeField] private float CooldownDuration = 2.5f;
    [SerializeField] private float jumpforce = 20.0f;
    private bool isPunching = false;
    private bool isChargePunching = false;

    private bool isShockWaving = false;
    public GameObject shockwaveObject;
    [SerializeField] private float WaitBeforeShockwave = 1.5f;
    [SerializeField] private float WaitAfterShockwave = 2.5f;

    [SerializeField] private Animator _animator;

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
            _animator.SetInteger("WhichAttack", 0);
            ChasePlayer();
            break;

            case 1:
            _animator.SetInteger("WhichAttack", 1);
            ChargePunch();
            break;

            case 2:
            _animator.SetInteger("WhichAttack", 2);
            ShockWave();
            break;
        }
    }

    void ChasePlayer()
    {

        if (transform.position.x < player.transform.position.x)  //if boss is to the left of the player
        {
            playerPos = new Vector3(player.transform.position.x - 2.0f, player.transform.position.y, player.transform.position.z);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (transform.position.x > player.transform.position.x)  //if boss is to the right of the player
        {
            playerPos = new Vector3(player.transform.position.x + 2.0f, player.transform.position.y, player.transform.position.z);
            transform.localScale = new Vector3(-1, 1, 1);
        }

        float step = ChaseSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, playerPos, step);

        if (Mathf.Approximately(Vector2.Distance(transform.position, playerPos), 0) && !isPunching)
        {
            _animator.SetBool("isInRange", true);
            StartCoroutine(PunchAttack());
        }

    }

    IEnumerator PunchAttack()
    {
        isPunching = true;
        //punch goes here ?
        yield return new WaitForSeconds(2);

        if (!Mathf.Approximately(Vector2.Distance(transform.position, playerPos), 0))
        {
            _animator.SetBool("isInRange", false);
            isPunching = false;
        }

        isPunching = false;
    }

    void ChargePunch()
    {
        if (!isChargePunching)
        {
            if (transform.position.x < player.transform.position.x)  //if boss is to the left of the player
            {
                playerPos = new Vector3(player.transform.position.x - 4.0f, player.transform.position.y, player.transform.position.z);
                transform.localScale = new Vector3(1, 1, 1);
                facingDirection = transform.right;
            }
            else if (transform.position.x > player.transform.position.x)  //if boss is to the right of the player
            {
                playerPos = new Vector3(player.transform.position.x + 4.0f, player.transform.position.y, player.transform.position.z);
                transform.localScale = new Vector3(-1, 1, 1);
                facingDirection = -transform.right;
            }
        
            float step = ChaseSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, playerPos, step);
        }
        

        if (Mathf.Approximately(Vector2.Distance(transform.position, playerPos), 0) && !isChargePunching)
        {   
            _animator.SetBool("isInRange", true);
            StartCoroutine(Punching());
        }
        
    }

    IEnumerator Punching()
    {
        isChargePunching = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(ChargeUpDuration);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.AddForce(facingDirection * jumpforce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(CooldownDuration);

        if (!Mathf.Approximately(Vector2.Distance(transform.position, playerPos), 0))
        {
            _animator.SetBool("isInRange", false);
            isChargePunching = false;
        }

        isChargePunching = false;
    }


    void ShockWave()
    {
        if (!isShockWaving)
        {
            if (transform.position.x < player.transform.position.x)  //if boss is to the left of the player
            {
                playerPos = new Vector3(player.transform.position.x - 6.0f, player.transform.position.y, player.transform.position.z);
                transform.localScale = new Vector3(1, 1, 1);
                facingDirection = transform.right;
            }
            else if (transform.position.x > player.transform.position.x)  //if boss is to the right of the player
            {
                playerPos = new Vector3(player.transform.position.x + 6.0f, player.transform.position.y, player.transform.position.z);
                transform.localScale = new Vector3(-1, 1, 1);
                facingDirection = -transform.right;
            }
        
            float step = ChaseSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, playerPos, step);
        }

        if (Mathf.Approximately(Vector2.Distance(transform.position, playerPos), 0) && !isShockWaving)
        {
            _animator.SetBool("isInRange", true);
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
        
        if (!Mathf.Approximately(Vector2.Distance(transform.position, playerPos), 0))
        {
            _animator.SetBool("isInRange", false);
            isShockWaving = false;
        }

        isShockWaving = false;
    }


}
