using UnityEngine;
using System.Collections;

public class RedAttackState : RedState
{
    public RedChaseState chaseState;

    private bool AttisInRangeOfPlayer;
    private Transform player;
    public Vector3 playerPos;
    public Vector3 chargeTargetPos;
    private float distanceThreshold = 0.3f;
    [SerializeField] private float speed = 5.0f;
    public bool isAttacking = false;
    private Vector3 facingDirection;
    [SerializeField] private float AttackChargeUpDduration = 2.0f;
    [SerializeField] private float AttackCooldownTime = 3.0f;

    public Rigidbody2D rb;

    [SerializeField] private Animator _animator;

    public override RedState RunCurrentState()
    {
        if (AttisInRangeOfPlayer)
        {
            //_animator.SetBool("isInRange", true);
            return this;
        }
        else
        {
            //_animator.SetBool("isInRange", false);
            //Debug.Log("I have attacked!");
            return chaseState;
        }
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        CheckIfChaseFinished();
        CreatePlayerPos();
        //Debug.Log(Vector3.Distance(transform.parent.parent.position, playerPos));
        //Debug.Log(AttisInRangeOfPlayer);
        // if (!AttisInRangeOfPlayer && isAttacking)
        // {
        //     StopCoroutine(Attack());
        // }

        if (AttisInRangeOfPlayer && !isAttacking)
        {
            StartCoroutine(Attack(transform.parent.parent.position, chargeTargetPos, speed));
        }

        

    }

    private void CheckIfChaseFinished()
    {
        if (Vector3.Distance(transform.parent.parent.position, playerPos) < distanceThreshold )
        {
            AttisInRangeOfPlayer= true;
        }
        else
        {
            AttisInRangeOfPlayer = false;
        }
    }

    private void CreatePlayerPos()
    {
        Transform grandParentTransform = transform.parent.parent;

        if (player.position.x >= grandParentTransform.position.x)
        {
            playerPos = new Vector3(player.position.x - 4.0f, player.position.y, player.position.z);
            chargeTargetPos = new Vector3(player.position.x + 2.0f, player.position.y, player.position.z);
            facingDirection = transform.right;
        }
        else if (player.position.x <= grandParentTransform.position.x)
        {
            playerPos = new Vector3(player.position.x + 4.0f, player.position.y, player.position.z);
            chargeTargetPos = new Vector3(player.position.x - 2.0f, player.position.y, player.position.z);
            facingDirection = -transform.right;
        }
        //Debug.Log(Camera.main.WorldToViewportPoint(player.position));
    }

    IEnumerator Attack(Vector3 start, Vector3 end, float speed)
    {
        isAttacking = true;

        chaseState.GetComponent<RedChaseState>().enabled = false;
        //Debug.Log("charging");
        _animator.SetBool("isCharging", true);
        yield return new WaitForSeconds(AttackChargeUpDduration);
        _animator.SetBool("isCharging", false);
        float t = 0;
        float parameter = 0;
        //Debug.Log("start: " + start);
        //Debug.Log("end: " + end);

        while (t < 1)
        {
            t = parameter / speed;
            transform.parent.parent.position = Vector3.Lerp(start, end, t);
            parameter += Time.deltaTime;
            
            if (Mathf.Approximately(start.x, end.x))
            {
                Debug.Log("pos: " + transform.parent.parent.position);
            }
            //Debug.Log(t);
            yield return null;
        }
        transform.parent.parent.position = end;

        //Debug.Log("cooldown");
        _animator.SetBool("isCoolDown", true);
        yield return new WaitForSeconds(AttackCooldownTime);
        _animator.SetBool("isCoolDown", false);


        // rb.constraints = RigidbodyConstraints2D.FreezeAll;
        // yield return new WaitForSeconds(AttackChargeUpDduration); 
        
        // rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        // rb.AddForce(facingDirection * jumpforce, ForceMode2D.Impulse);
        // yield return new WaitForSeconds(AttackCooldownTime);

        chaseState.GetComponent<RedChaseState>().enabled = true;
        isAttacking = false;        
    }
}
