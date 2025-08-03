using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip backgroundMusic;
    public AudioSource source;
    private float TimeToStart = 0;

    void Update()
    {
        if(TimeToStart <= Time.time)
        {
            source.clip = backgroundMusic;
            source.Play();
            TimeToStart = Time.time + backgroundMusic.length;
        }
    }

}
