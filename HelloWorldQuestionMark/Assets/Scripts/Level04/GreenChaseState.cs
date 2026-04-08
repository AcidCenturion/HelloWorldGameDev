using Unity.VisualScripting;
using UnityEngine;

public class GreenChaseState : GreenState
{
    public bool isInRangeOfPlayer;
    public GreenStateManager greenStateManager;
    public GreenAttackState attackState;

    public Transform target;
    public Vector3 targetPos;
    public float speed = 5.0f;
    public float angle;
    private Vector3 moveDirection;
    private float distanceThreshold = 0.3f;


    public override GreenState RunCurrentState()
    {
        if (isInRangeOfPlayer)
        {
            return attackState;
        }
        else
        {
            return this;
        }
    }

    void Update()
    {
        FindTargetAngle();
        CheckIfChaseFinished();
        //TestButton();        

    }

    private void CheckIfChaseFinished()
    {
        if (Vector3.Distance(transform.parent.parent.position, targetPos) <= distanceThreshold)
        {
            isInRangeOfPlayer = true;
            Debug.Log("chase done");
        }
        else
        {
            isInRangeOfPlayer = false;
        }
    }

    private void FindTargetAngle()
    {
        Transform grandParentTransform = transform.parent.parent;

        if (target.position.x >= grandParentTransform.position.x)
        {
            targetPos = new Vector3(target.position.x - 2, target.position.y, target.position.z);
        }
        else if (target.position.x <= grandParentTransform.position.x)
        {
            targetPos = new Vector3(target.position.x + 2, target.position.y, target.position.z);
        }

        if (target != null && !isInRangeOfPlayer)
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
