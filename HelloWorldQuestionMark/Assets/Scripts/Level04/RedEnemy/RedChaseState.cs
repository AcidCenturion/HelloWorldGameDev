using Unity.VisualScripting;
using UnityEngine;

public class RedChaseState : RedState
{
    public bool ChaseisInRangeOfPlayer;
    public RedStateManager redStateManager;
    public RedAttackState attackState;

    private Transform player;
    public Vector3 playerPos;
    public float speed = 5.0f;
    public float angle;
    private Vector3 moveDirection;
    private float distanceThreshold = 0.3f;

    [SerializeField] private Animator _animator;


    public override RedState RunCurrentState()
    {
        if (ChaseisInRangeOfPlayer)
        {
            _animator.SetBool("isInRange", true);
            return attackState;
        }
        else
        {
            _animator.SetBool("isInRange", false);
            return this;
        }
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (!attackState.isAttacking)
        {
            FindTargetAngle();
        }
        
        CheckIfChaseFinished();
        //TestButton();
        //Debug.Log("chasing");
        //Debug.Log(ChaseisInRangeOfPlayer);
    }

    private void CheckIfChaseFinished()
    {
        if (Vector3.Distance(transform.parent.parent.position, playerPos) < distanceThreshold )
        {
            ChaseisInRangeOfPlayer = true;
            //Debug.Log("chase done");
        }
        else
        {
            ChaseisInRangeOfPlayer = false;
        }
    }

    private void FindTargetAngle()
    {
        Transform grandParentTransform = transform.parent.parent;

        if (player.position.x >= grandParentTransform.position.x)
        {
            playerPos = new Vector3(player.position.x - 4.0f, player.position.y, player.position.z);
            grandParentTransform.localScale = new Vector3(Mathf.Abs(grandParentTransform.localScale.x), grandParentTransform.localScale.y, grandParentTransform.localScale.z);
        }
        else if (player.position.x <= grandParentTransform.position.x)
        {
            playerPos = new Vector3(player.position.x + 4.0f, player.position.y, player.position.z);
            grandParentTransform.localScale = new Vector3(-1*Mathf.Abs(grandParentTransform.localScale.x), grandParentTransform.localScale.y, grandParentTransform.localScale.z);
        }

        if (player != null && !ChaseisInRangeOfPlayer)
        {
            if (player.position.y >= grandParentTransform.position.y)
            {
                if (playerPos.x >= grandParentTransform.position.x)
                {
                    angle = 45.0f;
                    float angleInRadians = angle * Mathf.Deg2Rad;
                    moveDirection = new Vector3(Mathf.Sin(angleInRadians), Mathf.Cos(angleInRadians)).normalized;
                    grandParentTransform.position += moveDirection * speed * Time.deltaTime;
                }
                else if (playerPos.x <= grandParentTransform.position.x)
                {
                    angle = 315.0f;
                    float angleInRadians = angle * Mathf.Deg2Rad;
                    moveDirection = new Vector3(Mathf.Sin(angleInRadians), Mathf.Cos(angleInRadians)).normalized;
                    grandParentTransform.position += moveDirection * speed * Time.deltaTime;
                }
            }
            else if (playerPos.y <= grandParentTransform.position.y)
            {
                if (playerPos.x >= grandParentTransform.position.x)
                {
                    angle = 135.0f;
                    float angleInRadians = angle * Mathf.Deg2Rad;
                    moveDirection = new Vector3(Mathf.Sin(angleInRadians), Mathf.Cos(angleInRadians)).normalized;
                    grandParentTransform.position += moveDirection * speed * Time.deltaTime;
                }
                else if (playerPos.x <= grandParentTransform.position.x)
                {
                    angle = 225.0f;
                    float angleInRadians = angle * Mathf.Deg2Rad;
                    moveDirection = new Vector3(Mathf.Sin(angleInRadians), Mathf.Cos(angleInRadians)).normalized;
                    grandParentTransform.position += moveDirection * speed * Time.deltaTime;
                }
            }

        }

    }


    // private void TestButton()
    // {
    //     if (Input.GetKeyDown(KeyCode.L))
    //     {
    //         isInRangeOfPlayer = true;
    //     }

    //     if (Input.GetKeyDown(KeyCode.P))
    //     {
    //         Debug.Log("gree pos" + transform.parent.parent.position);
    //         Debug.Log("player pos" + playerPos);
    //     }
    // }



}
