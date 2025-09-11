using UnityEngine;
using System.Collections;

public class BombBehavior : MonoBehaviour
{
    public float explosionRadius;
    public float explosionAnimationDuration;
    public float timeBetweenChargeAndExplosion;
    private Animator anim;

    public AudioSource bombAudioSource;
    public AudioClip explosionAudio;

    //public Collider2D blastRadius;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Electric Wall"))
        {
            //anim.SetBool("Grounded", isGrounded);
            StartCoroutine(Explode());
        }
    }
    //Bomb will only explode if it passes through the wall and gets charged
    IEnumerator Explode()
    {
        //Change Sprite Here 

        //Wait
        yield return new WaitForSeconds(timeBetweenChargeAndExplosion);

        //Change Sprite to Explosion

        //Delete self and all explodable objects withing range
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        


        anim.SetBool("Explode", true);

        //Let Explosion cover sprites that will be destroyed
        yield return new WaitForSeconds(explosionAnimationDuration * .5f);

        bombAudioSource.PlayOneShot(explosionAudio);

        yield return new WaitForSeconds(explosionAnimationDuration * .5f);
        yield return new WaitForSeconds(explosionAnimationDuration * .5f);
        yield return new WaitForSeconds(explosionAnimationDuration * .5f);

        foreach (Collider2D X in hits)
        {
            if (X.gameObject.CompareTag("Crate") || X.gameObject.CompareTag("Big Rock"))
            {
                Destroy(X.gameObject);
            }
        }

        Destroy(transform.parent.gameObject, 1f);
    }

}
