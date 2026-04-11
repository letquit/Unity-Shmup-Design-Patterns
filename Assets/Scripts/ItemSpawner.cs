using System;
using System.Collections.Generic;
using MEC;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Shmup
{
    /// <summary>
    /// 物品生成器组件，负责在游戏场景中定时生成随机物品
    /// </summary>
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private Item[] itemPrefabs;
        [SerializeField] private float spawnInterval = 3f;
        [SerializeField] private float spawnRadius = 3f;

        private CoroutineHandle spawnCoroutine;
        
        private void Start() => spawnCoroutine = Timing.RunCoroutine(SpawnItems());

        private void OnDestroy()
        {
            Timing.KillCoroutines(spawnCoroutine);
        }

        /// <summary>
        /// 持续生成物品的协程方法
        /// </summary>
        /// <returns>返回float类型的IEnumerator用于MEC协程系统</returns>
        private IEnumerator<float> SpawnItems()
        {
            // 持续循环生成物品
            while (true)
            {
                yield return Timing.WaitForSeconds(spawnInterval);
                var item = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)]);
                // 在以当前物体位置为中心、spawnRadius为半径的圆形区域内随机生成物品，并确保Z轴为0
                item.transform.position = (transform.position + Random.insideUnitSphere).With(z: 0) * spawnRadius;
            }
        }
    }
}
