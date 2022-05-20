using UnityEngine;
using Zenject;

public class Movable : MonoBehaviour
{
    // Config
    public float topSpeed = 3;

    // Public references
    [NotNull]
    [SerializeField]
    private Flipper flipper;

    // Private references
    [Inject]
    private Rigidbody2D rigidBody;

    public void SetHorizontalVelocity(float horizontalVelocity, bool bypassFlipper = false)
    {
        // Update flipper
        if (!bypassFlipper && flipper != null && Mathf.Abs(horizontalVelocity) > 0.1F)
        {
            if (horizontalVelocity < 0)
            {
                flipper.FacingDirection = Direction.Left;
            }
            else
            {
                flipper.FacingDirection = Direction.Right;
            }
        }

        // Update velocity
        rigidBody.velocity = new Vector2(horizontalVelocity, rigidBody.velocity.y);
    }
    public void SetVerticalVelocity(float verticalVelocity)
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, verticalVelocity);
    }
    public void SetVelocity(float horizontalVelocity, float verticalVelocity, bool bypassFlipper = false)
    {
        // Update flipper
        if (!bypassFlipper && flipper != null && Mathf.Abs(horizontalVelocity) > 0.1F)
        {
            if (horizontalVelocity < 0)
            {
                flipper.FacingDirection = Direction.Left;
            }
            else
            {
                flipper.FacingDirection = Direction.Right;
            }
        }

        // Update velocity
        rigidBody.velocity = new Vector2(horizontalVelocity, verticalVelocity);
    }

    public void SetHorizontalVelocityPercentage(float horizontalVelocityPercentage, bool bypassFlipper = false)
    {
        // Update flipper
        if (!bypassFlipper && flipper != null && Mathf.Abs(horizontalVelocityPercentage) > 0.03F)
        {
            if (horizontalVelocityPercentage < 0)
            {
                flipper.FacingDirection = Direction.Left;
            }
            else
            {
                flipper.FacingDirection = Direction.Right;
            }
        }

        // Update velocity
        rigidBody.velocity = new Vector2(horizontalVelocityPercentage * topSpeed, rigidBody.velocity.y);
    }
    public void SetVerticalVelocityPercentage(float verticalVelocityPercentage)
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, verticalVelocityPercentage * topSpeed);
    }
    public void SetVelocityPercentage(float horizontalVelocityPercentage, float verticalVelocityPercentage, bool bypassFlipper = false)
    {
        // Update flipper
        if (!bypassFlipper && flipper != null && Mathf.Abs(horizontalVelocityPercentage) > 0.03F)
        {
            if (horizontalVelocityPercentage < 0)
            {
                flipper.FacingDirection = Direction.Left;
            }
            else
            {
                flipper.FacingDirection = Direction.Right;
            }
        }

        // Update velocity
        rigidBody.velocity = new Vector2(horizontalVelocityPercentage, verticalVelocityPercentage) * topSpeed;
    }

    public float GetMaximumHorizontalVelocity()
    {
        return topSpeed;
    }

    public float GetHorizontalVelocity()
    {
        return rigidBody.velocity.x;
    }

    public float GetVerticalVelocity()
    {
        return rigidBody.velocity.y;
    }

    public Vector2 GetVelocity()
    {
        return rigidBody.velocity;
    }

    public float GetHorizontalVelocityPercentage()
    {
        return rigidBody.velocity.x / topSpeed;
    }

    public RaycastHit2D[] CastAll(Vector2 direction, float distance = float.PositiveInfinity, int maxResults = 10)
    {
        RaycastHit2D[] hitResults = new RaycastHit2D[maxResults];
        int numResults = rigidBody.Cast(direction, hitResults, distance);
        RaycastHit2D[] returnResults = new RaycastHit2D[numResults];
        for (int i = 0; i < returnResults.Length; i++)
        {
            returnResults[i] = hitResults[i];
        }

        return returnResults;
    }

    public RaycastHit2D Cast(Vector2 direction, float distance = float.PositiveInfinity)
    {
        RaycastHit2D[] results = new RaycastHit2D[1];
        rigidBody.Cast(direction, results, distance);
        return results[0];
    }
}