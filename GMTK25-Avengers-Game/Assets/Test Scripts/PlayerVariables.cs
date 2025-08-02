using UnityEngine;

public class PlayerVariables : MonoBehaviour
{
    //A place to store player information used by multiple function/scripts
    [HideInInspector]
    public bool isHoldingItem = false;
    [HideInInspector]
    public bool isMovingWall = false;

    public AudioSource playerAudioSource;

    //Audio Clips Used By Player
    public AudioClip jumpSoundEffect;
    public AudioClip dragSoundEffect;
    public AudioClip throwSoundEffect;


    public void playJumpAudio()
    {
        playerAudioSource.PlayOneShot(jumpSoundEffect);
    }

    public void playWalkingAudio()
    {
        //playerAudioSource.PlayOneShot(
    }

    public void playThrowAudio()
    {
        playerAudioSource.PlayOneShot(throwSoundEffect);
    }

    public void playDragAudio()
    {
        playerAudioSource.clip = dragSoundEffect;
        playerAudioSource.Play();
    }

    public void stopAudio()
    {
        playerAudioSource.Stop();
    }

}
