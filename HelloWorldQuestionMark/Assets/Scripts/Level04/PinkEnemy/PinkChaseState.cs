using Unity.VisualScripting;
using UnityEngine;

public class PinkChaseState : PinkState
{
    public bool pinkChaseReadyToAttack;
    public PinkStateManager pinkStateManager;
    public PinkAttackState pinkAttackState;

    public Rigidbody2D rb;
    private Transform target;
    public Vector3 targetPos;
    public float speed = 1.0f;

    
    [SerializeField] private Animator _animator;


    public override PinkState RunCurrentState()
    {
        if (pinkChaseReadyToAttack)
        {
            _animator.SetBool("isInRange", true);
            return pinkAttackState;
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


        //figure out whether to be on left or right wall (targetPos)

        Transform grandParentTransform = transform.parent.parent;
        Vector3 playerPos = Camera.main.WorldToViewportPoint(target.transform.position);
        Vector3 viewportLeft = new Vector3(0.05f, 0.5f, 0);
        Vector3 viewportRight = new Vector3(0.95f, 0.5f, 0);
        Vector3 worldpointLeft = Camera.main.ViewportToWorldPoint(viewportLeft);
        Vector3 worldpointRight = Camera.main.ViewportToWorldPoint(viewportRight);

        if (target.position.x >= grandParentTransform.position.x)  //player is to the right of me
        {
            targetPos = new Vector3(worldpointLeft.x, playerPos.y, Random.Range(0f, 1f));   //the random is so the pinks dont overlap w each other
            grandParentTransform.localScale = new Vector3(Mathf.Abs(grandParentTransform.localScale.x), grandParentTransform.localScale.y, grandParentTransform.localScale.z);
        }
        else if (target.position.x <= grandParentTransform.position.x) //player is to the left of me
        {
            targetPos = new Vector3(worldpointRight.x, playerPos.y, Random.Range(0f, 1f));
            grandParentTransform.localScale = new Vector3(-1*Mathf.Abs(grandParentTransform.localScale.x), grandParentTransform.localScale.y, grandParentTransform.localScale.z);
        }
    }

    void Update()
    {
        FollowPlayerY();
        CheckIfReadyToAttack();

    }

    private void CheckIfReadyToAttack()
    {
        if (Mathf.Approximately(transform.parent.parent.position.y, targetPos.y) && Mathf.Approximately(rb.linearVelocityX, 0))
        {
            pinkChaseReadyToAttack = true;
            //Debug.Log("chase done");
        }
        else
        {
            pinkChaseReadyToAttack = false;
        }
    }

    public void FollowPlayerY()
    {
        Transform grandParentTransform = transform.parent.parent;

        //Debug.Log(targetPos);
       

        if (target != null )
        {
            // if (target.position.y >= grandParentTransform.position.y)  //player is above me
            // {
            //     targetPos = new Vector3(targetPos.x, target.position.y, targetPos.z);
            // }
            // else if (targetPos.y <= grandParentTransform.position.y) //player is below me
            // {
            //     targetPos = new Vector3(targetPos.x, target.position.y, targetPos.z);
            // }

            targetPos = new Vector3(targetPos.x, target.position.y, targetPos.z);

        }
        float step = speed * Time.deltaTime;
        grandParentTransform.position = Vector3.MoveTowards(transform.position, targetPos, step);

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
