using System;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]
public class RedEnemy : MonoBehaviour
{

    public LayerMask wallMask;
    public int horizontalRays = 5;
    public int verticalRays = 5;
    public float skinWidth = 0.1f;
    private float horizontalRaySpacing;
    private float verticalRaySpacing;
    private bool isMovingLeft;
    private SpriteRenderer spriteRenderer;
    private EnemyHealth enemyHealth;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;
    [SerializeField] private float enemySpeed = 5f;
    [SerializeField] private int health = 1;

    CircleCollider2D collider;
    RayCast rayCastOrigins;
    public CollisionDetection collision;

    void Start ()
    {
        // Sets up enemy collissions with RayCasting
        collider = GetComponent<CircleCollider2D> ();
        wallMask = LayerMask.GetMask("Default");
        CalculateRaySpacing();

        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyHealth = new EnemyHealth(health);

        // Ensures the enemy always starts facing the right direction
        Flip();
    }

    // Makes enemy switch direction if it it bumps into an object or leaves the ground
    void Update ()
    {
        UpdateRayCast();
        collision.Reset();

        // Determines movement and checks for if hitting objects
        if (isMovingLeft)
        {
            Movement(-1);
        }
        else
        {
            Movement(1);
        }
    }

    // Creates rays to check if the object is touching another object
    void CalculateRaySpacing ()
    {
        Bounds bounds = collider.bounds;

        horizontalRays = Mathf.Clamp(horizontalRays, 2, int.MaxValue);
        verticalRays = Mathf.Clamp(verticalRays, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRays - 1);
        verticalRaySpacing = (bounds.size.x + 2 * skinWidth) / (verticalRays - 1);
    }

    // Detects horizontal collisions with walls
    void HorizontalCollisions (int direction)
    {
        float rayLength = skinWidth;

        for (int i = 0; i < horizontalRays; i++)
        {
            Vector2 rayOrigin = (direction < 0) ? rayCastOrigins.bottomLeft : rayCastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * direction, rayLength, wallMask);

            // Visibly displays rays
            // Debug.DrawRay(rayOrigin, Vector2.right * direction * rayLength, Color.green);
            // Debug.DrawLine(rayCastOrigins.bottomLeft, rayCastOrigins.topLeft, Color.blue);
            // Debug.DrawLine(rayCastOrigins.bottomRight, rayCastOrigins.topRight, Color.blue);
            if (hit)
            {
                rayLength = hit.distance;

                // Checks for walls
                if (hit.collider != null)
                {
                    // Debug.Log($"Wall detected at: {hit.point}");
                    ChangeDirection();
                }
            }
        }
    }

    // Detects vertical collisions with floors
    void VerticalCollisions (int direction)
    {
        float rayLength = skinWidth;

        for (int i = 0; i < verticalRays; i++)
        {
            Vector2 rayOrigin = (direction == 1)? rayCastOrigins.bottomLeft : rayCastOrigins.bottomRight;
            rayOrigin += Vector2.right * (verticalRaySpacing * i * direction);
            rayOrigin += Vector2.right * (skinWidth * direction);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, wallMask);

            // Visibly displays rays
            // Debug.DrawRay(rayOrigin, Vector2.down * rayLength, Color.blue);
            // Debug.DrawLine(rayCastOrigins.bottomLeft, rayCastOrigins.bottomRight, Color.blue);
            // Debug.DrawLine(rayCastOrigins.topLeft, rayCastOrigins.topRight, Color.blue);
            if (!hit)
            {
                // Checks if not touching the floor
                // Debug.Log($"No floor");
                ChangeDirection();
                return;
            }
        }
    }
    
    void UpdateRayCast ()
    {
        Bounds bounds = collider.bounds;

        rayCastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        rayCastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
        rayCastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        rayCastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
    }

    void Movement (int direction)
    {
        HorizontalCollisions(direction);
        VerticalCollisions(direction);
        enemy.position = new Vector2(enemy.position.x + Time.deltaTime * direction * enemySpeed, enemy.position.y);
    }

    void ChangeDirection ()
    {
        isMovingLeft = !isMovingLeft;

        Flip();
    }

    void Flip()
    {
        // Flips the enemy sprite
        if (isMovingLeft)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    struct RayCast
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    public struct CollisionDetection
    {
        public bool above, below;
        public bool left, right;

        public void Reset ()
        {
            above = below = false;
            left = right = false;
        }
    }
}