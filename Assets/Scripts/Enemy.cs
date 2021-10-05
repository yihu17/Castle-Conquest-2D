using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float runSpeed = 3f;
    [SerializeField] AudioClip deathSFX;

    Rigidbody2D thisRigidBody;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    public void Hit()
    {
        animator.SetTrigger("Death");
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        runSpeed = 0f;
    }

    private void Movement()
    {
        if (IsFacingLeft())
        {
            thisRigidBody.velocity = new Vector2(-runSpeed, 0f);
        }
        else
        {
            thisRigidBody.velocity = new Vector2(runSpeed, 0f);
        }
    }

    private bool IsFacingLeft()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FlipSprite();
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector2(Mathf.Sign(thisRigidBody.velocity.x), 1f);
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    void PlayDeathSFX()
    {
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position);
    }
}
