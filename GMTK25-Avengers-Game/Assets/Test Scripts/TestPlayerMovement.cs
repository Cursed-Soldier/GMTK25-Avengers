using UnityEngine;

public class TestPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dragSlowMultiplier = 0.5f;
    public float jumpForce = .02f;
    public Transform groundCheck;
    public float groundCheckRadius = .4f;
    private LayerMask whatIsGround;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;
    public PlayerVariables playerVars;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if player is touching the ground
        //IMPORTANT NEEDS TO BE ADDED TO MOVMENT SYSTEM

        if (gameObject.layer == LayerMask.NameToLayer("RightSidePlayer"))
        {
            //CheckGround
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, LayerMask.GetMask("RightSideGround"));
            //IfGroundNotFound Check Throwable Layer
            if (isGrounded == false)
            {
                isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, LayerMask.GetMask("RightSideThrowable"));
            }

        }
        else if (gameObject.layer == LayerMask.NameToLayer("LeftSidePlayer"))
        {
            //CheckGround
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, LayerMask.GetMask("LeftSideGround"));
            //IfGroundNotFound Check Throwable Layer
            if (isGrounded == false)
            {
                isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, LayerMask.GetMask("LeftSideThrowable"));
            }
        }

        //Debug Testing
        /*Collider2D[] hits = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, whatIsGround);
        if (hits.Length > 0) {
            Debug.Log("Ground detect hit " + hits[0].gameObject.name);
        }*/

        // Jump when pressing space and grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        moveInput = Input.GetAxisRaw("Horizontal");

        //Flip Charecter based on direction
        if (moveInput != 0 && playerVars.isMovingWall == false)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(moveInput) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    void FixedUpdate()
    {
        if (playerVars.isMovingWall)
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed * dragSlowMultiplier, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }
    }


    }
