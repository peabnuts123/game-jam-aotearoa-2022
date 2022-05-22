using Game.Config;
using UnityEngine;

namespace Game.Entities
{
    public class CatGhostPursuit : MonoBehaviour
    {
        // Public references
        [NotNull]
        [SerializeField]
        private CatGhostBehaviour catGhost;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == Tags.Player)
            {
                catGhost.Pursue(other.transform);
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == Tags.Player)
            {
                catGhost.AbandonPursuit();
            }
        }

    }
}