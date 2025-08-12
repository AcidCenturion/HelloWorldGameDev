using UnityEngine;

public class L2PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ProcessInputs();

        if (rb.linearVelocity.x > 0)
        {
            facingRight = true;
        }
        if (rb.linearVelocity.x < 0)
        {
            facingRight = false;
        }
    }

    void FixedUpdate()
    {
        Move();
        FlipPlayer();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * playerSpeed, moveDirection.y * playerSpeed);
    }

    void FlipPlayer()
    {
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
}
