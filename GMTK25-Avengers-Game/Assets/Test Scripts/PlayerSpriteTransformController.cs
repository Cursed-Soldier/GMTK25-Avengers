using UnityEngine;

public class PlayerSpriteTransformController : MonoBehaviour
{
    public GameObject playerMain;
    private Vector3 playerV;
    void Start()
    {
        playerV = playerMain.transform.position;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        Debug.Log("trying to match sprite to player");
        transform.position = playerV;
    }
}
