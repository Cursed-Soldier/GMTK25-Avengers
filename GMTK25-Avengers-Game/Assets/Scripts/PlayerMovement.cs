using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    public InputAction JumpAction;

    private bool isGrounded = false;
    private Rigidbody2D rb;

    public float moveSpeed = 10f;
    public float jumpForce = 10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        MoveAction.Enable();
        JumpAction.Enable();
    }

    void OnDisable()
    {
        MoveAction.Disable();
        JumpAction.Disable();
    }

    void Update()
    {
        Vector2 input = MoveAction.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(input.x * moveSpeed, rb.linearVelocity.y);

        // Jump if grounded
        if (JumpAction.IsPressed() && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }
}
