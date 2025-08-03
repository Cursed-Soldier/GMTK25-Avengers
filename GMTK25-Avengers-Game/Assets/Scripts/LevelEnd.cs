using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public Timer timer;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            timer.StopTimer();
        }
    }
}
