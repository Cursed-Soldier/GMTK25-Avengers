using UnityEngine;

public class PlayerGrabLightWallControler : MonoBehaviour
{
    public PlayerObjectInteract itemPickUpController;
    private WallOfLightController currentWall;
    public PlayerVariables playerVars;

    //While e is pressed and there is no item held and a currentwall is avaible starts moving the wall
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentWall != null && playerVars.isHoldingItem == false && playerVars.isMovingWall == false)
        {
            playerVars.isMovingWall = true;
            currentWall.StartDragging(transform); 
        }

        if (Input.GetKeyUp(KeyCode.E) && currentWall != null && playerVars.isHoldingItem == false)
        {
            playerVars.isMovingWall = false;
            currentWall.StopDragging();
        }
    }

    //determines if the player can grab onto some part of the wall

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GrabPointTrigger"))
        {
            currentWall = other.GetComponentInParent<WallOfLightController>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("GrabPointTrigger"))
        {
            if (currentWall != null)
                currentWall.StopDragging();

            currentWall = null;
        }
    }

}
