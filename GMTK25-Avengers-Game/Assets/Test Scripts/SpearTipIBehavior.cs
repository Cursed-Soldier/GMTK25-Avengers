using UnityEngine;

public class SpearTipIBehavior : MonoBehaviour
{
    private Collider2D spearTip;
    private Rigidbody2D spearRidgidBody;

    public Collider2D shaftCollider;
    public PlatformEffector2D effector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effector.enabled = false;
        shaftCollider.usedByEffector = false;
        spearTip = gameObject.GetComponent<Collider2D>();
        spearRidgidBody = gameObject.GetComponentInParent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Hit layer: " + other.gameObject.layer + " (" + LayerMask.LayerToName(other.gameObject.layer) + ")");

        //check if the spear tip hit a wall
        if (gameObject.layer == LayerMask.NameToLayer("LeftSideThrowable") && other.gameObject.layer == LayerMask.NameToLayer("LeftSideWalls"))
        {
            Rigidbody2D rb = transform.root.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.bodyType = RigidbodyType2D.Static;
                effector.enabled = true;
                shaftCollider.usedByEffector = true;
            }
        }
        else if (gameObject.layer == LayerMask.NameToLayer("RightSideThrowable") && other.gameObject.layer == LayerMask.NameToLayer("RightSideWalls"))
        {
            Rigidbody2D rb = transform.root.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.bodyType = RigidbodyType2D.Static;
                effector.enabled = true;
                shaftCollider.usedByEffector = true;
            }
        }
    }
    
}
