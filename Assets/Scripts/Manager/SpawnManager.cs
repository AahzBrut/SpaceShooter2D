using System.Collections;
using Domain;
using UnityEngine;

namespace Manager
{
    public class SpawnManager : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] [ExpandableAttribute] private SpawnStats spawnStats;

        [SerializeField] private GameObject enemyContainer;

        [SerializeField] private GameObject powerUpContainer;
#pragma warning restore 0649

        private bool stopSpawning;

        public void StartSpawning()
        {
            StartCoroutine(SpawnEnemy());
            StartCoroutine(SpawnTripleShotPowerUp());
            StartCoroutine(SpawnSpeedBoostPowerUp());
            StartCoroutine(SpawnShieldPowerUp());
        }

        private IEnumerator SpawnShieldPowerUp()
        {
            yield return new WaitForSeconds(spawnStats.startDelay);
            while (!stopSpawning)
            {
                var stats = spawnStats.shieldStats;
                var bounds = stats.moveBounds;
                var spawnPosition = GetSpawnPosition(bounds);

                var newEnemy = Instantiate(stats.powerUpPrefab, spawnPosition, Quaternion.identity);
                newEnemy.transform.SetParent(powerUpContainer.transform);
                yield return new WaitForSeconds(1f / spawnStats.shieldSpawnRate);
            }
        }

        private IEnumerator SpawnEnemy()
        {
            yield return new WaitForSeconds(spawnStats.startDelay);
            while (!stopSpawning)
            {
                var stats = spawnStats.enemyStats;
                var bounds = stats.bounds;
                var spawnPosition = GetSpawnPosition(bounds);

                var newEnemy = Instantiate(spawnStats.enemyStats.craftPrefab, spawnPosition, Quaternion.identity);
                newEnemy.transform.SetParent(enemyContainer.transform);
                yield return new WaitForSeconds(1f / spawnStats.enemySpawnRate);
            }
        }

        private IEnumerator SpawnTripleShotPowerUp()
        {
            yield return new WaitForSeconds(spawnStats.startDelay);
            while (!stopSpawning)
            {
                var stats = spawnStats.tripleShotStats;
                var moveBounds = stats.moveBounds;
                var spawnPosition = GetSpawnPosition(moveBounds);

                var newPowerUp = Instantiate(spawnStats.tripleShotStats.powerUpPrefab, spawnPosition,
                    Quaternion.identity);
                newPowerUp.transform.SetParent(powerUpContainer.transform);

                yield return new WaitForSeconds(1f / spawnStats.tripleShotSpawnRate);
            }
        }

        private static Vector3 GetSpawnPosition(Rect moveBounds)
        {
            return new Vector3(Random.Range(moveBounds.xMin, moveBounds.xMax), moveBounds.yMin, 0);
        }

        private IEnumerator SpawnSpeedBoostPowerUp()
        {
            yield return new WaitForSeconds(spawnStats.startDelay);
            while (!stopSpawning)
            {
                var stats = spawnStats.speedBoostStats;
                var moveBounds = stats.moveBounds;
                var spawnPosition = GetSpawnPosition(moveBounds);

                var newPowerUp = Instantiate(spawnStats.speedBoostStats.powerUpPrefab, spawnPosition,
                    Quaternion.identity);
                newPowerUp.transform.SetParent(powerUpContainer.transform);

                yield return new WaitForSeconds(1f / spawnStats.speedBoostSpawnRate);
            }
        }

        public void StopSpawning()
        {
            stopSpawning = true;
        }
    }
}