using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    /// <summary>
    /// Boss 控制器。
    /// 管理 Boss 的生命值、阶段切换以及战斗流程。
    /// 采用状态模式思想，将复杂的 Boss 战拆解为多个独立的 BossStage。
    /// </summary>
    public class Boss : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f; // 最大生命值
        [SerializeField] private GameObject explosionPrefab; // 死亡时的爆炸特效预制体
        private float health; // 当前生命值

        private Collider bossCollider; // Boss 自身的碰撞体

        public List<BossStage> Stages; // Boss 战斗阶段列表（在 Inspector 中配置）
        private int currentStage = 0; // 当前阶段索引

        // 当生命值发生变化时触发的事件（用于更新 UI）
        public event Action OnHealthChanged;

        private void Awake() => bossCollider = GetComponent<Collider>();

        private void Start()
        {
            health = maxHealth;
            bossCollider.enabled = true;

            // 检查是否配置了阶段，如果没有则报错
            if (Stages == null || Stages.Count == 0)
            {
                Debug.LogError("Boss has no stages configured.");
                return;
            }

            // 初始化第一个阶段
            InitializeStage();
        }

        /// <summary>
        /// 获取归一化的生命值（0 到 1 之间）。
        /// 用于驱动 UI 血条进度。
        /// </summary>
        public float GetHealthNormalized() => health / maxHealth;

        /// <summary>
        /// 检查当前阶段是否完成。
        /// 由敌机系统的销毁事件触发。
        /// </summary>
        private void CheckStageComplete()
        {
            if (currentStage < 0 || currentStage >= Stages.Count) return;

            if (Stages[currentStage].IsStageComplete())
            {
                AdvanceToNextStage();
            }
        }

        /// <summary>
        /// 推进到下一个阶段。
        /// 负责清理旧阶段的监听，并初始化新阶段。
        /// </summary>
        private void AdvanceToNextStage()
        {
            if (currentStage < 0 || currentStage >= Stages.Count) return;

            // 移除当前阶段所有敌机系统的监听器（防止内存泄漏或重复触发）
            foreach (var system in Stages[currentStage].enemySystems)
            {
                if (system != null)
                    system.OnSystemDestroyed.RemoveListener(CheckStageComplete);
            }

            currentStage++;

            // 如果还有下一个阶段，则初始化；否则 Boss 进入最终状态（可能变为可攻击）
            if (currentStage < Stages.Count)
            {
                InitializeStage();
            }
            else
            {
                bossCollider.enabled = true;
            }
        }

        /// <summary>
        /// 初始化当前阶段。
        /// 设置阶段状态并监听敌机系统的销毁事件。
        /// </summary>
        private void InitializeStage()
        {
            if (currentStage < 0 || currentStage >= Stages.Count) return;

            var stage = Stages[currentStage];
            stage.InitializeStage();

            // 监听当前阶段所有敌机系统的销毁事件
            foreach (var system in stage.enemySystems)
            {
                if (system != null)
                    system.OnSystemDestroyed.AddListener(CheckStageComplete);
            }

            // 根据阶段配置设置 Boss 是否无敌（控制碰撞体开关）
            bossCollider.enabled = !stage.IsBossInvulnerable;
        }

        /// <summary>
        /// 碰撞检测。
        /// 当玩家或其他物体撞击 Boss 时扣血。
        /// </summary>
        private void OnCollisionEnter(Collision other)
        {
            health -= 10; // 固定扣血量
            OnHealthChanged?.Invoke();

            if (health <= 0)
            {
                BossDefeated();
            }
        }

        /// <summary>
        /// Boss 被击败时的逻辑。
        /// </summary>
        private void BossDefeated()
        {
            Debug.Log("Boss defeated!");
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }
}