using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public Collider2D triggerCollider;
    public float launchForce =10;

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") || other.CompareTag("Spear") || other.CompareTag("Crate") || other.CompareTag("Bomb"))
        {
            Rigidbody2D rb = other.GetComponentInParent<Rigidbody2D>();
            if (rb != null)
            {
                // Reset vertical velocity
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); 
                //Launch Player
                rb.AddForce(Vector2.up * launchForce, ForceMode2D.Impulse);
            }
         }
    }
}
