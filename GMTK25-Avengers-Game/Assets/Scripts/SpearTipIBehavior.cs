using UnityEngine;

public class SpearTipIBehavior : MonoBehaviour
{
    private Collider2D spearTip;
    private Rigidbody2D spearRidgidBody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spearTip = gameObject.GetComponent<Collider2D>();
        spearRidgidBody = gameObject.GetComponentInParent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit layer: " + other.gameObject.layer + " (" + LayerMask.LayerToName(other.gameObject.layer) + ")");

        //check if the spear tip hit a wall
        if (other.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            Rigidbody2D rb = transform.root.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.bodyType = RigidbodyType2D.Static;
                /*Debug.Log("SpearShouldBeStuck");
                spearRidgidBody.bodyType = RigidbodyType2D.Static;*/
            }
        }
    }
    
}
