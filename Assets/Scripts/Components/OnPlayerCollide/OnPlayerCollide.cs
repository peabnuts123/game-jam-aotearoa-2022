using Game.Config;
using Game.Entities;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Components
{
    [RequireComponent(typeof(Collider2D))]
    public class OnPlayerCollide : MonoBehaviour
    {
        // Public config
        [SerializeField]
        private UnityEvent onPlayerCollide;

        void OnTriggerEnter2D(Collider2D other)
        {
            // If player, call callbacks
            if (other.tag == Tags.Player)
            {
                if (onPlayerCollide != null)
                {
                    onPlayerCollide.Invoke();
                }
            }
        }
    }
}