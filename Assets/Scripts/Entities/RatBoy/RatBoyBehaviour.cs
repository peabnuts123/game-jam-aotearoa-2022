using Game.Config;
using UnityEngine;

namespace Game.Entities
{
    public class RatBoyBehaviour : MonoBehaviour
    {
        // Public references
        [NotNull]
        [SerializeField]
        private Flipper flipper;
        [NotNull]
        [SerializeField]
        private Movable movable;

        // Private state
        private bool invulnerable = false;
        private bool dead = false;

        // Lifecycle
        void Update()
        {
            UpdateMovement();
        }

        // Functions
        public void TurnAround() /* bright eyes */
        {
            /* Every now and then I fall apart */
            flipper.Flip();
        }

        public void OnBonked()
        {
            if (!invulnerable)
            {
                Debug.Log($"[{name}] Got bonk'd!");
                Destroy(gameObject);
                dead = true;
            }
        }

        public void OnAttackPlayer(PlayerController player)
        {
            if (dead) return;
            Debug.Log($"[{name}] Attacking the boy!! {player.name}");
            player.OnAttacked(gameObject);

            // I frames for ratboy, can't get bonked IMMEDIATELY after attacking player
            invulnerable = true;
            StartCoroutine(this.Timeout(0.2F, () =>
            {
                invulnerable = false;
            }));
        }

        private void UpdateMovement()
        {
            switch (flipper.FacingDirection)
            {
                case Direction.Left:
                    // Begin moving left
                    movable.SetHorizontalVelocityPercentage(-1);
                    break;
                case Direction.Right:
                    // Begin moving right
                    movable.SetHorizontalVelocityPercentage(1);
                    break;
            }
        }
    }
}