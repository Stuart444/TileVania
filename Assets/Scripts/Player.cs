using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    // Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;

    // State
    bool isAlive = true;
    bool isJumping = false;

    // Cached component references
    Rigidbody2D myRigidbody;
    Animator myAnimator;

    // Messages & Methods
    void Start ()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Run();
        Jump();
        FlipSprite();
    }

    private void Run()
    {
        // Value is between -1 to +1
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");

        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        print(playerVelocity);

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        myAnimator.SetBool("Running", playerHasHorizontalSpeed);

    }

    // Project Settings -> Physics 2D and use Gravity Speed 
    // along with our variable jumpSpeed to tune our jump
    // and our fall speed (aka Gravity)
    private void Jump()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 velocityJumpToAdd = new Vector2(0f, jumpSpeed);
            myRigidbody.velocity += velocityJumpToAdd;
        }
    }

    private void FlipSprite()
    {
        // if Velocity is greater than Zero (Epsilon) then true
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            // returns 1 if x is positive or zero otherwise it returns -1
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
}
