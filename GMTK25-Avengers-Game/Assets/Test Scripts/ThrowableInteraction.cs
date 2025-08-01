using UnityEngine;

public class ThrowableInteraction: MonoBehaviour
{
    [HideInInspector]
    public Vector3 originalScale;


    // How strongly the player can throw each object
    public float throwForce = 10f;
    public Rigidbody2D rb;
    public bool isLargeItem;

    //Spear Offset to avoid collision when thrown
    public float SpearThrowExtendAmount;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
    }

    public void Throw(Vector2 direction)
    {
        transform.SetParent(null);
        rb.AddForce(direction.normalized * throwForce, ForceMode2D.Impulse);
    }

}
