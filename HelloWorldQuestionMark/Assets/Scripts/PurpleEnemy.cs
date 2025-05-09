using System;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]
public class PurpleEnemy : MonoBehaviour
{

    public LayerMask wallMask;
    public int verticalRays = 5;
    float verticalRaySpacing;
    public float skinWidth = 0.1f;
    private bool isMovingDown;
    public EnemyHealth enemyHealth;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;
    [SerializeField] public float enemySpeed = 5f;
    [SerializeField] public int health = 1;

    [Header("")]
    [SerializeField] Get;

    CircleCollider2D collider;
    RayCast rayCastOrigins;
    public CollisionDetection collision;

    void Start ()
    {
        collider = GetComponent<CircleCollider2D> ();
        wallMask = LayerMask.GetMask("Default");
        enemyHealth = new EnemyHealth(health);
        CalculateRaySpacing();
    }

    // Makes enemy switch direction if it it bumps into an object or leaves the ground
    void Update ()
    {
        if (this.enemyHealth.entityHealth == 0) {
            Destroy(this);
        }

        UpdateRayCast();
        collision.Reset();

        // Determines movement and checks for if hitting objects
        if (isMovingDown)
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

        verticalRays = Mathf.Clamp(verticalRays, 2, int.MaxValue);

        verticalRaySpacing = (bounds.size.x + 2 * skinWidth) / (verticalRays - 1);
    }

    // Detects vertical collisions with floors
    void VerticalCollisions (int direction)
    {
        float rayLength = skinWidth;

        for (int i = 0; i < verticalRays; i++)
        {
            // Determines whether the rays are drawn on the top or the bottom
            Vector2 rayOrigin = (direction == 1) ? rayCastOrigins.topLeft : rayCastOrigins.bottomLeft;

            // Creates each of the rays based on the amount, propogating them left to right
            rayOrigin += Vector2.right * (verticalRaySpacing * i);
            rayOrigin += Vector2.right * (skinWidth * direction);

            // Detects if the rays interact with anything on the "Default" layer
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, wallMask);

            // Visibly displays rays
            Debug.DrawRay(rayOrigin, Vector2.down * direction * rayLength, Color.blue);
            Debug.DrawLine(rayCastOrigins.bottomLeft, rayCastOrigins.bottomRight, Color.blue);
            Debug.DrawLine(rayCastOrigins.topLeft, rayCastOrigins.topRight, Color.blue);

            // Checks if the enemy has hit anything that isn't itelf
            if (hit && hit.collider != collider)
            {
                rayLength = hit.distance;

                // Checks if not touching the floor
                Debug.Log("Wall");
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
        VerticalCollisions(direction);
        enemy.position = new Vector2(enemy.position.x, enemy.position.y + Time.deltaTime * direction * enemySpeed);
    }

    void ChangeDirection ()
    {
        isMovingDown = !isMovingDown;
    }

    struct RayCast
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    public struct CollisionDetection
    {
        public bool above, below;

        public void Reset ()
        {
            above = below = false;
        }
    }
}