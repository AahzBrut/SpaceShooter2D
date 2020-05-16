using UnityEngine;

namespace Domain
{
    [CreateAssetMenu(menuName = "Data/CraftStats")]
    public class CraftStats : ScriptableObject
    {
        public float speed;
        public Vector3 startingPosition;
        public Rect bounds;
        public GameObject laserBolt;
        public GameObject tripleShotBolt;
        public Vector3 turretOffset;
        public float firingRate;
        public int lives;
        public GameObject craftPrefab;
        public AudioClip laserFireSound;
        public AudioClip deathExplosionSound;
        public AudioClip powerUpCollectSound;
    }
}
