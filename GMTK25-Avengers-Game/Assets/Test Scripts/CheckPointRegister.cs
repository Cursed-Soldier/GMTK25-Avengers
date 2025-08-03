using UnityEngine;

public class CheckPointRegister : MonoBehaviour
{
    
    private RestartController rcont;

    void Start()
    {
        rcont = FindObjectOfType<RestartController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            rcont.spawnPoint = other.transform.position;
        }
    }
}
