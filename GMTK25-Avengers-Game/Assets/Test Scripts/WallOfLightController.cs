using UnityEngine;

public class WallOfLightController : MonoBehaviour
{
    public Transform leftLimit, rightLimit;
    private bool isDragging = false;
    private Transform player = null;

    public float playerOffsetFromWall;
    private float xOffset = 0f;

    void Update()
    {
        if (isDragging && player != null)
        {
            float targetX = player.position.x + xOffset;
            float clampedX = Mathf.Clamp(targetX, leftLimit.position.x, rightLimit.position.x);
            transform.position = new Vector2(clampedX, transform.position.y);
        }
    }

    public void StartDragging(Transform playerTransform)
    {
        player = playerTransform;
        xOffset = transform.position.x - player.position.x;

        //add/subtract it to create space between player and the wall
        if(xOffset < 0f)
        {
            xOffset -= playerOffsetFromWall;
        }
        else
        {
            xOffset += playerOffsetFromWall;
        }

            isDragging = true;
    }

    public void StopDragging()
    {
        isDragging = false;
        player = null;
    }
}
