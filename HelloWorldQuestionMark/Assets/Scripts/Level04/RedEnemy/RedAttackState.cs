using UnityEngine;
using System.Collections;

public class RedAttackState : RedState
{
    public RedChaseState chaseState;

    private bool AttisInRangeOfPlayer;
    private Transform player;
    public Vector3 playerPos;
    private float distanceThreshold = 0.3f;
    [SerializeField] private float jumpforce = 400f;
    public bool isAttacking = false;
    private Vector3 facingDirection;
    [SerializeField] private float AttackChargeUpDduration = 2.0f;
    [SerializeField] private float AttackCooldownTime = 3.0f;

    public Rigidbody2D rb;

    //[SerializeField] private Animator _animator;

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
        if (!AttisInRangeOfPlayer && isAttacking)
        {
            StopCoroutine(Attack());
        }

        if (AttisInRangeOfPlayer && !isAttacking)
        {
            StartCoroutine(Attack());
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
            facingDirection = transform.right;
        }
        else if (player.position.x <= grandParentTransform.position.x)
        {
            playerPos = new Vector3(player.position.x + 4.0f, player.position.y, player.position.z);
            facingDirection = -transform.right;
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;

        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(AttackChargeUpDduration); 
        
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.AddForce(facingDirection * jumpforce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(AttackCooldownTime);

        isAttacking = false;
        //yield return new WaitForSeconds(1.0f);
        
    }
}
