using UnityEngine;

namespace Game.Entities
{
    public class GoalBone : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == Tags.Player)
            {
                Debug.Log($"[{name}] Colliding with player");
            }
        }
    }
}