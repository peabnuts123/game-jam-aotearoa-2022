using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class PlayerHealthDisplayController : MonoBehaviour
    {
        // Public references
        [NotNull]
        [SerializeField]
        private GameObject UiHeartPrefab;

        // Private references
        [Inject]
        private IInstantiator Container;

        public void SetNumberOfLives(int newNumLives)
        {
            // Destroy all hearts
            foreach (Transform child in this.transform)
            {
                Destroy(child.gameObject);
            }

            // Create new hearts
            for (int i = 0; i < newNumLives; i++)
            {
                Container.InstantiatePrefab(UiHeartPrefab, this.transform);
            }
        }
    }
}