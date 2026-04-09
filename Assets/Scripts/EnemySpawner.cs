using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using Random = UnityEngine.Random;

namespace Shmup
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<EnemyType> enemyTypes;
        [SerializeField] private int maxEnemies = 10;
        [SerializeField] private float spawnInterval = 2f;

        private List<SplineContainer> splines;
        private EnemyFactory enemyFactory;

        private float spawnTimer;
        private int enemiesSpawned;

        private void OnValidate()
        {
            splines = new List<SplineContainer>(GetComponentsInChildren<SplineContainer>());
        }
        
        private void Start() => enemyFactory = new EnemyFactory();

        private void Update()
        {
            spawnTimer += Time.deltaTime;

            if (enemiesSpawned < maxEnemies && spawnTimer >= spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            }
        }

        private void SpawnEnemy()
        {
            EnemyType enemyType = enemyTypes[Random.Range(0, enemyTypes.Count)];
            SplineContainer spline = splines[Random.Range(0, splines.Count)];

            enemyFactory.CreateEnemy(enemyType, spline);
            enemiesSpawned++;
        }
    }
}