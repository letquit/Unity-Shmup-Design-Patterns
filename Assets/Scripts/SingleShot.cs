using UnityEngine;

namespace Shmup
{
    /// <summary>
    /// 单发武器策略类，实现单次发射一个子弹的武器行为
    /// </summary>
    [CreateAssetMenu(fileName = "SingleShot", menuName = "Shmup/WeaponStrategy/SingleShot")]
    public class SingleShot : WeaponStrategy
    {
        /// <summary>
        /// 执行射击操作
        /// </summary>
        /// <param name="firePoint">射击点的Transform组件，决定子弹的发射位置和方向</param>
        /// <param name="layer">子弹应该设置的层，用于碰撞检测</param>
        public override void Fire(Transform firePoint, LayerMask layer)
        {
            // 创建子弹实例
            var projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.transform.SetParent(firePoint);
            projectile.layer = layer;
            
            // 设置子弹速度
            var projectileComponent = projectile.GetComponent<Projectile>();
            projectileComponent.SetSpeed(projectileSpeed);
            
            // 设置子弹生命周期后自动销毁
            Destroy(projectile, projectileLifetime);
        }
    }
}
