using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
  [SerializeField] float moveSpeed;
  [SerializeField] float dashSpeed = 10f;
  [SerializeField] float dashDuration = 1f;
  [SerializeField] float dashCooldown = 1f;
  bool isDashing;
  bool canDash = true;

  private Rigidbody2D rb;
  private Vector2 moveInput;
  private Vector2 moveDirection;

  

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
         if (isDashing)
        {
            return;
        }
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        rb.linearVelocity = moveInput * moveSpeed;
        moveDirection = new Vector2(moveX,moveY).normalized;
        
    }

    void FixedUpdate()
    {
         if (isDashing)
        {
            return;
        }
    }


    public void Move(InputAction.CallbackContext context)
    {
       
        moveInput = context.ReadValue<Vector2>();
    }

    public void DashControl(InputAction.CallbackContext context)
    {
       if (canDash){ 
       StartCoroutine(Dash());
       }
    }

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

}
