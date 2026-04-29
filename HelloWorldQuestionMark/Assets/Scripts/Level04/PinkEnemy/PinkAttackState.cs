using UnityEngine;
using System.Collections;

public class PinkAttackState : PinkState
{
    public PinkChaseState chaseState;

    private bool pinkAttisInRangeOfPlayer = true;
    private Transform target;
    public Vector3 targetPos;
    private float bulletSpeed = 1.0f;
    [SerializeField] private GameObject bullet;

    //[SerializeField] private Animator _animator;

    public override PinkState RunCurrentState()
    {
        if (pinkAttisInRangeOfPlayer)
        {
            //_animator.SetBool("isInRange", true);
            //Debug.Log("I have attacked!");
            return this;
        }
        else
        {
            //_animator.SetBool("isInRange", false);
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
        Debug.Log("pink: " + transform.parent.parent.position.y);
        Debug.Log("player: " + targetPos.y);
    }

    private void CheckIfChaseFinished()
    {
        if (Mathf.Approximately(transform.parent.parent.position.y, targetPos.y))
        {
            pinkAttisInRangeOfPlayer = true;
        }
        else
        {
            pinkAttisInRangeOfPlayer = false;
        }
    }

    
    private void Attack()
    {
        GameObject bulletInstance = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = transform.forward * bulletSpeed;
        }

        StartCoroutine(InterspaceAttack());
    }

    IEnumerator InterspaceAttack()
    {
        yield return new WaitForSeconds(2.0f);

    }
}
