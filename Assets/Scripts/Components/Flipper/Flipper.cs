using UnityEngine;

public class Flipper : MonoBehaviour
{
    // Public config
    public Direction _facingDirection = Direction.Right;
    public Transform[] exemptChildren;


    public void Flip()
    {
        switch (FacingDirection)
        {
            case Direction.Left:
                FacingDirection = Direction.Right;
                break;
            case Direction.Right:
                FacingDirection = Direction.Left;
                break;
            default:
                throw new System.NotImplementedException("Cannot flip flipper. Unimplemented Direction: " + this.FacingDirection);
        }
    }

    public bool IsFacing(Transform transform)
    {
        if (this.transform.position.x > transform.position.x)
        {
            // We are to the right of other
            return FacingDirection == Direction.Left;
        }
        else if (this.transform.position.x < transform.position.x)
        {
            // We are to the left of other
            return FacingDirection == Direction.Right;
        }
        else
        {
            // Exactly horizontally aligned, lol.
            return false;
        }
    }

    private void FlipTransform(Transform transform)
    {
        // Context-free flip
        // @TODO make this based on `FacingDirection`
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public Direction FacingDirection
    {
        get
        {
            return _facingDirection;
        }
        set
        {
            if (_facingDirection != value)
            {
                // Our direction is changing,
                //  we need to flip everything
                FlipTransform(this.transform);

                foreach (Transform exemptTransform in exemptChildren)
                {
                    FlipTransform(exemptTransform);
                }
            }

            // Store the new value
            _facingDirection = value;
        }
    }
}