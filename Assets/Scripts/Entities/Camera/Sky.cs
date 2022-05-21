using UnityEngine;
using Zenject;

namespace Game.Entities
{
    public class Sky : MonoBehaviour
    {
        // Private config
        public const float TIME_SUN_START_SETTING_MOON_START_RISING = 14F / 24F;
        public const float TIME_SUN_START_PEAK = 12F / 24F;
        public const float TIME_MOON_START_SETTING_SUN_START_RISING = 2F / 24F;
        public const float TIME_MOON_START_PEAK = 24F/24F;

        // Public config
        [SerializeField]
        private Color daytimeBackgroundColor;
        [SerializeField]
        private Color nighttimeBackgroundColor;

        // Public references
        [NotNull]
        [SerializeField]
        private Camera camera;
        [NotNull]
        [SerializeField]
        private Transform moon;
        [NotNull]
        [SerializeField]
        private Transform sun;
        [NotNull]
        [SerializeField]
        private Transform celestialApogeeTransform;


        // Private references
        [Inject]
        private TimeController timeController;

        void Update()
        {
            if (timeController.CurrentTimePercentage >= TIME_SUN_START_SETTING_MOON_START_RISING)
            {
                // Sun setting, moon rising
                float t = Mathf.InverseLerp(TIME_SUN_START_SETTING_MOON_START_RISING, TIME_MOON_START_PEAK, timeController.CurrentTimePercentage);
                moon.transform.localPosition = TValueToPosition(t);
                sun.transform.localPosition = TValueToPosition(1 - t);

                if (t > 0.5F)
                {
                    // Moon rising
                    camera.backgroundColor = nighttimeBackgroundColor;
                }
            }
            else if (timeController.CurrentTimePercentage >= TIME_SUN_START_PEAK)
            {
                // Sun peaking
                sun.localPosition = CelestialApogee;
                moon.localPosition = CelestialPerigee;
            }
            else if (timeController.CurrentTimePercentage >= TIME_MOON_START_SETTING_SUN_START_RISING)
            {
                // Moon setting, sun rising
                float t = Mathf.InverseLerp(TIME_MOON_START_SETTING_SUN_START_RISING, TIME_SUN_START_PEAK, timeController.CurrentTimePercentage);
                sun.transform.localPosition = TValueToPosition(t);
                moon.transform.localPosition = TValueToPosition(1 - t);

                if (t > 0.5F)
                {
                    // Sun rising
                    camera.backgroundColor = daytimeBackgroundColor;
                }
            }
            else if (timeController.CurrentTimePercentage >= TIME_MOON_START_PEAK)
            {
                // Moon peaking
                // @NOTE this is kind of hanging together because the `else` statement doesn't need to check
                //  between 24H and 2H which would not quite work because 24 is greater than 2 and would require
                //  some kind of "modulus-based lerp"
                // But because this condition is handled by the `else` it works without extra logic 👍
                moon.localPosition = CelestialApogee;
                sun.localPosition = CelestialPerigee;
            }
        }

        /// <summary>
        /// Lerp between perigee and apogee based on `t`
        /// </summary>
        /// <param name="t">Lerp value. 1 = Full apogee. 0 = Full perigee.</param>
        private Vector3 TValueToPosition(float t) => Vector3.Lerp(CelestialPerigee, CelestialApogee, t);

        // Properties
        private Vector3 CelestialApogee => celestialApogeeTransform.localPosition;
        private float HorizonY => -1.5F * camera.orthographicSize;
        private Vector3 CelestialPerigee => CelestialApogee.WithY(
            // 1 dimensional vector math
            // Twice the vector from `celestialApogee` to `horizonY`
            //  added to the current position
            CelestialApogee.y +
            (HorizonY - CelestialApogee.y) * 2
        );
    }
}