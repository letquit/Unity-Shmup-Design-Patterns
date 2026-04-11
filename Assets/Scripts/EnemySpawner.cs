using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using Random = UnityEngine.Random;

namespace Shmup
{
    /// <summary>
    /// 敌人生成器组件，负责在游戏场景中按照指定间隔和数量限制生成敌人
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<EnemyType> enemyTypes;
        [SerializeField] private int maxEnemies = 10;
        [SerializeField] private float spawnInterval = 2f;

        private List<SplineContainer> splines;
        private EnemyFactory enemyFactory;

        private float spawnTimer;
        private int enemiesSpawned;

        /// <summary>
        /// 验证组件时初始化样条容器列表
        /// </summary>
        private void OnValidate()
        {
            // 获取所有子对象中的SplineContainer组件并存储到列表中
            splines = new List<SplineContainer>(GetComponentsInChildren<SplineContainer>());
        }
        
        /// <summary>
        /// 初始化敌人工厂实例
        /// </summary>
        private void Start() => enemyFactory = new EnemyFactory();

        /// <summary>
        /// 更新方法，控制敌人生成的时间间隔和数量限制
        /// </summary>
        private void Update()
        {
            spawnTimer += Time.deltaTime;

            // 检查是否达到最大敌人数量限制以及是否到达生成时间间隔
            if (enemiesSpawned < maxEnemies && spawnTimer >= spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            }
        }

        /// <summary>
        /// 生成一个随机类型的敌人，并将其放置在随机选择的样条路径上
        /// </summary>
        private void SpawnEnemy()
        {
            // 随机选择敌人类型
            EnemyType enemyType = enemyTypes[Random.Range(0, enemyTypes.Count)];
            // 随机选择样条路径
            SplineContainer spline = splines[Random.Range(0, splines.Count)];

            // 使用工厂创建敌人实例
            enemyFactory.CreateEnemy(enemyType, spline);
            enemiesSpawned++;
        }
    }
}
