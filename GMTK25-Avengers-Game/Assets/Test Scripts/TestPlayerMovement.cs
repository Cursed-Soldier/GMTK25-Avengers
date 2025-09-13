using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerMovement : MonoBehaviour
{
    //Control Inputs
    private Vector2 moveInput;
    private bool jumpInput = false;

    // Movement tuning
    public float groundSpeed = 8f;
    public float MaxAirSpeed = 6.5f;
    public float dragSlowMultiplier = 0.5f;   // applies when playerVars.isMovingWall is true
    public float jumpForce = 12f;

    // Ground check
    public Transform groundCheck;
    public float groundCheckRadius = 0.18f;   // keep small to avoid ledge/wall flicker

    // Acceleration
    public float airAccel = 20f;
    public float airDeccel = 10f;             // not used when no input (we keep momentum)
    public float groundAccel = 60f;           // kept for reference; ground snaps instantly
    public float groundDeccel = 70f;          // kept for reference; ground snaps instantly

    // Launch-carry tuning
    public float baseCarryTime = 0.22f;       // straight-up or slow launches
    public float extendedCarryTime = 0.34f;   // fast directional launches
    public float minCarryFactor = 0.20f;      // weak bias for straight-up
    public float maxCarryFactor = 0.70f;      // strong bias for fast launches
    public float boostSpeedThreshold = 0.5f;  // fraction of MaxAirSpeed to qualify as "fast"

    // Runtime carry state
    float carryDuration;      // duration for this jump
    float carryStrength;      // strength for this jump (0..1)
    float carryTimer = 0f;    // counts down while biasing
    float carryX = 0f;        // horizontal speed at takeoff

    // Misc
    private float nextStepTime = 0f;
    private Rigidbody2D rb;
    private Animator anim;

    private float fallingAnimTimer = 0;
    public float fallingAnimBuffer = .5f;
    public bool fallTimerActive = false;

    [SerializeField] private bool isGrounded;
    public PlayerVariables playerVars;

    public void Move(InputAction.CallbackContext IcallBack)
    {
        moveInput = IcallBack.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext IcallBack)
    {
        if (IcallBack.performed)
        {
            jumpInput = true;
        }
        else
        {
            jumpInput = false;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float rt = Time.time;

        // --- Ground check (layer-aware) ---
        // Keep radius modest; large values cause false positives on walls/ledges.
        if (gameObject.layer == LayerMask.NameToLayer("RightSidePlayer"))
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, LayerMask.GetMask("RightSideGround"));
            if (!isGrounded)
                isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, LayerMask.GetMask("RightSideThrowable"));
        }
        else if (gameObject.layer == LayerMask.NameToLayer("LeftSidePlayer"))
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, LayerMask.GetMask("LeftSideGround"));
            if (!isGrounded)
                isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, LayerMask.GetMask("LeftSideThrowable"));
        }

        //Reset falling anim timer 
        if(isGrounded)
        {
            fallTimerActive = false;
        }

        // --- Jump input (preserve horizontal, set vertical only) ---
        if (jumpInput == true && isGrounded)
        {
            var v = rb.linearVelocity;
            v.y = jumpForce;
            rb.linearVelocity = v;

            //Start Jump Animation
            
            bool jumpingUp = v.y > 0.05f;
            anim.ResetTrigger("JumpStart");
            anim.SetTrigger("JumpStart");
            // Snapshot takeoff horizontal speed and choose carry profile
            carryX = Mathf.Clamp(rb.linearVelocity.x, -MaxAirSpeed, MaxAirSpeed);
            float speedAbs = Mathf.Abs(carryX);
            float threshold = boostSpeedThreshold * MaxAirSpeed;

            if (speedAbs >= threshold)
            {
                // Fast directional launch
                carryDuration = extendedCarryTime;
                carryStrength = Mathf.Clamp01(maxCarryFactor);
            }
            else
            {
                // Straight-up or slow launch
                carryDuration = baseCarryTime;
                carryStrength = Mathf.Clamp01(minCarryFactor);
            }
            carryTimer = carryDuration;

            if (playerVars != null) playerVars.playJumpAudio();
        }

        

        // --- Facing via scale ---
        if (Mathf.Abs(moveInput.x) > 0.01f && (playerVars == null || playerVars.isMovingWall == false))
        {
            var s = transform.localScale;
            float sign = Mathf.Sign(moveInput.x);
            transform.localScale = new Vector3(Mathf.Abs(s.x) * sign, s.y, s.z);
        }
        float facingSign = Mathf.Sign(transform.localScale.x);

        // --- Animator: single source of truth ---
        // Drive all parameters here, every frame, from physics.
        anim.SetBool("Grounded", isGrounded);

        float vy = rb.linearVelocity.y;

        // Jump is true while moving upward (ensures immediate Jump transition even if ground flag lags)
        //bool jumpingUp = vy > 0.05f;

        // Falling only when in air and descending
        if (fallTimerActive == false && vy < -.05f)
        {
            //Debug.Log("Should be starting fall timer");
            fallingAnimTimer = rt + fallingAnimBuffer;
            fallTimerActive = true;
        }

        if(!isGrounded && vy < -0.05f && fallTimerActive && fallingAnimTimer <= rt)
        {
            Debug.Log("Falling animation should be showing");
        }

        bool falling = !isGrounded && vy < -0.05f && fallTimerActive && fallingAnimTimer <= rt;

        //anim.SetBool("Jump", jumpingUp);
        anim.SetBool("Falling", falling);
        anim.SetFloat("FacingX", facingSign);

        // Smooth horizontal speed for Idle/Run blend to avoid flicker at threshold
        float speedX = Mathf.Abs(rb.linearVelocity.x);
        anim.SetFloat("Speed", speedX, 0.05f, Time.deltaTime);

        // Footsteps (optional)
        if (isGrounded && speedX > 0.1f && Time.time > nextStepTime && playerVars != null)
        {
            playerVars.playWalkingAudio();
            nextStepTime = Time.time + playerVars.MaxStepAudioSize;
        }
    }

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        Vector2 v = rb.linearVelocity;

        // Target horizontal speed from input
        float max = isGrounded ? groundSpeed : MaxAirSpeed;
        float targetX = moveInput.x * max;
        if (playerVars != null && playerVars.isMovingWall) targetX *= dragSlowMultiplier;

        // Launch-carry bias (air only): blend targetX toward carryX while timer runs
        if (!isGrounded && carryTimer > 0f && carryDuration > 0f)
        {
            float u = carryTimer / carryDuration;            // 1 -> 0
            float ease = 1f - (1f - u) * (1f - u);           // ease-out
            float alpha = Mathf.Clamp01(carryStrength * ease);
            targetX = Mathf.Lerp(targetX, carryX, alpha);
            carryTimer -= dt;
        }
        else if (isGrounded)
        {
            carryTimer = 0f; // reset on landing
        }

        // Ground: instant stop/turn, no slide
        if (isGrounded)
        {
            if (Mathf.Abs(moveInput.x) < 0.01f) v.x = 0f;
            else v.x = targetX;
        }
        else
        {
            // Air: momentum-based. Input steers gradually; no input keeps momentum.
            bool hasInput = Mathf.Abs(moveInput.x) > 0.01f;

            if (hasInput)
                v.x = Mathf.MoveTowards(v.x, targetX, airAccel * dt);
            // else: do nothing, keep current v.x

            // Optional clamp to air max
            v.x = Mathf.Clamp(v.x, -MaxAirSpeed, MaxAirSpeed);
        }

        rb.linearVelocity = v;
    }

    
}

