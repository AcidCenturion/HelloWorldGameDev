using UnityEngine;

public class L2GreenEnemy : Enemy
{
    
    private Rigidbody2D rb;
    private Vector2 moveDirectionStart;
    private Vector2 moveDirection;
    private bool hasHitX = false;
    private bool hasHitY = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirectionStart = new Vector2(this.enemySpeed, this.enemySpeed);
        rb.linearVelocity = moveDirectionStart;

        moveDirection.x = rb.linearVelocity.x;
        moveDirection.y = rb.linearVelocity.y;
    }

    
    void Update()
    {
        //Flip y-direction if hits north or south
        if (hasHitY == true)
        {
            moveDirection.y = moveDirection.y * -1;
            rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y);
            hasHitY = false;

        }

        //Flip x-direction if hits east or west
        if (hasHitX == true)
        {
            moveDirection.x = moveDirection.x * -1;
            rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y);
            hasHitX = false;
        }

        rb.linearVelocityY = moveDirection.x;
        rb.linearVelocityY = moveDirection.y;   
        
        //Debug.Log(moveDirection.x + " " + moveDirection.y);
    }

    //Tells which direction the Green collides a wall on
    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector2 collisionDirection = contact.normal;

            // Debug.Log($"Collision occurred at point: {contact.point}");
            // Debug.Log($"Collision normal (direction from which collision occurred): {collisionDirection}");

            if (collisionDirection.x < 0.5 && collisionDirection.y < 0.05f && collisionDirection.y > -0.05f)
            {
                //Debug.Log("hit east wall");
                hasHitX = true;
            }
            else if (collisionDirection.x > 0.5 && collisionDirection.y < 0.05f && collisionDirection.y > -0.05f)
            {
                //Debug.Log("hit west wall");
                hasHitX = true;
            }
            else if (collisionDirection.y < 0.5 && collisionDirection.x < 0.05f && collisionDirection.x > -0.05f)
            {
                //Debug.Log("hit north wall");
                hasHitY = true;
            }
            else if (collisionDirection.y > 0.5 && collisionDirection.x < 0.05f && collisionDirection.x > -0.05f)
            {
                //Debug.Log("hit south wall");
                hasHitY = true;
            }
        }
    }
}