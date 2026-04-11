using System;
using UnityEngine;
using Utilities;

namespace Shmup
{
    /// <summary>
    /// 武器基类，继承自MonoBehaviour，用于定义武器的基本行为和属性
    /// </summary>
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected WeaponStrategy weaponStrategy;
        [SerializeField] protected Transform firePoint;
        [SerializeField, Layer] protected int layer;

        /// <summary>
        /// 验证函数，在编辑器中修改组件时自动调用，同步游戏对象的层设置
        /// </summary>
        private void OnValidate() => layer = gameObject.layer;

        /// <summary>
        /// 启动函数，在对象初始化时调用武器策略的初始化方法
        /// </summary>
        private void Start() => weaponStrategy.Initialize();
        
        /// <summary>
        /// 设置武器策略的方法
        /// </summary>
        /// <param name="strategy">要设置的武器策略对象</param>
        public void SetWeaponStrategy(WeaponStrategy strategy)
        {
            weaponStrategy = strategy;
            // 初始化新设置的武器策略
            weaponStrategy.Initialize();
        }
    }
}
