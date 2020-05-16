using Domain;
using UnityEngine;

namespace Controller
{
    public class EnemyController : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] 
        [ExpandableAttribute]
        private CraftStats craftStats;
#pragma warning restore 0649

        private PlayerController playerController;
        private Animator animator;
        private static readonly int OnDeath = Animator.StringToHash("OnDeath");
        private float speed;
        private Collider2D currentCollider;
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource is null)  Debug.LogError("Can't find AudioSource");
            
            currentCollider = gameObject.GetComponent<Collider2D>();
            if (currentCollider == null) Debug.LogError("Can't find Collider");
            
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            if (playerController is null) Debug.LogError("Can't find PlayerController");

            animator = gameObject.GetComponent<Animator>();
            if (animator is null) Debug.LogError("Can't find Animator");

            speed = craftStats.speed;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.Translate(speed * Time.deltaTime * Vector3.down);
            CheckBounds();
        }

        private void CheckBounds()
        {
            var position = transform.position;
            var craftBounds = craftStats.bounds;
            
            if (position.y < craftBounds.yMax)
                transform.position = new Vector3(
                    Random.Range(craftBounds.xMin, craftBounds.xMax), 
                    craftBounds.yMin, 
                    0);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            CheckCollisionWithPlayer(other);
            CheckCollisionWithProjectile(other);
        }

        private void CheckCollisionWithProjectile(Component other)
        {
            if (!other.transform.CompareTag("Projectile")) return;
            if (playerController is null) return;

            playerController.AddScore(10);
            Destroy(other.gameObject);

            Explode();
        }

        private void Explode()
        {
            currentCollider.enabled = false;
            animator.SetTrigger(OnDeath);
            speed = 0;
            AudioSource.PlayClipAtPoint(craftStats.deathExplosionSound, transform.position);
            Destroy(gameObject, 2.8f);
        }

        private void CheckCollisionWithPlayer(Component other)
        {
            if (!other.transform.CompareTag("Player")) return;
            if (playerController is null) return;
            
            playerController.TakeDamage();

            Explode();
        }
    }
}
