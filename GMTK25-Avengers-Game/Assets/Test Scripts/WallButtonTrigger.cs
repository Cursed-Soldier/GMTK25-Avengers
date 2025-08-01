using UnityEngine;

public class WallButtonTrigger : MonoBehaviour
{
    public bool isActive = false;
    public float requiredMassToTrigger = 1;
    void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
        if(rb != null && rb.mass >= requiredMassToTrigger)
        {
            isActive = true;
        }
    }
}
