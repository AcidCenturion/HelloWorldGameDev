using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    //Add parameter for how many circles the boss should do for a normal roll or a fast roll
    //Need to add enemy health script


    //Speed Variables
    [Header("Speed Variables")]
    public float speed = 15f;
    private float currentSpeed;
    public float maxRollSpeed = 400f;
    public float timeForFastSpeed = 2;
    public float jumpSpeed = 8f;
    public float swaySpeed = 4f;
    public float swayAmount = 5f;
    Rigidbody2D rb;

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
    
    //Attacking timeframe variables
    [Header("Attack timeframe Variables")]
    private bool isAttacking = false;
    private int currentAttack;
    private float timeSinceAttack = 0;
    public float attackCooldown = 3f;
    
    [Header("Player Variables")]
    public GameObject player;
    Vector2 playerPosition;
    bool reachedPlayerPosition = false;

    [Header("Boxcast Variables")]
    public LayerMask ground;
    public Vector2 groundCheck;
    public float groundDistance = 2f;

    //Miscellaneous
    private EnemyHealth enemyHealth;
    private Vector3 lastPosition;
    public int health = 5;
    


    enum Attacks
    {
        Roll, Jump, FastRoll
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyHealth = new EnemyHealth(health);
        currentSpeed = speed;
        ground = LayerMask.GetMask("Default");
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(rollPoints.Length);
        ChooseMoves();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            switch (currentAttack)
            {
                case (int) Attacks.Roll:

                    currentSpeed = speed;
                    Roll(MaxNumOfRolls);
                    break;
                case (int) Attacks.Jump:
                    if (!reachedPlayerPosition)
                    {
                        //First jumps to players position
                        JumpTo(playerPosition);
                        if ((Vector2)transform.position == playerPosition)
                        {
                            //enables gravity
                            rb.bodyType = RigidbodyType2D.Dynamic;

                            reachedPlayerPosition = true;
                        }

                        return;
                    }
                    else
                    {
                        
                        //check if its grounded, if so then set isAttacking to false and then chooseMoves()
                        if (Physics2D.BoxCast(transform.position, groundCheck, 0, -transform.up, groundDistance, ground))
                        {
                            isAttacking = false;
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
                        currentSpeed += Time.deltaTime * timeForFastSpeed;
                    }
                    Roll(MaxNumOfFastRolls);
                    break;
            }
        }
        else if (timeSinceAttack > attackCooldown)
        {
            ChooseMoves();
            isAttacking = true;
        }
        else
        {
            timeSinceAttack += Time.deltaTime;
            
            if (SinAmount() < 0)
            {
                //faces left
                transform.rotation = Quaternion.Euler(0,0,0);
            }
            else    
            {
                //faces right
                transform.rotation = Quaternion.Euler(0,180,0);
            }

            //Boss moves left and right
            transform.position = new Vector3(lastPosition.x + SinAmount(), lastPosition.y, lastPosition.z);

        }

        
    }

    //Visualize GroundChecker (Optional)
//   void OnDrawGizmos()
//   {
//     Gizmos.DrawCube(transform.position - transform.up * groundDistance, groundCheck);
//   }

  private void RollInCircle(float maxAmount) {
        //thinking of adding when on the last roll, randomly lunges towards player

        if (rollPointsIndex <= rollPoints.Length && numOfRolls < maxAmount)
        {
            //boss rolls to next point
            transform.Rotate(transform.rotation.x, transform.rotation.y,  -Time.deltaTime * currentSpeed * rotationSpeed);
            transform.position = Vector2.MoveTowards(transform.position, rollPoints[rollPointsIndex].transform.position, Time.deltaTime * currentSpeed);

            //if new point reached, set index to next point
            if (this.transform.position == rollPoints[rollPointsIndex].transform.position)
            {
                rollPointsIndex++;

                //boss did a whole roll
                if (rollPointsIndex >= rollPoints.Length)
                {
                    rollPointsIndex = 0;
                    numOfRolls++;
                }

            }

            return;
        }

        //resets variables
        isAttacking = false;
        numOfRolls = 0;
        rollPointsIndex = 0;
        transform.rotation = Quaternion.Euler(0,0,0);
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
                }
            }
            else
            {
                RollInCircle(maxAmount);
            }
        }
    }

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
        //stores lasts instance of boss' position
        lastPosition = transform.position;

        //Randomly chooses an attack
        currentAttack = Random.Range(0,3);

        //resets variables
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
            Debug.Log(playerPosition);
        }
        else if (currentAttack == (int)Attacks.FastRoll)
        {
            reachedAboveFirstPoint = false;
            reachedFirstPoint = false;
            rollPointsIndex = 0;
        }

        //Resets timer
        timeSinceAttack = 0;

    }
}
