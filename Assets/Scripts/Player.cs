using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    protected Rigidbody2D thisRigidBody;
    protected Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
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
