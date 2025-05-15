using Unity.VisualScripting;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{

    //Speed Variables
    [Header("Speed Variables")]
    public float speed = 15f;
    private float currentSpeed;
    public float maxRollSpeed = 400f;
    public float rateForFastSpeed = 2;
    public float jumpSpeed = 8f;
    public float swaySpeed = 4f;
    public float swayAmount = 5f;
    Rigidbody2D rb;

    public Transform bossEnemy;
    //Roll Variables
    [Header("Roll Variables")]
    public Transform[] rollPoints;
    private int rollPointsIndex = 0;
    public int MaxNumOfRolls = 3;
    public int MaxNumOfFastRolls = 5;
    private int numOfRolls = 0;
    public float rotationSpeed = 25;
    private bool reachedAboveFirstPoint = false;
    private bool reachedFirstPoint = false;
    public float jumpHeightBeforeRolling = 7f;
    public float swayRotation = 90;
    
    //Attacking timeframe variables
    [Header("Attack timeframe Variables")]
    private bool isAttacking = false;
    private int currentAttack;
    private float timeSinceAttack = 0;
    public float attackCooldown = 3f;
    public float timeToLook = 2f;
    
    [Header("Player Variables")]
    public GameObject player;
    Vector2 playerPosition;
    bool reachedPlayerPosition = false;

    [Header("Boxcast Variables")]
    public LayerMask ground;
    public Vector2 groundCheck;
    public float groundDistance = 2f;

    //Miscellaneous
    public EnemyHealth enemyHealth;
    public SpriteRenderer sr;
    private Vector3 lastPosition;
    private Quaternion lastRotation;
    public int health = 10;
    public int damage = 1;
    private CircleCollider2D hitbox;
    public float hitboxCooldown = 1;
    private float hitboxTimer = 0;
    AudioSource rollSound;
    public GameObject cutsceneManager;

    enum Attacks
    {
        Roll, Jump, FastRoll
    }

    void OnEnable()
    {
        //Initializes the EnemyHealth and damage
        enemyHealth = bossEnemy.GetComponent<EnemyHealth>();
        enemyHealth.Init(health, damage);
        hitbox = GetComponent<CircleCollider2D>();
        ground = LayerMask.GetMask("Floor");
        rb = GetComponent<Rigidbody2D>();
        ChooseMoves();
        sr = GetComponent<SpriteRenderer>();
        rollSound = GetComponent<AudioSource>();

    }
  void OnDisable()
  {
        numOfRolls = 0;
  }
  void Start()
    {
        // //Initializes the EnemyHealth and damage
        // enemyHealth = bossEnemy.GetComponent<EnemyHealth>();
        // enemyHealth.Init(health, damage);

        currentSpeed = speed;
        // hitbox = GetComponent<CircleCollider2D>();
        // ground = LayerMask.GetMask("Floor");
        // rb = GetComponent<Rigidbody2D>();
        // ChooseMoves();
        // sr = GetComponent<SpriteRenderer>();

        // //change later when more than one sound is needed
        // rollSound = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        hitboxTimer += Time.deltaTime;

        if (isAttacking)
        {
            switch (currentAttack)
            {
                case (int) Attacks.Roll:

                    //rolls in default speed
                    currentSpeed = speed;
                    Roll(MaxNumOfRolls);
                    break;
                case (int) Attacks.Jump:
                    //boss faces left/right depending on player x coord
                    if (transform.position.x > playerPosition.x)
                    {
                        sr.flipX = false;
                    }
                    else
                    {
                        sr.flipX = true;
                    }

                    //give player time to change location
                    if (timeSinceAttack < attackCooldown + timeToLook)
                    {
                        timeSinceAttack += Time.deltaTime;
                        return;
                    }

                    //if boss hasn't reached jump location
                    if (!reachedPlayerPosition)
                    {
                        //First jumps to players position
                        JumpTo(playerPosition);

                        if ((Vector2)transform.position == playerPosition)
                        {
                            //enables gravity
                            //rb.bodyType = RigidbodyType2D.Dynamic;
                            reachedPlayerPosition = true;
                            transform.rotation = Quaternion.identity;
                        }

                        return;
                    }
                    else
                    {
                        //"Falls" to the ground
                        transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * jumpSpeed, transform.position.z);

                        //check if its grounded, if so then set isAttacking to false and then chooseMoves()
                        if (Physics2D.BoxCast(transform.position, groundCheck, 0, -transform.up, groundDistance, ground))
                        {
                            //resets boss properties
                            reachedPlayerPosition = false;
                            rb.bodyType = RigidbodyType2D.Kinematic;
                            ChooseMoves();
                        }
                        
                    }
                    break;

                case (int) Attacks.FastRoll:

                    //Speed builds up over time
                    if (currentSpeed < maxRollSpeed)
                    {
                        currentSpeed += Time.deltaTime * rateForFastSpeed;
                    }

                    Roll(MaxNumOfFastRolls);
                    break;
            }
        }
        else if (timeSinceAttack > attackCooldown)
        {

            //adding after showcase (cus im rlly busy...) if statement once boss is fully upright

            isAttacking = true;

            //resets rotation from swaying
            transform.rotation = Quaternion.identity;
        }
        else
        {
            //boss faces left/right depending on player x coord
            if (transform.position.x > playerPosition.x)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            
            }
            timeSinceAttack += Time.deltaTime;

            //Boss sways left and right
            transform.position = new Vector3(lastPosition.x + SinAmount(), lastPosition.y, lastPosition.z);
            transform.rotation = Quaternion.Euler(lastRotation.x, lastRotation.y, swayRotation * -SinAmount());

        }

        
    }

  //Visualize GroundChecker (Optional)
  void OnDrawGizmos()
  {
    Gizmos.DrawCube(transform.position - transform.up * groundDistance, groundCheck);
  }

  private void RollInCircle(float maxAmount) {

        //if path hasnt finished & theres still more rounds to complete
        if (rollPointsIndex <= rollPoints.Length && numOfRolls < maxAmount)
        {
            //boss rolls/spins to next point
            transform.Rotate(transform.rotation.x, transform.rotation.y,  -Time.deltaTime * currentSpeed * rotationSpeed);
            transform.position = Vector2.MoveTowards(transform.position, rollPoints[rollPointsIndex].transform.position, Time.deltaTime * currentSpeed);

            //if new point reached, set index to next point
            if (this.transform.position == rollPoints[rollPointsIndex].transform.position)
            {
                rollPointsIndex++;

                //boss completed a round
                if (rollPointsIndex >= rollPoints.Length)
                {
                    rollPointsIndex = 0;
                    numOfRolls++;
                }

            }

            return;
        }

        //resets variables
        rollSound.Pause();
        numOfRolls = 0;
        currentSpeed = speed;
        ChooseMoves();
    }

    private void Roll(float maxAmount)
    {
        //Jumps to area above rolling path's first location
        if ((Vector2)transform.position != new Vector2(rollPoints[0].position.x, rollPoints[0].position.y + jumpHeightBeforeRolling ) && !reachedAboveFirstPoint)
        {
            JumpTo(new Vector2(rollPoints[0].position.x, rollPoints[0].position.y + jumpHeightBeforeRolling ));
            return;
        }
        else
        {
            reachedAboveFirstPoint = true;
        }

        if (reachedAboveFirstPoint)
        {
            //"Falls" to the first point to begin spinning
            if (!reachedFirstPoint)
            {
                transform.position = Vector2.MoveTowards(transform.position, rollPoints[0].position, Time.deltaTime * speed / 1.5f);
                if (transform.position == rollPoints[0].position)
                {
                    reachedFirstPoint = true;
                    rollSound.Play();
                }
            }
            else
            {
                RollInCircle(maxAmount);
            }
        }
    }

  
    //---helper functions---
    private void JumpTo(Vector2 position) //jumps to player or middle of stage
    {

        //if object hasnt yet reached playerPosition
        if ((Vector2)transform.position != playerPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, position, jumpSpeed * Time.deltaTime);
        }
        
    }
    private float SinAmount()
    {
        return Mathf.Sin(Time.time * swaySpeed) * swayAmount;
    }
    
    
    private void ChooseMoves()
    {
        hitbox.enabled = true;

        //stores lasts instance of boss' position
        lastPosition = transform.position;
        lastRotation = transform.rotation;

        //Randomly chooses an attack
        currentAttack = Random.Range(0,3);

        //resets variables
        transform.rotation = Quaternion.identity;
        if (currentAttack == (int)Attacks.Roll)
        {
            reachedAboveFirstPoint = false;
            reachedFirstPoint = false;
            rollPointsIndex = 0;
        }
        else if (currentAttack == (int)Attacks.Jump)
        {
            playerPosition = player.transform.position;
            reachedPlayerPosition = false;
        }
        else if (currentAttack == (int)Attacks.FastRoll)
        {
            reachedAboveFirstPoint = false;
            reachedFirstPoint = false;
            rollPointsIndex = 0;
        }

        //Resets timer
        timeSinceAttack = 0;
        isAttacking = false;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //if boss touches player
        if (collision.CompareTag("Player"))
        {
            if (hitboxTimer < hitboxCooldown)
            {
                return;
            }

            //hitbox disabled until next attack
            hitbox.enabled = false;
            hitboxTimer = 0;

        }
    }
}
