using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float jumpSpeed = 8.5f;

    Rigidbody2D thisRigidBody;
    Animator myAnimator;
    BoxCollider2D myBoxCollider;
    PolygonCollider2D myPolygonFeet;

    float gravityScale;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myPolygonFeet = GetComponent<PolygonCollider2D>();

        gravityScale = thisRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
        Climb();
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
}
