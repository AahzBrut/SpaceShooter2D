using Domain;
using Manager;
using UnityEngine;

namespace Controller
{
    public class PlayerController : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] [ExpandableAttribute] private CraftStats craftStats;
#pragma warning restore 0649

        private float lastShotTime;
        private Transform projectileContainer;
        private SpawnManager spawnManager;
        private int numLives;
        private int score;
        private float tripleShotEndTime = -1f;
        private float speedBoostEndTime = -1f;
        private float shieldBoostEndTime = -1f;
        private GameObject rightEngine;
        private GameObject leftEngine;
        private UiManager uiManager;
        private AudioSource audioSource;

        private void Start()
        {
            transform.position = craftStats.startingPosition;
            numLives = craftStats.lives;

            audioSource = GetComponent<AudioSource>();
            if (audioSource is null) Debug.LogError("AudioSource not found!");
            audioSource.clip = craftStats.laserFireSound;
            
            rightEngine = transform.GetChild(2).gameObject;
            leftEngine = transform.GetChild(3).gameObject;
            if (rightEngine is null || leftEngine is null) Debug.LogError("Engines not found!");

            uiManager = GameObject.Find("UIManager").GetComponent<UiManager>();
            if (uiManager is null) Debug.LogError("UiManager not found!");

            uiManager.UpdateLivesDisplay(numLives);

            projectileContainer = GameObject.FindGameObjectWithTag("ProjectileContainer").transform;
            if (projectileContainer == null) Debug.Log("ProjectileContainer is NULL!");

            spawnManager = FindObjectOfType<SpawnManager>();
            if (spawnManager == null) Debug.Log("EnemyManager is NULL!");
        }

        private void Update()
        {
            CalculateMovement();
            CalculateFiring();
            UpdateVfx();
        }

        private void UpdateVfx()
        {
            var isShieldActive = Time.time <= shieldBoostEndTime;
            transform.GetChild(0).gameObject.SetActive(isShieldActive);
        }

        private void CalculateFiring()
        {
            if (!Input.GetKey(KeyCode.Space)) return;

            var fireDelay = 1f / craftStats.firingRate;
            if (Time.time - lastShotTime < fireDelay) return;

            FireLaser(Time.time <= tripleShotEndTime ? craftStats.tripleShotBolt : craftStats.laserBolt);

            lastShotTime = Time.time;
        }

        private void FireLaser(GameObject laserPrefab)
        {
            var newLaser = Instantiate(laserPrefab, transform.position + craftStats.turretOffset, Quaternion.identity);
            newLaser.transform.SetParent(projectileContainer);
            
            audioSource.Play();
        }

        private void CalculateMovement()
        {
            var horizontalValue = Input.GetAxis("Horizontal");
            var verticalValue = Input.GetAxis("Vertical");
            var direction = Vector3.up * verticalValue + Vector3.right * horizontalValue;
            var boost = Time.time <= speedBoostEndTime ? 2 : 1;

            transform.Translate(boost * craftStats.speed * Time.deltaTime * direction);

            RestrainMovement();
        }

        private void RestrainMovement()
        {
            var position = transform.position;
            var craftBounds = craftStats.bounds;

            transform.position = new Vector3(
                position.x,
                Mathf.Clamp(position.y, craftBounds.yMax, craftBounds.yMin),
                position.z);

            if (position.x < craftBounds.xMin)
                transform.position = new Vector3(craftBounds.xMax, position.y, position.z);
            if (position.x > craftBounds.xMax)
                transform.position = new Vector3(craftBounds.xMin, position.y, position.z);
        }

        public void TakeDamage()
        {
            if (Time.time <= shieldBoostEndTime)
            {
                shieldBoostEndTime = -1f;
                return;
            }

            numLives--;

            AudioSource.PlayClipAtPoint(craftStats.deathExplosionSound, transform.position);
            DestroyEngines();

            uiManager.UpdateLivesDisplay(numLives);
            if (numLives > 0) return;

            spawnManager.StopSpawning();
            Destroy(gameObject);
        }

        private void DestroyEngines()
        {
            switch (numLives)
            {
                case 2:
                    rightEngine.gameObject.SetActive(true);
                    break;
                case 1:
                    leftEngine.gameObject.SetActive(true);
                    break;
                case 0:
                    rightEngine.gameObject.SetActive(false);
                    leftEngine.gameObject.SetActive(false);
                    break;
            }
        }

        public void EnableTripleShot(float duration)
        {
            tripleShotEndTime = Time.time + duration;
            AudioSource.PlayClipAtPoint(craftStats.powerUpCollectSound, transform.position, 2);
        }

        public void EnableSpeedBoost(float duration)
        {
            speedBoostEndTime = Time.time + duration;
            AudioSource.PlayClipAtPoint(craftStats.powerUpCollectSound, transform.position, 2);
        }

        public void EnableShieldBoost(float duration)
        {
            shieldBoostEndTime = Time.time + duration;
            AudioSource.PlayClipAtPoint(craftStats.powerUpCollectSound, transform.position, 2);
        }

        public void AddScore(int scoreAmount)
        {
            score += scoreAmount;
            uiManager.UpdateScore(score);
        }
    }
}