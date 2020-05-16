using UnityEngine;

namespace Domain
{
    [CreateAssetMenu(menuName = "Data/AsteroidStats")]
    public class AsteroidStats : ScriptableObject
    {
        public float speed;
        public float rotationSpeed;
        public Rect moveBounds;
        public GameObject prefab;
        public GameObject explosionPrefab;
        public AudioClip explosionSound;
    }
}
