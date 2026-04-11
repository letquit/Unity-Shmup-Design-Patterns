using UnityEngine;

namespace Shmup
{
    /// <summary>
    /// 三连发射击策略类，继承自WeaponStrategy
    /// 实现从指定位置发射三个呈扇形分布的子弹
    /// </summary>
    [CreateAssetMenu(fileName = "TripleShot", menuName = "Shmup/WeaponStrategy/TripleShot")]
    public class TripleShot : WeaponStrategy
    {
        /// <summary>
        /// 子弹扩散角度，控制三个子弹之间的夹角
        /// </summary>
        [SerializeField] private float spreadAngle = 15f;
        
        /// <summary>
        /// 执行三连发射击逻辑
        /// </summary>
        /// <param name="firePoint">射击起始点的Transform组件</param>
        /// <param name="layer">子弹所在的物理层</param>
        public override void Fire(Transform firePoint, LayerMask layer)
        {
            // 循环创建三个子弹，形成扇形射击效果
            for (int i = 0; i < 3; i++)
            {
                var projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                projectile.transform.SetParent(firePoint);
                
                // 根据索引调整子弹的旋转角度，实现扩散效果
                // i=0时向左偏转spreadAngle度，i=1时无偏转，i=2时向右偏转spreadAngle度
                projectile.transform.Rotate(0f, spreadAngle * (i - 1), 0f);
                projectile.layer = layer;

                var projectileComponent = projectile.GetComponent<Projectile>();
                projectileComponent.SetSpeed(projectileSpeed);

                Destroy(projectile, projectileLifetime);
            }
        }
    }
}
