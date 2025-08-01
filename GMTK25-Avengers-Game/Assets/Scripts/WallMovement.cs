using UnityEngine;
using UnityEngine.InputSystem;

public class WallMovement : MonoBehaviour
{
    public InputAction MoveAction;

    private Rigidbody2D rb;

    public float moveSpeed = 10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        MoveAction.Enable();
    }

    void OnDisable()
    {
        MoveAction.Disable();
    }

    void Update()
    {
        Vector2 input = MoveAction.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(input.x * moveSpeed, rb.linearVelocity.y);
    }
}
