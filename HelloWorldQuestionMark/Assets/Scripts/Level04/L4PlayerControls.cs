using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class L4PlayerControls : MonoBehaviour
{
    //Movement variables
  [SerializeField] float moveSpeed;
  [SerializeField] float dashSpeed = 10f;
  [SerializeField] float dashDuration = 1f;
  [SerializeField] float dashCooldown = 1f;
  bool isDashing;
  bool canDash = true;


  //Attacking Variables
private int pComboIterator = 0;
private float punchLag = 0.5f;
private bool isPunching;
    
    [SerializeField] int lightPDamage;
  //Component variables

  private Rigidbody2D rb;
  private Vector2 moveInput;
  private Vector2 moveDirection;

  private Animator animator;

  [SerializeField] private Boolean facingRight;

  

    void Start()
    {

        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    void FixedUpdate()
    {
         if (isDashing)
        {
            return;
        }
        if (isPunching)
        {
        Debug.Log("ispsuf");
        rb.linearVelocity = moveInput * 0;
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
       StartCoroutine(GivePunchlag());
    }
    private IEnumerator GivePunchlag()
    {
        isPunching = true;
        yield return new WaitForSeconds(punchLag);
        isPunching = false;
    }

}
