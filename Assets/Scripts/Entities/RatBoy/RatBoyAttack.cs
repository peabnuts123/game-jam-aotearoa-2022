using Game.Config;
using UnityEngine;

namespace Game.Entities
{
    // @NOTE very similar to `OnPlayerCollide`
    [RequireComponent(typeof(Collider2D))]
    public class RatBoyAttack : MonoBehaviour
    {
        // Public references
        [NotNull]
        [SerializeField]
        private RatBoyBehaviour ratBoy;

        void OnTriggerEnter2D(Collider2D other)
        {
            // If player, call callbacks
            if (other.tag == Tags.Player)
            {
                ratBoy.OnAttackPlayer(other.GetComponent<PlayerController>());
            }
        }
    }
}