using System;
using UnityEngine;
using Zenject;

public class Jumpable : MonoBehaviour
{
    // Constants
    private const int MAX_RAYCAST_RESULTS = 3;

    // Public references
    [NotNull]
    [SerializeField]
    private Collider2D groundCollider;
    [NotNull]
    [SerializeField]
    private Collider2D jumpTestCollider;

    // Private references
    [Inject]
    private Rigidbody2D rigidBody;

    // Internal State
    private bool isOnGround;
    private bool canJump;
    private bool isJumpRequested;
    // Used for raycasting the ground, declared once for performance
    // private RaycastHit2D[] raycastResults;

    // Config
    public float jumpVelocity = 5F;
    public float fallGravity = 2.5F;


    void Start()
    {
        // Ignore collision between rigidBody of this Jumper object and its test colliders
        this.rigidBody.IgnoreCollision(groundCollider, true);
        this.rigidBody.IgnoreCollision(jumpTestCollider, true);

        // Used for raycasting the ground, declared once for performance
    }

    public void AttemptJump()
    {
        if (!isJumpRequested && canJump)
        {
            isJumpRequested = true;
        }
    }

    public bool IsGrounded()
    {
        return isOnGround;
    }

    public Collider2D GetCurrentGroundCollider()
    {
        RaycastHit2D hitInfo = RaycastGround(groundCollider);
        return hitInfo.collider;
    }

    private RaycastHit2D RaycastGround(Collider2D testCollider)
    {
        RaycastHit2D[] raycastResults = new RaycastHit2D[MAX_RAYCAST_RESULTS];
        int numResults = testCollider.Cast(Vector2.down, raycastResults, testCollider.bounds.extents.y / 2F);

        // Find the shortest result
        RaycastHit2D minResult = default(RaycastHit2D);
        float minResultDistance = float.MaxValue;
        for (int i = 0; i < numResults; i++)
        {
            if (!raycastResults[i].collider.isTrigger && raycastResults[i].distance < minResultDistance)
            {
                minResultDistance = raycastResults[i].distance;
                minResult = raycastResults[i];
            }
        }

        return minResult;
    }

    public float GetJumpVelocity()
    {
        return jumpVelocity;
    }

    void FixedUpdate()
    {
        // Test for the ground
        isOnGround = RaycastGround(groundCollider);
        canJump = RaycastGround(jumpTestCollider);

        // If the player has pressed jump, perform a jump
        if (isJumpRequested)
        {
            isJumpRequested = false;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpVelocity);
        }

        // Apply more gravity when falling for a snappier jump feel
        if (rigidBody.velocity.y < 0)
        {
            rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallGravity - 1) * Time.fixedDeltaTime;
        }
    }
}