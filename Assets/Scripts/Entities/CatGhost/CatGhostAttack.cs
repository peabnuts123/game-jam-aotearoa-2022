using Game.Config;
using UnityEngine;

namespace Game.Entities
{
    // @NOTE very similar to `OnPlayerCollide`
    [RequireComponent(typeof(Collider2D))]
    public class CatGhostAttack : MonoBehaviour
    {
        // Public references
        [NotNull]
        [SerializeField]
        private CatGhostBehaviour catGhost;

        void OnTriggerEnter2D(Collider2D other)
        {
            // If player, call callbacks
            if (other.tag == Tags.Player)
            {
                catGhost.OnAttackPlayer(other.GetComponent<PlayerController>());
            }
        }
    }
}