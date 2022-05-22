using UnityEngine;
using Zenject;

namespace Game.Entities
{
    public class CatGhostBehaviour : MonoBehaviour
    {
        // Public references
        [NotNull]
        [SerializeField]
        private Movable movable;

        // Private references
        [Inject]
        private TimeController timeController;

        // Private state
        Transform target;

        // Lifecycle
        void Awake()
        {
            timeController.OnDayTime -= OnDayTime;
            timeController.OnDayTime += OnDayTime;
            timeController.OnNightTime -= OnNightTime;
            timeController.OnNightTime += OnNightTime;
        }
        void Update()
        {
            if (target != null)
            {
                var delta = target.position - transform.position;
                movable.SetVelocityPercentage(Mathf.Min(delta.x, 1), Mathf.Min(delta.y, 1));
            }
            else
            {
                movable.SetVelocity(0, 0);
            }
        }

        // Functions
        public void OnAttackPlayer(PlayerController player)
        {
            Debug.Log($"[{name}] Attacking the boy!! {player.name}");
            player.OnAttacked(gameObject);
        }

        public void Pursue(Transform target)
        {
            this.target = target;
        }

        public void AbandonPursuit()
        {
            this.target = null;
        }

        private void OnDayTime()
        {
            Debug.Log($"[{name}] Cat ghost going offline");
            gameObject.SetActive(false);
        }
        private void OnNightTime()
        {
            Debug.Log($"[{name}] Cat ghost LOGGING IN");
            gameObject.SetActive(true);
        }
    }
}
