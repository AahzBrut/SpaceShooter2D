using UnityEngine;

namespace Domain
{
    [CreateAssetMenu(menuName = "Data/PowerUpStat")]
    public class PowerUpStats : ScriptableObject
    {
        public float speed;
        public Rect moveBounds;
        public float duration;
        public PowerUpType powerUpType;
        public GameObject powerUpPrefab;
    }
}
