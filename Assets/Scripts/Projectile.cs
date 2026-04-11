using System;
using UnityEngine;

namespace Shmup
{
    /// <summary>
    /// 子弹/投射物类，用于处理游戏中的子弹行为，包括移动、碰撞检测和特效播放
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private GameObject muzzlePrefab;
        [SerializeField] private GameObject hitPrefab;

        private Transform parent;
        
        /// <summary>
        /// 设置子弹的速度
        /// </summary>
        /// <param name="speed">子弹移动速度</param>
        public void SetSpeed(float speed) => this.speed = speed;
        
        /// <summary>
        /// 设置子弹的父级变换对象
        /// </summary>
        /// <param name="parent">父级变换对象</param>
        public void SetParent(Transform parent) => this.parent = parent;

        /// <summary>
        /// 子弹生命周期结束时的回调委托
        /// </summary>
        public Action Callback;

        private void Start()
        {
            // 在子弹发射位置创建枪口特效
            if (muzzlePrefab != null)
            {
                var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
                muzzleVFX.transform.forward = gameObject.transform.forward;
                muzzleVFX.transform.SetParent(parent);

                DestroyParticleSystem(muzzleVFX);
            }
        }

        private void Update()
        {
            // 移除子弹的父级关系以避免继承父级变换
            transform.SetParent(null);
            // 根据子弹当前朝向和速度更新位置
            transform.position += transform.forward * (speed * Time.deltaTime);
            
            Callback?.Invoke();
        }

        private void OnCollisionEnter(Collision collision)
        {
            // 创建碰撞击中特效
            if (hitPrefab != null)
            {
                ContactPoint contact = collision.contacts[0];
                var hitVFX = Instantiate(hitPrefab, contact.point, Quaternion.identity);
                
                DestroyParticleSystem(hitVFX);
            }
            
            // 检测碰撞对象是否为飞机并造成伤害
            var plane = collision.gameObject.GetComponent<Plane>();
            if (plane != null)
            {
                plane.TakeDamage(10);
            }
            
            // 销毁子弹对象
            Destroy(gameObject);
        }

        /// <summary>
        /// 销毁粒子系统特效对象
        /// </summary>
        /// <param name="vfx">包含粒子系统的特效对象</param>
        private void DestroyParticleSystem(GameObject vfx)
        {
            var ps = vfx.GetComponent<ParticleSystem>();
            if (ps == null)
            {
                ps = vfx.GetComponentInChildren<ParticleSystem>();
            }
            Destroy(vfx, ps.main.duration);
        }
    }
}
