using System;
using System.Collections.Generic;
using MEC;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Shmup
{
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

        private IEnumerator<float> SpawnItems()
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(spawnInterval);
                var item = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)]);
                item.transform.position = (transform.position + Random.insideUnitSphere).With(z: 0) * spawnRadius;
            }
        }
    }
}