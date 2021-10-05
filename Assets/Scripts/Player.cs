using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float jumpSpeed = 8.5f;
    [SerializeField] float attackRadius = 1f;
    [SerializeField] Vector2 knockBack = new Vector2(50f, 50f);
    [SerializeField] Transform hurtBox; 
    [SerializeField] AudioClip jumpingSFX, attackingSFX, getHitSFX, walkingSFX;

    Rigidbody2D thisRigidBody;
    Animator myAnimator;
    BoxCollider2D myBoxCollider;
    PolygonCollider2D myPolygonFeet;
    AudioSource myAudioSource;

    float gravityScale;
    bool frozen = false;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myPolygonFeet = GetComponent<PolygonCollider2D>();
        myAudioSource = GetComponent<AudioSource>();

        gravityScale = thisRigidBody.gravityScale;

        if(FindObjectOfType<Minion>())
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), FindObjectOfType<Minion>().GetComponent<BoxCollider2D>());
        }

        myAnimator.SetTrigger("Door Out");
    }

    // Update is called once per frame
    void Update()
    {
        if(!frozen)
        {
            Run();
            Jump();
            Climb();
            Attack();
            EnterDoor();

            if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
            {
                PlayerHit();
            }
        }
    }

    private void EnterDoor()
    {
        bool isEntering = CrossPlatformInputManager.GetButtonDown("Vertical");
        bool isTouching = myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Interactables"));

        if (isEntering && isTouching)
        {
            myAnimator.SetTrigger("Door In");
            
        }
    }

    private void LoadNextLevel()
    {
        FindObjectOfType<ExitDoor>().StartLoadingNextLevel();
        TurnOffRenderder();
    }

    private void TurnOffRenderder()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Attack()
    {
        bool isAttacking = CrossPlatformInputManager.GetButtonDown("Fire1");

        if(isAttacking)
        {
            myAnimator.SetTrigger("Attacking");
            myAudioSource.PlayOneShot(attackingSFX);
            Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(hurtBox.position, attackRadius, LayerMask.GetMask("Enemy"));

            foreach(Collider2D enemy in enemiesInRange)
            {
                var identitfier = enemy.name.Substring(0, 3);

                Debug.Log(enemy.name);
                switch (identitfier)
                {
                    case "Ene":
                        enemy.GetComponent<Enemy>().Hit();
                        break;
                    case "Min":
                        enemy.GetComponent<Minion>().Hit();
                        break;
                }
            }
        }
    }

    public void PlayerHit()
    {
        thisRigidBody.velocity = knockBack * new Vector2(-transform.localScale.x, 1f);
        myAudioSource.PlayOneShot(getHitSFX);
        myAnimator.SetTrigger("Knock Back");
        frozen = true;

        StartCoroutine(Freeze());

        FindObjectOfType<GameSession>().PlayerDeath();
    }

    IEnumerator Freeze()
    {
        yield return new WaitForSeconds(2f);
        frozen = false;
    }

    private void Climb()
    {
        bool isClimbing = myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Drapes"));

        if (isClimbing)
        {
            float controlFlow = CrossPlatformInputManager.GetAxis("Vertical");
            Vector2 climbingVelocity = new Vector2(thisRigidBody.velocity.x, controlFlow * climbSpeed);
            thisRigidBody.velocity = climbingVelocity;

            thisRigidBody.gravityScale = 0f;
        }
        else
        {
            thisRigidBody.gravityScale = gravityScale;
        }

        myAnimator.SetBool("Climbing", isClimbing);
    }

    private void Jump()
    {
        bool isJumping = CrossPlatformInputManager.GetButtonDown("Jump");
        bool onGround = myPolygonFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if (isJumping && onGround)
        {
            Vector2 jumpVelocity = new Vector2(thisRigidBody.velocity.x, jumpSpeed);
            thisRigidBody.velocity = jumpVelocity;

            myAudioSource.PlayOneShot(jumpingSFX);
        }
    }

    private void Run()
    {
        float controlFlow = CrossPlatformInputManager.GetAxis("Horizontal");

        Vector2 playerVelocity = new Vector2(controlFlow * runSpeed, thisRigidBody.velocity.y);
        thisRigidBody.velocity = playerVelocity;
        FlipSprite();
        ChangingToRunningState();
    }

    private void ChangingToRunningState()
    {
        bool horizontalRun = Mathf.Abs(thisRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", horizontalRun);
    }

    private void FlipSprite()
    {
        bool horizontalRun = Mathf.Abs(thisRigidBody.velocity.x) > Mathf.Epsilon;
        if (horizontalRun)
        {
            transform.localScale = new Vector2(Mathf.Sign(thisRigidBody.velocity.x), 1f);
        }
    }

    void WalkingSFX()
    {
        bool horizontalRun = Mathf.Abs(thisRigidBody.velocity.x) > Mathf.Epsilon;

        if(horizontalRun)
        {
            if (myPolygonFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                myAudioSource.PlayOneShot(walkingSFX);
            }
        }
        else
        {
            myAudioSource.Stop();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(hurtBox.position, attackRadius);
    }
}
