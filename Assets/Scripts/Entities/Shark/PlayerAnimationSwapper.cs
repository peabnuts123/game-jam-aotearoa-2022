using UnityEditor.Animations;
using UnityEngine;
using Zenject;

namespace Game.Entities
{
    public class PlayerAnimationSwapper : MonoBehaviour
    {
        // Public references
        [NotNull]
        [SerializeField]
        private AnimatorController dayController;
        [NotNull]
        [SerializeField]
        private AnimatorController nightController;
        [NotNull]
        [SerializeField]
        private Animator animator;

        // Private references
        [Inject]
        private TimeController timeController;

        void Start()
        {
            timeController.OnDayTime += OnDayTime;
            timeController.OnNightTime += OnNightTime;
        }

        private void OnDayTime()
        {
            animator.runtimeAnimatorController = dayController;
        }
        private void OnNightTime()
        {
            animator.runtimeAnimatorController = nightController;
        }
    }
}