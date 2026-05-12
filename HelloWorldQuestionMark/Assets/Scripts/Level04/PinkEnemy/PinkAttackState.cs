using UnityEngine;
using System.Collections;

public class PinkAttackState : PinkState
{
    public PinkChaseState chaseState;

    private bool pinkAttReadyToAttack;
    private Transform target;
    public Vector3 targetPos;
    private float bulletSpeed = 10.0f;
    public GameObject bullet;
    private bool isAttacking = false;
    [SerializeField] private float duration = 2.0f;

    [SerializeField] private Animator _animator;


    public override PinkState RunCurrentState()
    {
        if (pinkAttReadyToAttack)
        {
            _animator.SetBool("isInRange", true);
            return this;
            //Debug.Log("I have attacked!");

        }
        else
        {
            _animator.SetBool("isInRange", false);
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
        targetPos = target.position;

        if (pinkAttReadyToAttack && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    private void CheckIfChaseFinished()
    {
        if (Mathf.Approximately(transform.parent.parent.position.y, targetPos.y))
        {
            pinkAttReadyToAttack = true;
        }
        else
        {
            pinkAttReadyToAttack = false;
        }
    }



    IEnumerator Attack()
    {
        isAttacking = true;
        GameObject bulletInstance = Instantiate(bullet, transform.parent.parent.position, transform.parent.parent.rotation);
        Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
    
        yield return new WaitForSeconds(duration);   
        isAttacking = false;
        
    }
}
