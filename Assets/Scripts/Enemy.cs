using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float runSpeed = 3f;

    Rigidbody2D thisRigidBody;
    BoxCollider2D myBoxCollider;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidBody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsFacingLeft())
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
}
