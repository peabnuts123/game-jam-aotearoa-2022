using Game.Config;
using UnityEngine;

namespace Game.Entities
{
    public class CoinBone : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == Tags.Player)
            {
                Debug.Log($"[{name}] Colliding with player");
                Destroy(gameObject);
            }
        }
    }
}