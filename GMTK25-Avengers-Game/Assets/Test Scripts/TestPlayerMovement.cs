using UnityEngine;

public class TestPlayerMovement : MonoBehaviour
{
    public float MaxGroundSpeed;
    public float MaxAirSpeed;
    public float dragSlowMultiplier = 0.5f;
    public float jumpForce = .02f;
    public Transform groundCheck;
    public float groundCheckRadius = .4f;
    public float airAccel;
    public float airDeccel;
    public float groundAccel;
    public float groundDeccel;



    private LayerMask whatIsGround;
    private float nextStepTime = 0;

    private Rigidbody2D rb;
    private float moveInput;
    [SerializeField] private bool isGrounded;
    public PlayerVariables playerVars;

    private Animator anim;
    private bool jump = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

        // Check if falling
        if (jump && isGrounded)
        {
            jump = false;
            anim.SetBool("Grounded", isGrounded);
            
        }

        // Jump when pressing space and grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            var v = rb.linearVelocity;
            v.y = jumpForce; 
            rb.linearVelocity = v;

            playerVars.playJumpAudio();
            jump = true;
            anim.SetBool("Jump", jump);
        }




        moveInput = Input.GetAxisRaw("Horizontal");
        anim.SetBool("Grounded", isGrounded);
        if (transform.localScale.x == -1)
        {
            anim.SetFloat("FacingX", -1);
        }
        else
        {
            anim.SetFloat("FacingX", 1);
        }


        //Flip Charecter based on direction
        if (moveInput != 0 && playerVars.isMovingWall == false)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(moveInput) * Mathf.Abs(scale.x);
            transform.localScale = scale;
            //anim.SetFloat("FacingX", moveInput);
        }

        //Play Audio If player is grounded and moving 
        if (isGrounded && rb.linearVelocity.magnitude > 0.1f && Time.time > nextStepTime)
        {
            playerVars.playWalkingAudio();
            nextStepTime = Time.time + playerVars.MaxStepAudioSize;
        }
    }

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;

        Vector2 v = rb.linearVelocity;

        // Desired speed from input
        float max = isGrounded ? MaxGroundSpeed : MaxAirSpeed;
        float targetX = moveInput * max;
        if (playerVars.isMovingWall) targetX *= dragSlowMultiplier;

        if (isGrounded)
        {
            // ON GROUND: instant stop / instant turn
            if (Mathf.Abs(moveInput) < 0.01f)
            {
                v.x = 0f;                    // no slide when releasing input
            }
            else
            {
                v.x = targetX;               // snap immediately to the new direction/speed
            }
        }
        else
        {
            // IN AIR: reduced control, momentum carries
            bool hasInput = Mathf.Abs(moveInput) > 0.01f;

            if (hasInput)
                v.x = Mathf.MoveTowards(v.x, targetX, airAccel * dt);
            else
                v.x = Mathf.MoveTowards(v.x, 0f, airDeccel * dt);
        }

        // Never touch v.y here (gravity/jump handled elsewhere)
        rb.linearVelocity = v;

        // Animator drive from horizontal speed
        anim.SetFloat("Speed", Mathf.Abs(v.x));
    }




}
