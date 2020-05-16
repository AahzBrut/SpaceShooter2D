using Domain;
using UnityEngine;

namespace Controller
{
    public class LaserController : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField]
        [ExpandableAttribute]
        private LaserStats laserStats;
#pragma warning restore 0649

        private void Update()
        {
            CheckExplosion();
            CalculateMove();
        }

        private void CheckExplosion()
        {
            if (transform.position.y > laserStats.upperBound) SelfDestruct();
        }

        private void SelfDestruct()
        {
            var parent = transform.parent;
            var isTripleShot = parent.name.Contains("TripleShot");

            Destroy(isTripleShot ? parent.gameObject : gameObject);
        }

        private void CalculateMove()
        {
            transform.Translate(laserStats.speed * Time.deltaTime * Vector3.up);
        }
    }
}
