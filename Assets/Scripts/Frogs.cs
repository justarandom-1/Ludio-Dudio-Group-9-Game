using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frogs : Enemies
{
    public float patrolSpeed = 2f;       // Speed of the frog's horizontal movement (distance per jump).
    public float jumpHeight = 0.5f;        // Maximum height of the jump.
    public float jumpDuration = 1f;      // Time it takes for the frog to complete the jump.
    public float jumpCooldown = 1f;      // Time between consecutive jumps.
    public int maxJumpsBeforeTurning = 3; // Number of jumps before turning direction.

    private Vector3 startPosition;       // Starting position of the frog.
    private float currentX;              // Current X position of the frog after each jump.
    private bool isJumping = false;      // Is the frog currently jumping?
    private float jumpStartTime;         // The time when the jump started.
    private float jumpProgress = 0f;     // Progress of the jump (from 0 to 1).
    private int jumpCount = 0;           // Track number of jumps made in current direction.

    private Animator animator;
    private float lastJumpTime;
    private bool movingRight = true;     // Direction flag: true = moving right, false = moving left
    


    private void Start()
    {
        // Get required components
        animator = GetComponent<Animator>();

        // Initialize the starting position for the frog
        startPosition = transform.position;
        currentX = startPosition.x; // Start from the initial position
    }

    // Update is called once per frame
    new void Update()
    {
        if (!isDead)
        {
            base.Update();  // Call the base class Update to handle attacks
            
            // Only perform patrol logic when the frog is not already jumping and cooldown is over
            if (!isJumping && Time.time - lastJumpTime >= jumpCooldown)
            {
                Patrol();
            }
            
            // Handle jump progress when jumping
            if (isJumping)
            {
                JumpProgress();
            }
        }
        
    }
    
    

    // Patrolling: move by jumping back and forth for a set number of jumps
    private void Patrol()
    {
        // Trigger the jump and apply a force to simulate a jump
        Jump();

        // Update last jump time so the frog doesn't jump immediately again
        lastJumpTime = Time.time;
    }

    // Handle the jump logic: simulate a parabolic movement by manually updating position
    private void Jump()
    {
        // Trigger the jump animation
        animator.SetBool("isJumping", true);
        

        // Set the frog to the jumping state
        isJumping = true;

        // Record the start time of the jump
        jumpStartTime = Time.time;

        // Reset jump progress (for when the jump completes)
        jumpProgress = 0f;
    }

    // Handle jump progress using a sine function for smooth parabolic movement
    private void JumpProgress()
    {
        // Calculate how far through the jump we are (from 0 to 1)
        jumpProgress = (Time.time - jumpStartTime) / jumpDuration;

        // If the jump is completed, stop jumping and reverse direction if needed
        if (jumpProgress >= 1f)
        {
            OnLand();
            return;
        }

        // Calculate horizontal movement (based on patrolSpeed and direction)
        float horizontalMovement = patrolSpeed * jumpProgress;

        // Calculate the vertical position using a sine curve for the parabolic jump
        float verticalMovement = Mathf.Sin(jumpProgress * Mathf.PI) * jumpHeight;  // Sine curve for smooth jump arc
        
        FlipSprite();
        
        transform.position = new Vector3(currentX + horizontalMovement * (movingRight ? 1 : -1), 
            startPosition.y + verticalMovement, 
            startPosition.z);
    }

    // This function will be called when the frog lands (or completes its jump)
    public void OnLand()
    {
        // Once the frog lands, set isJumping to false and allow another jump
        isJumping = false;
        animator.SetBool("isJumping", false);

        // Increment the jump counter
        jumpCount++;

        // Update current X position to reflect the last jump's position
        currentX = transform.position.x;
        
        // Check if the frog has reached the max jumps, and reverse direction if necessary
        if (jumpCount >= maxJumpsBeforeTurning)
        {
            ReverseDirection();
        }

        // Update last jump time after landing
        lastJumpTime = Time.time;
    }

    // Flips the frog's sprite based on its movement direction
    private void FlipSprite()
    {
        Vector3 localScale = transform.localScale;
        localScale.x = !movingRight ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
        transform.localScale = localScale;
    }

    // Reverses the frog's patrol direction after a set number of jumps
    private void ReverseDirection()
    {
        // Reset jump counter after turning
        jumpCount = 0;

        // Reverse the patrol direction
        movingRight = !movingRight;
        
        FlipSprite();
    }

    // Optionally, override the PerformAttack method if the frog has a special attack
    public override void Attack()
    {
        base.Attack();
        // Frog-specific attack (e.g., jumping attack or damage on landing)
        Debug.Log("Frog enemy attacks with a jump!");
    }

    public override BoostType GetBoostType()
    {
         return BoostType.Health;
    }
    
    // Override die method to have frog-specific death behavior
    protected override void Die()
    {
        base.Die();
        animator.SetBool("isDead", true);
        GetBoostType();
        Debug.Log("Frog enemy has died!");
    }
}