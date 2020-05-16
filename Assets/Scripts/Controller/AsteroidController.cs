using Domain;
using Manager;
using UnityEngine;

namespace Controller
{
    public class AsteroidController : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] 
        [ExpandableAttribute]
        private AsteroidStats stats;
#pragma warning restore 0649
        
        private PlayerController playerController;
        private float rotationSpeed;
        private SpawnManager spawnManager;

        private void Start()
        {
            rotationSpeed = stats.rotationSpeed;

            playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            if (playerController is null)
                Debug.LogError("Can't find PlayerController");
            
            spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
            if (spawnManager is null)
                Debug.LogError("Can't find SpawnManager");
        }

        private void Update()
        {
            Rotate();
        }

        private void Rotate()
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            CheckCollisionWithProjectile(other);
        }

        private void CheckCollisionWithProjectile(Component other)
        {
            if (!other.transform.CompareTag("Projectile")) return;
            if (playerController is null) return;

            Destroy(other.gameObject);

            Explode();
        }
        
        private void Explode()
        {
            var currentPosition = transform.position;
            var explosion = Instantiate(stats.explosionPrefab, currentPosition, Quaternion.identity);
            AudioSource.PlayClipAtPoint(stats.explosionSound, currentPosition);
            spawnManager.StartSpawning();
            Destroy(gameObject, .25f);
            Destroy(explosion, 3f);
        }
    }
}
