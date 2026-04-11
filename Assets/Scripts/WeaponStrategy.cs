using UnityEngine;

namespace Shmup
{
    /// <summary>
    /// 武器策略抽象基类，继承自ScriptableObject
    /// 定义了武器的基本属性和行为规范
    /// </summary>
    public abstract class WeaponStrategy : ScriptableObject
    {
        [SerializeField] private int damage = 10;
        [SerializeField] private float fireRate = 0.5f;
        [SerializeField] protected float projectileSpeed = 10f;
        [SerializeField] protected float projectileLifetime = 4f;
        [SerializeField] protected GameObject projectilePrefab;
        
        /// <summary>
        /// 获取武器伤害值
        /// </summary>
        public int Damage => damage;
        
        /// <summary>
        /// 获取武器射速
        /// </summary>
        public float FireRate => fireRate;

        /// <summary>
        /// 初始化武器策略
        /// 子类可以重写此方法来执行特定的初始化逻辑
        /// </summary>
        public virtual void Initialize()
        {
            
        }

        /// <summary>
        /// 抽象方法：发射武器
        /// 子类必须实现此方法来定义具体的发射逻辑
        /// </summary>
        /// <param name="firePoint">发射点的Transform组件</param>
        /// <param name="layer">用于碰撞检测的层掩码</param>
        public abstract void Fire(Transform firePoint, LayerMask layer);
    }
}
