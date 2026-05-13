using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class L4PlayerControls : MonoBehaviour
{
    //Movement variables
  [SerializeField] float moveSpeed = 3f;
  [SerializeField] float dashSpeed = 10f;
  [SerializeField] float dashDuration = 1f;
  [SerializeField] float dashCooldown = 1f;
  bool isDashing;
  bool canDash = true;


  //Attacking Variables
private int pComboIterator = 0;
private float punchLag = 0.67f;
private bool isPunching;
    [SerializeField] int lightPDamage;

    // hitbox variables 
    public GameObject attackPoint;
    public float radius;

    public LayerMask enemies;

  //Component variables

  private Rigidbody2D rb;
  private Vector2 moveInput;
  private Vector2 moveDirection;

    private PlayerInput playerInput;
  private Animator animator;

  [SerializeField] private Boolean facingRight;

    // LayerMask variables
    public LayerMask breakables;


    void Start()
    {

        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();

    }

    void FixedUpdate()
    {
         if (isDashing)
        {
            return;
        }
        if (isPunching)
        {
        
        rb.linearVelocity = moveInput * 0;
        animator.SetBool("IsPunching", false); 
           return;
        }
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        rb.linearVelocity = moveInput * moveSpeed;
        moveDirection = new Vector2(moveX,moveY).normalized;
        PointPlayer(moveX);
    }
    public void Move(InputAction.CallbackContext context)
    {
       
       animator.SetBool("IsWalking", true);
        if (context.canceled)
        {
           animator.SetBool("IsWalking", false); 
        }
        

        moveInput = context.ReadValue<Vector2>();

        
    }
    
void PointPlayer(float moveX)
    {

        //checks horizontal input -> points player towards left/right   
         if (moveX > 0)
        {
            facingRight = true;
        }
        if (moveX < 0)
        {
            facingRight = false;
        }

        // Changes the direction the Player is facing
        if (facingRight == true)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
        }
        else
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
        }
    }
    
    //
    public void DashControl(InputAction.CallbackContext context)
    {
       if (canDash){ 
       StartCoroutine(Dash());
       }
    }



    //IEnumerator that handles dash cooldowns
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.linearVelocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }



    //Attacking    
    public void Punch(InputAction.CallbackContext context)
    {
        // Debug.Log("Punch() called, phase: " + context.phase);

        if (context.interaction is TapInteraction)
        {
           animator.SetBool("IsPunching", true);
            StartCoroutine(GivePunchlag()); 
            
        }
       if(context.interaction is HoldInteraction)
        {
            animator.SetBool("IsPunching", false);
        }
       if (context.canceled)
        {
           animator.SetBool("IsPunching", false); 
        }

    }


    
    private IEnumerator GivePunchlag()
    {
        
        isPunching = true;
        yield return new WaitForSeconds(punchLag);
        isPunching = false;
        animator.SetBool("IsPunching", false);

        
    }



    //Creates Light Punch Hitbox with animation event
    public void CreateLightPHitbox()
    {
        
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);

        foreach(Collider2D enemyGameObject in enemy)
        {
            Debug.Log("HitEnemy");
            enemyGameObject.GetComponent<L4Health>().health -= lightPDamage;
        }
        

       

        // 1. Detect breakables
        Collider2D[] breakableHits = Physics2D.OverlapCircleAll(attackPoint.transform.position,radius, breakables);

        foreach (Collider2D hit in breakableHits)
        {
            //Debug.Log("HitBreakable: " + hit.name);

            Debug.Log("HitBreakable: " + hit.name);

            BreakableObject b = hit.GetComponent<BreakableObject>();
            if (b != null)
            {
                b.TakeDamage(1); // 1 damage per punch
            }

        }

      
        

    }

    //Draws hitbox when i needed it in editor
    // private void OnDrawGizmos() 
    // {
    //     Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    // }






}
