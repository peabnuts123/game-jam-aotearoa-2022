using Game.Config;
using UnityEngine;

namespace Game.Entities
{
    public class RatBoyWallDetector : MonoBehaviour
    {
        // Public references
        [NotNull]
        [SerializeField]
        private RatBoyBehaviour ratBoy;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == Tags.Level)
            {
                ratBoy.TurnAround();
            }
        }
    }
}