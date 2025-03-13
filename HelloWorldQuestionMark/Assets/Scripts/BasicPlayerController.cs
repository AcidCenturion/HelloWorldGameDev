using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicPlayerController : MonoBehaviour
{

    [Header("Player")]
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private int playerHealth;

    Rigidbody2D rigidbody2D;
    Vector2 playerInput;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
       playerInput = new Vector2(Input.GetAxisRaw("Horizontal") * playerSpeed, Input.GetAxisRaw("Vertical") * 2 * playerSpeed);
    }

    void FixedUpdate()
    {
        rigidbody2D.AddForce(playerInput);
    }

}
