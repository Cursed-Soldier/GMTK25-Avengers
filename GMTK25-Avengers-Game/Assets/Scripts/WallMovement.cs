using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallMovement : MonoBehaviour
{
    public InputAction MoveAction;
    private Rigidbody2D rb;

    public float moveSpeed = 10f;

    public LevelManager levelManager;

    private Transform draggedLevel = null;
    private int draggedIndex = 1;
    private bool movingForward = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        draggedLevel = levelManager.GetLevel(draggedIndex);
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

        if (input.x != 0)
            movingForward = input.x > 0;

        if (draggedLevel != null)
        {
            draggedLevel.position += Vector3.right * input.x * moveSpeed * Time.deltaTime;
        }
    }

    public void StartDragging(bool rightLoop)
    {
        // This needs a lot of tweaking and edge case handling, I'm geeking out so I commit
        if (rightLoop)
        {
            levelManager.DisableLevel(draggedIndex - 1);
            levelManager.SetActiveLevel(draggedIndex);
            levelManager.SnapLevelToCenter(draggedLevel);
            draggedIndex++;
            draggedLevel = levelManager.GetLevel(draggedIndex);
            levelManager.SnapNextLevelToLeft(draggedLevel);
        }

        else
        {
            draggedIndex--;
            draggedLevel = levelManager.GetLevel(draggedIndex);
            levelManager.EnableLevel(draggedIndex);
            levelManager.SetActiveLevel(draggedIndex);
            levelManager.SnapLevelToCenter(draggedLevel);
            draggedLevel = levelManager.GetLevel(draggedIndex + 1);
            levelManager.SnapNextLevelToRight(draggedLevel);
            UnityEngine.Debug.Log("Left");
            UnityEngine.Debug.Log(draggedIndex);
        }
    }
}
