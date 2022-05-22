using UnityEngine;

namespace Game.Entities
{
    public class TimeController : MonoBehaviour
    {
        // Private constants
        private const float DAY_START_TIME = 7F;
        private const float NIGHT_START_TIME = 19F;

        // Public events
        public delegate void DayTimeCallback();
        public delegate void NightTimeCallback();
        public event DayTimeCallback OnDayTime;
        public event NightTimeCallback OnNightTime;

        // Private config
        private static readonly float DAY_LENGTH_TIME = 24;

        // Public config
        [SerializeField]
        private int DAY_CYCLE_LENGTH_SECONDS = 120;

        // Private state
        [SerializeField] // @DEBUG
        private float currentTime = 7F;

        void Start()
        {
            if (currentTime >= DAY_START_TIME && currentTime < NIGHT_START_TIME)
            {
                if (OnDayTime != null)
                {
                    Debug.Log($"[{name}] Firing initial onDayTime() callbacks");
                    OnDayTime();
                }
            }
            else
            {
                if (OnNightTime != null)
                {
                    Debug.Log($"[{name}] Firing initial onNightTime() callbacks");
                    OnNightTime();
                }
            }
        }

        void FixedUpdate()
        {
            var previousTime = currentTime;
            currentTime += Time.fixedDeltaTime * DAY_LENGTH_TIME / DAY_CYCLE_LENGTH_SECONDS;

            if (previousTime < DAY_START_TIME && currentTime >= DAY_START_TIME)
            {
                if (OnDayTime != null)
                {
                    Debug.Log($"[{name}] Firing onDayTime() callbacks");
                    OnDayTime();
                }
            }
            else if (previousTime < NIGHT_START_TIME && currentTime >= NIGHT_START_TIME)
            {
                if (OnNightTime != null)
                {
                    Debug.Log($"[{name}] Firing onNightTime() callbacks");
                    OnNightTime();
                }
            }

            currentTime = currentTime % DAY_LENGTH_TIME;
        }

        // Properties
        public float CurrentTime => currentTime;

        public float CurrentTimePercentage => currentTime / DAY_LENGTH_TIME;
    }
}