using UnityEngine;

namespace Shmup
{
    /// <summary>
    /// 飞行器基类。
    /// 定义了所有飞行器（玩家、敌人、Boss）共有的生命值逻辑。
    /// 采用模板方法模式，将“死亡”的具体表现留给子类实现。
    /// </summary>
    public abstract class Plane : MonoBehaviour
    {
        [SerializeField] private int maxHealth; // 最大生命值
        private int health; // 当前生命值

        /// <summary>
        /// 初始化生命值。
        /// 使用 virtual 允许子类重写 Awake 逻辑，但通常建议保持基础初始化。
        /// </summary>
        protected virtual void Awake() => health = maxHealth;

        /// <summary>
        /// 设置最大生命值。
        /// 用于在运行时动态调整飞机的耐久度（例如 Boss 的不同阶段）。
        /// </summary>
        /// <param name="amount">新的最大生命值</param>
        public void SetMaxHealth(int amount) => maxHealth = amount;

        /// <summary>
        /// 受到伤害。
        /// 扣除生命值，并在生命值耗尽时触发死亡逻辑。
        /// </summary>
        /// <param name="amount">伤害数值</param>
        public void TakeDamage(int amount)
        {
            health -= amount;
            if (health <= 0)
            {
                Die();
            }
        }

        /// <summary>
        /// 恢复生命值。
        /// 增加生命值，但不会超过最大生命值上限。
        /// </summary>
        /// <param name="amount">恢复数值</param>
        public void AddHealth(int amount)
        {
            health += amount;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }

        /// <summary>
        /// 获取归一化的生命值（0.0 到 1.0）。
        /// 用于 UI 血条显示。
        /// </summary>
        /// <returns>当前生命值百分比</returns>
        public float GetHealthNormalized() => health / (float)maxHealth;

        /// <summary>
        /// 死亡逻辑的抽象定义。
        /// 子类（如 Player, Enemy）必须实现具体的死亡行为（如播放特效、掉落物品等）。
        /// </summary>
        protected abstract void Die();
    }
}