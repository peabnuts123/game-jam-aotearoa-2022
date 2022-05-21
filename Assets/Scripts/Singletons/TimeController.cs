using UnityEngine;

namespace Game.Entities
{
    public class TimeController : MonoBehaviour
    {
        // Private config
        private static readonly float DAY_LENGTH_TIME = 24;

        // Public config
        [SerializeField]
        private int DAY_CYCLE_LENGTH_SECONDS = 120;

        // Private state
        [SerializeField] // @DEBUG
        private float currentTime = 7F;

        void FixedUpdate()
        {
            currentTime += Time.fixedDeltaTime * DAY_LENGTH_TIME / DAY_CYCLE_LENGTH_SECONDS;
            currentTime = currentTime % DAY_LENGTH_TIME;
        }

        // Properties
        public float debug_CurrentTime => currentTime;

        public float CurrentTimePercentage => currentTime / DAY_LENGTH_TIME;
    }
}