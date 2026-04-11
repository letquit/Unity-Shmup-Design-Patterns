using System;
using UnityEngine;

namespace Shmup
{
    /// <summary>
    /// 玩家飞机类，继承自Plane基类
    /// </summary>
    public class Player : Plane
    {
        [SerializeField] private float maxFuel;
        [SerializeField] private float fuelConsumptionRate;

        private float fuel;
        
        private void Start() => fuel = maxFuel;
        
        /// <summary>
        /// 获取当前燃料的归一化值（0-1之间）
        /// </summary>
        /// <returns>当前燃料与最大燃料的比值</returns>
        public float GetFuelNormalized() => fuel / maxFuel;

        private void Update()
        {
            // 每帧根据消耗率减少燃料
            fuel -= fuelConsumptionRate * Time.deltaTime;
        }

        /// <summary>
        /// 为玩家添加燃料
        /// </summary>
        /// <param name="amount">要添加的燃料量</param>
        public void AddFuel(float amount)
        {
            fuel += amount;
            if (fuel > maxFuel)
            {
                fuel = maxFuel;
            }
        }

        /// <summary>
        /// 重写的死亡方法
        /// </summary>
        protected override void Die()
        {
        }
    }
}
