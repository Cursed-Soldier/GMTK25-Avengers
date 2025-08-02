using UnityEngine;

public class ElectricWallBehavior : MonoBehaviour
{
   /* private GameObject playerObj;
    void Start()
    {
        playerObj = GameObject.FindWithTag("Player");

    }*/


    //when sothing is thrown through the wall it needs to switch its physics for the other side
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("LeftSideThrowable"))
        {
            other.gameObject.layer = LayerMask.NameToLayer("RightSideThrowable");
            SpriteRenderer sr = other.gameObject.GetComponent<SpriteRenderer>();
            sr.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("RightSideThrowable"))
        {
            other.gameObject.layer = LayerMask.NameToLayer("LeftSideThrowable");
            SpriteRenderer sr = other.gameObject.GetComponent<SpriteRenderer>();
            sr.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        }
    }
}
