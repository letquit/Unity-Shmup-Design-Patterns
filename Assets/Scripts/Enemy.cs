using System;
using UnityEngine;
using UnityEngine.Events;

namespace Shmup
{
    /// <summary>
    /// 敌机类，继承自Plane基类
    /// </summary>
    public class Enemy : Plane
    {
        [SerializeField] private GameObject explosionPrefab;
        
        /// <summary>
        /// 敌机死亡时的处理方法
        /// </summary>
        protected override void Die()
        {
            // 增加游戏分数
            GameManager.Instance.AddScore(10);
            // 在当前位置创建爆炸特效
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            // 触发系统销毁事件
            OnSystemDestroyed?.Invoke();
            // 销毁敌机对象
            Destroy(gameObject);
        }

        /// <summary>
        /// 当系统被销毁时触发的Unity事件
        /// </summary>
        public UnityEvent OnSystemDestroyed;
    }
}
