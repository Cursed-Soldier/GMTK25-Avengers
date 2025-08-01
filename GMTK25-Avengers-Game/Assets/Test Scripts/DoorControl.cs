using UnityEngine;

public class DoorControl : MonoBehaviour
{
    public WallButtonTrigger wbTrigger;
    public GroundButtonTrigger gbTrigger;

    private Vector3 closedPosition;
    public Vector3 openPosition;
    public float speed = 2f;

    void Start()
    {
        closedPosition = transform.position;
        float height = GetComponent<BoxCollider2D>().bounds.size.y;
        openPosition = closedPosition - new Vector3(0f, height, 0f);
    }

    void Update()
    {
        if (wbTrigger != null)
        {
            Vector3 target = wbTrigger.isActive ? openPosition : closedPosition;
            MoveDoor(target);
        }
        else if (gbTrigger != null)
        {
            Vector3 target = gbTrigger.isActive ? openPosition : closedPosition;
            MoveDoor(target);
        }
    }

    void MoveDoor(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
