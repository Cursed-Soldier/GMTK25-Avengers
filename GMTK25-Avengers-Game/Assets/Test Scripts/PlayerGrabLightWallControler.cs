using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrabLightWallControler : MonoBehaviour
{
    public PlayerObjectInteract itemPickUpController;
    private WallOfLightController currentWall;
    public PlayerVariables playerVars;


    //While e is pressed and there is no item held and a currentwall is avaible starts moving the wall
    public void grabWall(InputAction.CallbackContext IcallBack)
    {
        if (IcallBack.started && currentWall != null && playerVars.isHoldingItem == false && playerVars.isMovingWall == false)
        {
            playerVars.isMovingWall = true;
            playerVars.playDragAudio();
            currentWall.StartDragging(transform);
        }

        if (IcallBack.canceled && currentWall != null && playerVars.isHoldingItem == false)
        {
            playerVars.isMovingWall = false;
            playerVars.stopAudio();
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
            {
                currentWall.StopDragging();
                playerVars.isMovingWall = false;
                playerVars.stopAudio();
                currentWall = null;
            }
        }
    }

}
