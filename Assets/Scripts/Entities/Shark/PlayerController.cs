using Game.Components;
using UnityEngine;
using Zenject;

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

        // Private references
        [Inject]
        private Rigidbody2D rigidBody;

        // Private state
        private bool invulnerable = false;
        private bool frozen = false;

        void Update()
        {
            float hMovement = Input.GetAxis("Horizontal");

            if (!frozen)
            {
                // Update "movable" to move the player
                movable.SetHorizontalVelocityPercentage(hMovement);
            }

            if (jumpable.IsGrounded())
            {
                if (Mathf.Abs(hMovement) > 0.01F)
                {
                    // Player is moving
                    animator.SetAnimation("shark_run");
                }
                else
                {
                    // Player is NOT moving
                    if (Input.GetButton("Bark"))
                    {
                        animator.SetAnimation("shark_bark");
                    }
                    else
                    {
                        animator.SetAnimation("shark_idle");
                    }
                }
            }
            else
            {
                animator.SetAnimation("shark_jump");
            }

            if (Input.GetButtonDown("Jump"))
            {
                jumpable.AttemptJump();
                animator.SetAnimation("shark_jump", interruptSelf: true);
            }
        }

        public void OnAttacked(GameObject other)
        {
            if (!invulnerable)
            {
                rigidBody.AddForce((transform.position - other.transform.position + Vector3.up).WithLength(2000), ForceMode2D.Impulse);
                animator.SetAnimation("shark_damage", layer: 1, interruptable: false);

                // I frames
                invulnerable = true;
                frozen = true;
                StartCoroutine(this.Timeout(1, () =>
                {
                    invulnerable = false;
                    animator.SetAnimation("default", layer: 1);
                }));
                StartCoroutine(this.Timeout(0.5F, () =>
                {
                    frozen = false;
                }));
            }
        }

        public void OnBonkEnemy()
        {
            Debug.Log($"[{name}] Bonk'd a guy");
            jumpable.AttemptJump();
            animator.SetAnimation("shark_jump", interruptSelf: true);
        }
    }
}