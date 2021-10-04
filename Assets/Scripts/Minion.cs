using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    [SerializeField] float runSpeed = 3f, blastRadius = 1.5f;
    [SerializeField] AudioClip burningSFX, explodeSFX;
    [SerializeField] Vector2 detectBoxSize = new Vector2(3.5f, 0.5f);

    Rigidbody2D thisRigidBody;
    Animator myAnimator;
    AudioSource myAudioSource;

    private bool engage = false, flip = true;
    private Collider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(engage)
        {
            Movement();
        }

        if (flip)
        {
            playerCollider = Physics2D.OverlapBox(new Vector3(transform.position.x + 1.5f*-1f, transform.position.y, 0f), detectBoxSize, LayerMask.GetMask("Player"));
            flip = false;
            StartCoroutine(FlipSprite());
        }
    }

    IEnumerator FlipSprite()
    {
        yield return new WaitForSeconds(3f);
        flip = true;
        if (!engage)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, 1f);
        }
    }

    private void Movement()
    {
        bool isFacingLeft = transform.localScale.x < 0;
        myAnimator.SetTrigger("Run");

        if (isFacingLeft)
        {
            thisRigidBody.velocity = new Vector2(-runSpeed, 0f);
        }
        else
        {
            thisRigidBody.velocity = new Vector2(runSpeed, 0f);
        }
    }

    void ExplodeMinion()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, blastRadius, LayerMask.GetMask("Player"));
        myAudioSource.PlayOneShot(explodeSFX);

        if (playerCollider)
        {
            //playerCollider.GetComponent<Rigidbody2D>().AddForce(blastForce);
            //playerCollider.GetComponent<Player>().PlayerHit();
        }
    }

    void DestroyMinion()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, blastRadius);
        Gizmos.DrawWireCube(new Vector3(transform.position.x + 1.5f, transform.position.y, 0f), detectBoxSize);
    }
}
