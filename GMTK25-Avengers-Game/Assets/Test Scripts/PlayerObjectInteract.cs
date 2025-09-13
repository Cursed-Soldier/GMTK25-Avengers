using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerObjectInteract : MonoBehaviour
{
    //Control Inputs
    bool Interacting = false;
    bool Throwing = false;

    public Transform holdPoint; // Empty GameObject where held item will stay
    public float pickupRange = 1.5f;
    public LayerMask leftSidepickupLayer;
    public LayerMask rightSidepickupLayer;
    public PlayerVariables playerVars;

    public float heldObjectExtendAmount;
    public bool holdPointIsExtended = false;

    [HideInInspector]
    public ThrowableInteraction heldObject;

    public void registerPlayerInteract(InputAction.CallbackContext IcallBack)
    {
        if (IcallBack.performed)
        {
            if (heldObject == null && playerVars.isMovingWall != true)
            {
                TryPickup();
            }
            else
            {
                Drop();
            }
        }
   
    }

    public void registerThrow(InputAction.CallbackContext IcallBack)
    {
        if (IcallBack.performed && heldObject != null)
        {
            ThrowObject();
        }
    }

    void Update()
    {
        //Reste the point where the item is held
        if (heldObject == null && holdPointIsExtended == true)
        {
            holdPointIsExtended=false;
            Vector3 pos = holdPoint.transform.position;
            pos.x -= heldObjectExtendAmount;
            holdPoint.transform.position = pos;
        }
    }

    void TryPickup()
    {
        Collider2D[] hits;
        if (gameObject.layer == LayerMask.NameToLayer("LeftSidePlayer")) {
            hits = Physics2D.OverlapCircleAll(transform.position, pickupRange, leftSidepickupLayer);
        }
        else
        {
            hits = Physics2D.OverlapCircleAll(transform.position, pickupRange, rightSidepickupLayer);
        }

            foreach (var hit in hits)
            {
                ThrowableInteraction to = hit.GetComponent<ThrowableInteraction>();
                if (to != null)
                {
                    heldObject = to;

                    //Check size of Held Object and Adjust position relative to player
                    if (heldObject.gameObject.GetComponent<ThrowableInteraction>().isLargeItem == true)
                    //Throwable is large
                    {
                        if (holdPointIsExtended == false)
                        {
                            holdPointIsExtended = true;
                            Vector3 pos = holdPoint.transform.position;
                            pos.x += heldObjectExtendAmount;
                            holdPoint.transform.position = pos;
                        }
                    }
                    //Throwable is small
                    else
                    {
                        if (holdPointIsExtended == true)
                        {
                            holdPointIsExtended = false;
                            Vector3 pos = holdPoint.transform.position;
                            pos.x -= heldObjectExtendAmount;
                            holdPoint.transform.position = pos;
                        }
                    }

                    //Reset Physics and Scale on Picked up stuff
                    heldObject.transform.rotation = Quaternion.identity;
                    heldObject.transform.localScale = heldObject.originalScale;
                    Rigidbody2D heldRB = heldObject.GetComponent<Rigidbody2D>();

                    heldRB.linearVelocity = Vector2.zero;
                    heldRB.angularVelocity = 0f;
                    heldRB.rotation = 0f;
                    heldRB.bodyType = RigidbodyType2D.Kinematic;

                    //Check if spear
                    if (heldObject.gameObject.CompareTag("Spear"))
                    {
                        heldObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        heldObject.gameObject.GetComponent<PlatformEffector2D>().enabled = false;
                        heldObject.gameObject.GetComponent<Collider2D>().usedByEffector = false;
                        heldObject.gameObject.GetComponentInChildren<SpearTipIBehavior>().isHeld = true;
                }

                    heldObject.transform.SetParent(holdPoint, true);
                    heldObject.transform.position = holdPoint.position;
                    playerVars.isHoldingItem = true;
                    break;
                }
            }
    }

    void ThrowObject()
    {
        if (heldObject.gameObject.CompareTag("Spear"))
        {
            //Set colliders to ignore each other
            /*Collider2D spearCollider = heldObject.gameObject.GetComponent<Collider2D>();
            Collider2D playerCollider = gameObject.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(spearCollider, playerCollider, true);*/

            float spearDirection = Mathf.Sign(transform.localScale.x);
            float spearOffset = heldObject.gameObject.GetComponent<ThrowableInteraction>().SpearThrowExtendAmount;
            Vector2 offset = new Vector2(spearDirection * spearOffset, 0f);
            heldObject.transform.position += (Vector3)offset;

            //Rotate Spear
            heldObject.transform.rotation = Quaternion.Euler(0f, 0f, spearDirection == 1 ? -90f : 90f);

            //Set Spear to Not Held
            heldObject.gameObject.GetComponentInChildren<SpearTipIBehavior>().isHeld = false;


        }
        playerVars.playThrowAudio();
        Vector2 throwDir = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        heldObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        heldObject.Throw(throwDir);
        //Fix scaling
        heldObject.transform.localScale = heldObject.originalScale;
        heldObject = null;
        playerVars.isHoldingItem = false;
    }

    void Drop()
    {
        if (heldObject != null)
        {
            if(heldObject.gameObject.CompareTag("Spear"))
            {
                heldObject.gameObject.GetComponentInChildren<SpearTipIBehavior>().isHeld = false;
            }
            heldObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            heldObject.transform.SetParent(null);
            heldObject = null;
            playerVars.isHoldingItem = false;
            
        }
    }

    //after a spear throw we can re-enable the colliders
    /*private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spear"))
        {
            Collider2D spearCollider = collision.gameObject.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), spearCollider, false);
        }
    }*/
}
