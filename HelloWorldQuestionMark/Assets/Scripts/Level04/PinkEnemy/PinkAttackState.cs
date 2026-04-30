using UnityEngine;
using System.Collections;

public class PinkAttackState : PinkState
{
    public PinkChaseState chaseState;

    private bool pinkAttReadyToAttack;
    private Transform target;
    public Vector3 targetPos;
    private float bulletSpeed = 10.0f;
    [SerializeField] private GameObject bullet;

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
        //Debug.Log("pink: " + transform.parent.parent.position.y);
        //Debug.Log("player: " + targetPos.y);
        //Debug.Log(Mathf.Approximately(transform.parent.parent.position.y, targetPos.y));
        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
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

    
    private void Attack()
    {
        GameObject bulletInstance = Instantiate(bullet, transform.parent.parent.position, transform.parent.parent.rotation);
        Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = transform.parent.parent.forward * bulletSpeed;
        }

        //StartCoroutine(InterspaceAttack());
    }

    // IEnumerator InterspaceAttack()
    // {
    //     yield return new WaitForSeconds(2.0f);

    // }
}
