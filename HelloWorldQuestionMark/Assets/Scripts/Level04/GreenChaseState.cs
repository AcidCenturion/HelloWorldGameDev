using Unity.VisualScripting;
using UnityEngine;

public class GreenChaseState : GreenState
{
    public bool isInRangeOfPlayer;
    public GreenStateManager greenStateManager;
    public GreenAttackState attackState;

    public Transform target;
    public float speed = 5.0f;
    public float angle;
    private Vector3 moveDirection;


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
        
        

    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            isInRangeOfPlayer = true;
        }
        else
        {
            isInRangeOfPlayer = false;
        }
    }

    private void FindTargetAngle()
    {
        Transform grandParentTransform = transform.parent.parent;

        if (target != null)
        {
            if (target.position.y > grandParentTransform.position.y)
            {
                if (target.position.x > grandParentTransform.position.x)
                {
                    angle = 45.0f;
                    float angleInRadians = angle * Mathf.Deg2Rad;
                    moveDirection = new Vector3(Mathf.Sin(angleInRadians), Mathf.Cos(angleInRadians)).normalized;
                    grandParentTransform.position += moveDirection * speed * Time.deltaTime;
                }
                else if (target.position.x < grandParentTransform.position.x)
                {
                    angle = 315.0f;
                    float angleInRadians = angle * Mathf.Deg2Rad;
                    moveDirection = new Vector3(Mathf.Sin(angleInRadians), Mathf.Cos(angleInRadians)).normalized;
                    grandParentTransform.position += moveDirection * speed * Time.deltaTime;
                }
            }
            else if (target.position.y < grandParentTransform.position.y)
            {
                if (target.position.x > grandParentTransform.position.x)
                {
                    angle = 135.0f;
                    float angleInRadians = angle * Mathf.Deg2Rad;
                    moveDirection = new Vector3(Mathf.Sin(angleInRadians), Mathf.Cos(angleInRadians)).normalized;
                    grandParentTransform.position += moveDirection * speed * Time.deltaTime;
                }
                else if (target.position.x < grandParentTransform.position.x)
                {
                    angle = 225.0f;
                    float angleInRadians = angle * Mathf.Deg2Rad;
                    moveDirection = new Vector3(Mathf.Sin(angleInRadians), Mathf.Cos(angleInRadians)).normalized;
                    grandParentTransform.position += moveDirection * speed * Time.deltaTime;
                }
            }

            
            //float step = speed * Time.deltaTime;
            //grandParentTransform.position = Vector3.MoveTowards(grandParentTransform.position, target.position, step);
        }
    }



}
