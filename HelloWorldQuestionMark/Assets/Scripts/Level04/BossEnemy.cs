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
        if (transform.position.x < player.transform.position.x)  //if boss is to the left of the player
        {
            playerPos = new Vector3(player.transform.position.x - 3.0f, player.transform.position.y, player.transform.position.z);
        }
        else if (transform.position.x > player.transform.position.x)  //if boss is to the right of the player
        {
            playerPos = new Vector3(player.transform.position.x + 3.0f, player.transform.position.y, player.transform.position.z);
        }

        float step = ChaseSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, playerPos, step);


        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        // yield return new WaitForSeconds(AttackChargeUpDduration); 
        
        // rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        // rb.AddForce(facingDirection * jumpforce, ForceMode2D.Impulse);
        // yield return new WaitForSeconds(AttackCooldownTime);
    }

    void ShockWave()
    {
        
    }
}
