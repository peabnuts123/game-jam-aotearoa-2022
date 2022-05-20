using System;
using UnityEngine;
using Zenject;

public class Jumpable : MonoBehaviour
{
    // Constants
    private const int MAX_RAYCAST_RESULTS = 3;

    // Private references
    [Inject]
    private Rigidbody2D rigidBody;

    // Internal State
    private bool isOnGround;
    private bool isJumpRequested;
    // Used for raycasting the ground, declared once for performance
    private RaycastHit2D[] raycastResults;

    // Config
    public float jumpVelocity = 5F;
    public float fallGravity = 2.5F;
    [NotNull]
    public Collider2D groundCollider;

    void Start()
    {
        // Ignore collision between rigidBody of this Jumper object and its groundCollider
        this.rigidBody.IgnoreCollision(groundCollider, true);

        // Used for raycasting the ground, declared once for performance
        raycastResults = new RaycastHit2D[MAX_RAYCAST_RESULTS];
    }

    public void AttemptJump()
    {
        if (!isJumpRequested && (IsGrounded()))
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
        RaycastHit2D hitInfo = RaycastGround();
        return hitInfo.collider;
    }

    private RaycastHit2D RaycastGround()
    {
        int numResults = groundCollider.Cast(Vector2.down, raycastResults, groundCollider.bounds.extents.y);

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
        isOnGround = RaycastGround();

        // If the player has pressed jump, perform a jump
        if (isJumpRequested)
        {
            isJumpRequested = false;
            isOnGround = false;

            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpVelocity);
        }

        // Apply more gravity when falling for a snappier jump feel
        if (rigidBody.velocity.y < 0)
        {
            rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallGravity - 1) * Time.fixedDeltaTime;
        }
    }
}