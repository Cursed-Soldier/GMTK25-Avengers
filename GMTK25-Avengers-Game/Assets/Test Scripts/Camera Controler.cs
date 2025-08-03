using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public GameObject player;
    private Transform playerT;      
    public float xOffset = 0f;    
    public float zOffset = -10f;  
    public float yOffset = 0f;

    void Start()
    {
        playerT = player.transform;
    }

    void LateUpdate()
    {
        Vector3 newPos = new Vector3(xOffset, playerT.position.y + yOffset, zOffset);
        transform.position = newPos;
    }
}
