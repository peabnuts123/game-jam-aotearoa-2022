using Game.Config;
using Game.UI;
using UnityEngine;
using Zenject;

namespace Game.Entities
{
    public class GoalBone : MonoBehaviour
    {
        // Private references
        [Inject]
        private GoalMenu goalMenu;
        [Inject]
        private PauseManager pauseManager;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == Tags.Player)
            {
                pauseManager.SetGameOver(true);
                goalMenu.gameObject.SetActive(true);
            }
        }
    }
}