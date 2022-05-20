using UnityEngine;
using Zenject;

namespace Game.Entities
{
    public class CameraMovement : MonoBehaviour
    {

        // Public config
        [NotNull]
        [SerializeField]
        private Transform target;

        // Private references
        private new Camera camera;

        // Private state
        private Vector3 targetPosition;

        void Start()
        {
            camera = GetComponent<Camera>();

            // Initialise Camera at target position
            UpdateTargetPosition();
            this.transform.position = targetPosition.WithZ(this.transform.position.z);
        }

        void Update()
        {
            ApproachTargetPosition();
        }

        void FixedUpdate()
        {
            UpdateTargetPosition();
        }

        void UpdateTargetPosition()
        {
            this.targetPosition = target.position;
        }

        void ApproachTargetPosition()
        {
            Vector2 delta = targetPosition - this.transform.position;
            float timeFactor = Time.deltaTime * 5;
            this.transform.Translate(delta * timeFactor);
        }
    }
}