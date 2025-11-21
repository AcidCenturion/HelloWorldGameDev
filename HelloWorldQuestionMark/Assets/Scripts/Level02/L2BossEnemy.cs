using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class L2BossEnemy : Enemy 
{


    private const int V = 0;
    private Rigidbody2D rb;
    private Vector2 moveDirectionStart;
    private Vector2 moveDirection;
    private bool hasHitX = false;
    private bool hasHitY = false;
    private int currentAttack = 3;

    private SpriteRenderer spriteRend;

    public List<GameObject> waypoints;
    int index = 0;

    int startingIndex;

    private float distToPlayer;

    public GameObject player;

    public int numOfBounces = 5;

    private int bouncesDone;

    public float chaseDuration = 4f;

    public float chaseSpeed = 1.5f;
    private float timer = 0f;

    private float point0Dist;

    private float point1Dist;

    private float point2Dist;

    private float point3Dist;

    private float shortestPointDist = 100;

    private bool didFullLoop = false;
    private float[] pointDistances = new float[4];

    private int lastAttack = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        ChooseMoves();
    }

    // Update is called once per frame
    void Update()
    {

        point0Dist = Vector2.Distance(gameObject.transform.position, waypoints[0].transform.position);
        pointDistances[0] = point0Dist;

        point1Dist = Vector2.Distance(gameObject.transform.position, waypoints[1].transform.position);
        pointDistances[1] = point1Dist;

        point2Dist = Vector2.Distance(gameObject.transform.position, waypoints[2].transform.position);
        pointDistances[2] = point2Dist;

        point3Dist = Vector2.Distance(gameObject.transform.position, waypoints[3].transform.position);
        pointDistances[3] = point3Dist;


           

        switch ((int)currentAttack)
        {

            //Bouncing Attacks
            case 0:

             

                //Flip y-direction if hits north or south
                if (hasHitY == true)
                {
                    bouncesDone++;
                    moveDirection.y = moveDirection.y * -1;
                    rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y);
                    hasHitY = false;

                }

                //Flip x-direction if hits east or west
                if (hasHitX == true)
                {
                    bouncesDone++;
                    moveDirection.x = moveDirection.x * -1;
                    rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y);
                    hasHitX = false;
                }

                //Debug.Log(moveDirection.x + " " + moveDirection.y);
                if (bouncesDone >= numOfBounces)
                {

                    moveDirection.x = 0;
                    moveDirection.y = 0;
                    rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y);
                    ChooseMoves();
                }

                rb.linearVelocityY = moveDirection.x;
                rb.linearVelocityY = moveDirection.y;  

                break;


            //Track attack
            case 1:


                Vector2 destination = waypoints[index].transform.position;
                Vector2 newPos = Vector2.MoveTowards(transform.position, destination, enemySpeed * Time.deltaTime);
                transform.position = newPos;

                float distance = Vector2.Distance(transform.position, destination);

                if (distance <= 0.05f)
                {

                    if (didFullLoop)
                    {
                        ChooseMoves();
                    }
                    else if (index < waypoints.Count - 1)
                    {
                        index++;
                    }
                    else
                    {
                        index = 0;
                    }

                    if (index == startingIndex)
                    {
                        didFullLoop = true;
                    }
                }

                break;


            //Chase attack
            case 2:


                timer += Time.deltaTime;

                distToPlayer = Vector2.Distance(transform.position, player.transform.position);

                if (distToPlayer < 7.5)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
                }
                //Debug.Log(distToPlayer);

                if (timer >= chaseDuration)
                {

                    timer = 0f;
                    ChooseMoves();
                }

                break;

            default:
                break;

        }

    }


    //chooses move boss does, rerolls duplicate attacks
    private void ChooseMoves()
    {
        lastAttack = currentAttack;
        bouncesDone = 0;
        didFullLoop = false;
        shortestPointDist = 100;

        while (true)
        {
            currentAttack = Random.Range(0, 3);
            if (currentAttack != lastAttack)
            {
                break;
            }
        }

        //Debug.Log((int)currentAttack);
        
        if (currentAttack == 0)
        {
            Bounce();
        }
        if (currentAttack == 1)
        {
            //gets closest point and sets it as the starting index
            for (int i = 0; i < pointDistances.Length; i++)
            {
                float tempDist = pointDistances[i];

                if (tempDist < shortestPointDist)
                {
                    shortestPointDist = tempDist;
                    index = i;
                    //Debug.Log("index chosen: " + index);

                    startingIndex = index;
                }
            }
        }

    }

    //starts bouncing
    private void Bounce()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirectionStart = new Vector2(enemySpeed, enemySpeed);
        rb.linearVelocity = moveDirectionStart;

        moveDirection.x = rb.linearVelocity.x;
        moveDirection.y = rb.linearVelocity.y;
    }

    //Tells which direction the Green collides a wall on
    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector2 collisionDirection = contact.normal;

            // Debug.Log($"Collision occurred at point: {contact.point}");
            // Debug.Log($"Collision normal (direction from which collision occurred): {collisionDirection}");

            if (collisionDirection.x < 0.5 && collisionDirection.y < 0.05f && collisionDirection.y > -0.05f)
            {
                //Debug.Log("hit east wall");
                hasHitX = true;

            }
            else if (collisionDirection.x > 0.5 && collisionDirection.y < 0.05f && collisionDirection.y > -0.05f)
            {
                //Debug.Log("hit west wall");
                hasHitX = true;

            }
            else if (collisionDirection.y < 0.5 && collisionDirection.x < 0.05f && collisionDirection.x > -0.05f)
            {
                //Debug.Log("hit north wall");
                hasHitY = true;

            }
            else if (collisionDirection.y > 0.5 && collisionDirection.x < 0.05f && collisionDirection.x > -0.05f)
            {
                //Debug.Log("hit south wall");
                hasHitY = true;
            }

        }
    }


    



}






