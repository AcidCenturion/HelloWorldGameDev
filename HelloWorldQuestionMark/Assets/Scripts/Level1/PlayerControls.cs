using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Rigidbody2D rb;
    public LayerMask groundLayers;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 40.0f;
    public float gravityScale = 10.0f;
    private const float gravityValue = -9.81f;
    private bool isMovingRight = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        InputMove();
        FlipPlayer();
        InputJump();

        if (rb.linearVelocityX == 0)
        {
            rb.linearVelocityX = 0;
        }

        rb.linearVelocityY += gravityValue * gravityScale * Time.fixedDeltaTime;
    }

    private bool isGrounded()
    {
        RaycastHit2D onGround;
        onGround = Physics2D.CircleCast(transform.position, 0.4f, Vector2.down, 0.2f, groundLayers);

        if (onGround)
        {
            return true;
        }

        return false;
    }

    public void InputMove()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.linearVelocityX = playerSpeed;
            isMovingRight = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.linearVelocityX = playerSpeed * -1;
            isMovingRight = false;
        }
    }

    public void InputJump()
    {
        if (Input.GetKey(KeyCode.W) && isGrounded())
        {
            rb.linearVelocityY = jumpHeight;
        }
    }

    // Changes the Player to look left or right
    void FlipPlayer ()
    {
        // Changes the direction the Player is facing
        if (isMovingRight)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
        }
        else
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
        }
    }
}
