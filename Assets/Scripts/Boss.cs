using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class Boss : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private GameObject explosionPrefab;
        private float health;

        private Collider bossCollider;

        public List<BossStage> Stages;
        private int currentStage = 0;

        public event Action OnHealthChanged;

        private void Awake() => bossCollider = GetComponent<Collider>();

        private void Start()
        {
            health = maxHealth;
            bossCollider.enabled = true;

            if (Stages == null || Stages.Count == 0)
            {
                Debug.LogError("Boss has no stages configured.");
                return;
            }

            InitializeStage();
        }

        public float GetHealthNormalized() => health / maxHealth;

        private void CheckStageComplete()
        {
            if (currentStage < 0 || currentStage >= Stages.Count) return;

            if (Stages[currentStage].IsStageComplete())
            {
                AdvanceToNextStage();
            }
        }

        private void AdvanceToNextStage()
        {
            if (currentStage < 0 || currentStage >= Stages.Count) return;

            foreach (var system in Stages[currentStage].enemySystems)
            {
                if (system != null)
                    system.OnSystemDestroyed.RemoveListener(CheckStageComplete);
            }

            currentStage++;

            if (currentStage < Stages.Count)
            {
                InitializeStage();
            }
            else
            {
                bossCollider.enabled = true;
            }
        }

        private void InitializeStage()
        {
            if (currentStage < 0 || currentStage >= Stages.Count) return;

            var stage = Stages[currentStage];
            stage.InitializeStage();

            foreach (var system in stage.enemySystems)
            {
                if (system != null)
                    system.OnSystemDestroyed.AddListener(CheckStageComplete);
            }

            bossCollider.enabled = !stage.IsBossInvulnerable;
        }

        private void OnCollisionEnter(Collision other)
        {
            health -= 10;
            OnHealthChanged?.Invoke();

            if (health <= 0)
            {
                BossDefeated();
            }
        }

        private void BossDefeated()
        {
            Debug.Log("Boss defeated!");
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }
}