using UnityEngine;
using Utilities;

namespace Shmup
{
    /// <summary>
    /// 跟踪射击武器策略类，创建能够跟踪玩家目标的弹丸
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerTrackingShot", menuName = "Shmup/WeaponStrategy/PlayerTrackingShot")]
    public class TrackingShot : WeaponStrategy
    {
        [SerializeField] private float trackingSpeed = 1f;

        private Transform target;

        /// <summary>
        /// 初始化方法，查找并设置玩家作为跟踪目标
        /// </summary>
        public override void Initialize()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        /// <summary>
        /// 发射弹丸的方法
        /// </summary>
        /// <param name="firePoint">发射点的Transform组件</param>
        /// <param name="layer">弹丸所在的图层</param>
        public override void Fire(Transform firePoint, LayerMask layer)
        {
            var projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.transform.SetParent(firePoint);
            projectile.layer = layer;
    
            var projectileComponent = projectile.GetComponent<Projectile>();
            projectileComponent.SetSpeed(projectileSpeed);
    
            float fixedZ = firePoint.position.z; 
    
            GameObject projGO = projectile;
            // 设置弹丸的回调函数，实现跟踪逻辑
            projectileComponent.Callback = () =>
            {
                if (target == null || projGO == null) return;
        
                Vector3 direction = (target.position - projGO.transform.position).With(z: fixedZ).normalized;
                Quaternion rotation = Quaternion.LookRotation(direction, Vector3.forward);
                projGO.transform.rotation = Quaternion.Slerp(projGO.transform.rotation, rotation,
                    trackingSpeed * Time.deltaTime);
            };
    
            Destroy(projectile, projectileLifetime);
        }
    }
}
