using Game.Components;
using UnityEngine;

namespace Game.Entities
{
    public class PlayerController : MonoBehaviour
    {
        // Public references
        [NotNull]
        [SerializeField]
        private BetterAnimator animator;
        [NotNull]
        [SerializeField]
        private Movable movable;
        [NotNull]
        [SerializeField]
        private Jumpable jumpable;

        void Update()
        {
            float hMovement = Input.GetAxis("Horizontal");

            // Update "movable" to move the player
            movable.SetHorizontalVelocityPercentage(hMovement);

            if (Mathf.Abs(hMovement) > 0.01F)
            {
                // Player is moving
                animator.SetAnimation("shark_run");
            } else {
                // Player is NOT moving
                animator.SetAnimation("shark_idle");
            }

            if (Input.GetButtonDown("Jump"))
            {
                jumpable.AttemptJump();
            }
        }
    }
}