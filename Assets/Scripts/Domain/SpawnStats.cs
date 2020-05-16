using UnityEngine;

namespace Domain
{
    [CreateAssetMenu(menuName = "Data/SpawnStats")]
    public class SpawnStats : ScriptableObject
    {
        public float startDelay;
        public float enemySpawnRate;
        public CraftStats enemyStats;
        public float tripleShotSpawnRate;
        public PowerUpStats tripleShotStats;
        public float speedBoostSpawnRate;
        public PowerUpStats speedBoostStats;
        public float shieldSpawnRate;
        public PowerUpStats shieldStats;
    }
}
