using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float blastRadius = 1.5f;
    //[SerializeField] Vector2 blastForce = new Vector2(10000f, 0f);
    [SerializeField] AudioClip burningSFX, explodeSFX;

    Animator myAnimator;
    AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myAudioSource = GetComponent<AudioSource>();
    }

    void ExplodeBomb()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, blastRadius, LayerMask.GetMask("Player"));
        myAudioSource.PlayOneShot(explodeSFX);

        if (playerCollider)
        {
            //playerCollider.GetComponent<Rigidbody2D>().AddForce(blastForce);
            playerCollider.GetComponent<Player>().PlayerHit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        myAudioSource.PlayOneShot(burningSFX);
        myAnimator.SetTrigger("Burn");
    }

    void DestroyBomb()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }
}
