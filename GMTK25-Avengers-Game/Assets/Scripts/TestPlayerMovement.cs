using UnityEngine;

public class TestPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = .02f;
    public Transform groundCheck;
    public float groundCheckRadius = .4f;
    public LayerMask whatIsGround;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if player is touching the ground
        //IMPORTANT NEEDS TO BE ADDED TO MOVMENT SYSTEM
        whatIsGround = LayerMask.GetMask("Ground", "Throwables");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        // Jump when pressing space and grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(moveInput) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }
}
