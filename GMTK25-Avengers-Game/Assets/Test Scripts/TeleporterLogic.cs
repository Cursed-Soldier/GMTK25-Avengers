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
            if(toTeleport.GetComponent<PlayerObjectInteract>().heldObject  != null )
            {
                toTeleport.GetComponent<PlayerObjectInteract>().heldObject.gameObject.layer = LayerMask.NameToLayer("RightSideThrowable");
            }
        }
        else
        {
            toTeleport.layer = LayerMask.NameToLayer("LeftSidePlayer");
            if (toTeleport.GetComponent<PlayerObjectInteract>().heldObject != null)
            {
                toTeleport.GetComponent<PlayerObjectInteract>().heldObject.gameObject.layer = LayerMask.NameToLayer("LeftSideThrowable");
            }
        }

            //also need to set the layer of any held item

            toTeleport.transform.position = pairedTeleporter.transform.position + new Vector3(otherTeleXOffset, 0f, 0f);
    }
}
