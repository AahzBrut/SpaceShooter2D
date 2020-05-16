using UnityEngine;

namespace Domain
{
    [CreateAssetMenu(menuName = "Data/LaserStats")]
    public class LaserStats : ScriptableObject
    {
        public float speed;
        public float damage;
        public float upperBound;
    }
}
