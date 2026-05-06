using UnityEngine;

public class GreenAttackState : GreenState
{
    public GreenChaseState chaseState;

    private bool AttisInRangeOfPlayer;
    private Transform target;
    public Vector3 targetPos;
    private float distanceThreshold = 0.52f;

    [SerializeField] private Animator _animator;

    public override GreenState RunCurrentState()
    {
        if (AttisInRangeOfPlayer)
        {
            _animator.SetBool("isInRange", true);
            return this;
        }
        else
        {
            _animator.SetBool("isInRange", false);
            //Debug.Log("I have attacked!");
            return chaseState;
        }
    }

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        CheckIfChaseFinished();
        CreateTargetPos();
        //Debug.Log(Vector3.Distance(transform.parent.parent.position, targetPos));
        //Debug.Log(AttisInRangeOfPlayer);
    }

    private void CheckIfChaseFinished()
    {
        if (Vector3.Distance(transform.parent.parent.position, targetPos) < distanceThreshold)
        {
            AttisInRangeOfPlayer= true;
        }
        else
        {
            AttisInRangeOfPlayer = false;
        }
    }

    private void CreateTargetPos()
    {
        Transform grandParentTransform = transform.parent.parent;

        if (target.position.x >= grandParentTransform.position.x)
        {
            targetPos = new Vector3(target.position.x - 1, target.position.y, target.position.z);
        }
        else if (target.position.x <= grandParentTransform.position.x)
        {
            targetPos = new Vector3(target.position.x + 1, target.position.y, target.position.z);
        }
    }

    // private void Attack()
    // {
        
    // }
}
