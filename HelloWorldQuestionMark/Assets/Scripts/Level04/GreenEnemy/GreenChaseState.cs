using Unity.VisualScripting;
using UnityEngine;

public class GreenChaseState : GreenState
{
    public bool ChaseisInRangeOfPlayer;
    public GreenStateManager greenStateManager;
    public GreenAttackState attackState;

    private Transform target;
    public Vector3 targetPos;
    public float speed = 5.0f;
    public float angle;
    private Vector3 moveDirection;
    private float distanceThreshold = 0.3f;

    [SerializeField] private Animator _animator;


    public override GreenState RunCurrentState()
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
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        FindTargetAngle();
        CheckIfChaseFinished();
        //TestButton();
        //Debug.Log("chasing");
        //Debug.Log(ChaseisInRangeOfPlayer);
    }

    private void CheckIfChaseFinished()
    {
        if (Vector3.Distance(transform.parent.parent.position, targetPos) < distanceThreshold)
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

        if (target.position.x >= grandParentTransform.position.x)
        {
            targetPos = new Vector3(target.position.x - 1.2f, target.position.y, target.position.z);
            grandParentTransform.localScale = new Vector3(Mathf.Abs(grandParentTransform.localScale.x), grandParentTransform.localScale.y, grandParentTransform.localScale.z);
        }
        else if (target.position.x <= grandParentTransform.position.x)
        {
            targetPos = new Vector3(target.position.x + 1.2f, target.position.y, target.position.z);
            grandParentTransform.localScale = new Vector3(-1*Mathf.Abs(grandParentTransform.localScale.x), grandParentTransform.localScale.y, grandParentTransform.localScale.z);
        }

        if (target != null && !ChaseisInRangeOfPlayer)
        {
            if (target.position.y >= grandParentTransform.position.y)
            {
                if (targetPos.x >= grandParentTransform.position.x)
                {
                    angle = 45.0f;
                    float angleInRadians = angle * Mathf.Deg2Rad;
                    moveDirection = new Vector3(Mathf.Sin(angleInRadians), Mathf.Cos(angleInRadians)).normalized;
                    grandParentTransform.position += moveDirection * speed * Time.deltaTime;
                }
                else if (targetPos.x <= grandParentTransform.position.x)
                {
                    angle = 315.0f;
                    float angleInRadians = angle * Mathf.Deg2Rad;
                    moveDirection = new Vector3(Mathf.Sin(angleInRadians), Mathf.Cos(angleInRadians)).normalized;
                    grandParentTransform.position += moveDirection * speed * Time.deltaTime;
                }
            }
            else if (targetPos.y <= grandParentTransform.position.y)
            {
                if (targetPos.x >= grandParentTransform.position.x)
                {
                    angle = 135.0f;
                    float angleInRadians = angle * Mathf.Deg2Rad;
                    moveDirection = new Vector3(Mathf.Sin(angleInRadians), Mathf.Cos(angleInRadians)).normalized;
                    grandParentTransform.position += moveDirection * speed * Time.deltaTime;
                }
                else if (targetPos.x <= grandParentTransform.position.x)
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
    //         Debug.Log("target pos" + targetPos);
    //     }
    // }



}
