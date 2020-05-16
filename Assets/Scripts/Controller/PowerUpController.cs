using Domain;
using UnityEngine;

namespace Controller
{
    public class PowerUpController : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] [ExpandableAttribute] private PowerUpStats stats;
#pragma warning restore 0649

        private void Update()
        {
            Move();
            CheckBounds();
        }

        private void CheckCollisionWithPlayer(Component other)
        {
            if (!other.transform.CompareTag("Player")) return;

            var playerController = other.GetComponent<PlayerController>();
            if (playerController is null) return;

            switch (stats.powerUpType)
            {
                case PowerUpType.TripleShot:
                    playerController.EnableTripleShot(stats.duration);
                    break;
                case PowerUpType.SpeedBoost:
                    playerController.EnableSpeedBoost(stats.duration);
                    break;
                case PowerUpType.Shield:
                    playerController.EnableShieldBoost(stats.duration);
                    break;
                default:
                    Debug.LogError("Unknown power up!");
                    break;
            }

            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            CheckCollisionWithPlayer(other);
        }

        private void CheckBounds()
        {
            var moveBounds = stats.moveBounds;

            if (transform.position.y >= moveBounds.yMax) return;
            Destroy(gameObject);
        }

        private void Move()
        {
            transform.Translate(stats.speed * Time.deltaTime * Vector3.down);
        }
    }
}