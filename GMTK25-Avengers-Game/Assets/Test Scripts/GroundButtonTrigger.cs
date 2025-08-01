using UnityEngine;

public class GroundButtonTrigger : MonoBehaviour
{
    public float requireMassToTrigger;
    private float currentMassOnButton = 0;
    public bool isActive = false;

    public void Update()
    {
        if(currentMassOnButton >= requireMassToTrigger)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            currentMassOnButton += rb.mass;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            currentMassOnButton -= rb.mass;
        }
    }
    
}
