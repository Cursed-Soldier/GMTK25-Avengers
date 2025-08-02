using UnityEngine;

public class TeleporterLogic : MonoBehaviour
{
    public bool isleftLevelTeleporter;
    public float xOffset;
    public GameObject pairedTeleporter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            teleport(other.gameObject);
        }
    }

    void teleport(GameObject toTeleport)
    {
        float otherTeleXOffset = pairedTeleporter.GetComponent<TeleporterLogic>().xOffset;
        //set layer 
        if(isleftLevelTeleporter == true)
        {
            toTeleport.layer = LayerMask.NameToLayer("RightSidePlayer");
            SpriteRenderer sr = toTeleport.GetComponent<SpriteRenderer>();
            sr.maskInteraction = SpriteMaskInteraction.VisibleInsideMask; 

            //player is holding something
            if (toTeleport.GetComponent<PlayerObjectInteract>().heldObject  != null )
            {
                GameObject heldObject = toTeleport.GetComponent<PlayerObjectInteract>().heldObject.gameObject;
                heldObject.layer = LayerMask.NameToLayer("RightSideThrowable");

                SpriteRenderer itemsr = heldObject.GetComponent<SpriteRenderer>();
                itemsr.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

                if(heldObject.CompareTag("Spear"))
                {
                    GameObject speartip = heldObject.transform.GetChild(0).gameObject;
                    speartip.layer = LayerMask.NameToLayer("RightSideThrowable");
                }
            }
        }
        else
        {
            toTeleport.layer = LayerMask.NameToLayer("LeftSidePlayer");
            SpriteRenderer sr = toTeleport.GetComponent<SpriteRenderer>();
            sr.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;

            //Player is holding something
            if (toTeleport.GetComponent<PlayerObjectInteract>().heldObject != null)
            {
                GameObject heldObject = toTeleport.GetComponent<PlayerObjectInteract>().heldObject.gameObject;
                heldObject.layer = LayerMask.NameToLayer("LeftSideThrowable");

                SpriteRenderer itemsr = heldObject.GetComponent<SpriteRenderer>();
                itemsr.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;

                if (heldObject.CompareTag("Spear"))
                {
                    GameObject speartip = heldObject.transform.GetChild(0).gameObject;
                    speartip.layer = LayerMask.NameToLayer("LeftSideThrowable");
                }
            }
        }

            //also need to set the layer of any held item

            toTeleport.transform.position = pairedTeleporter.transform.position + new Vector3(otherTeleXOffset, 0f, 0f);
    }
}
