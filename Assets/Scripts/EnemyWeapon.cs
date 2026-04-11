using System;
using UnityEngine;

namespace Shmup
{
    /// <summary>
    /// 敌方武器类，继承自Weapon基类
    /// 负责处理敌方单位的武器射击逻辑
    /// </summary>
    public class EnemyWeapon : Weapon
    {
        /// <summary>
        /// 射击计时器，用于跟踪自上次射击以来经过的时间
        /// </summary>
        private float fireTimer;

        /// <summary>
        /// Unity更新方法，每帧调用一次
        /// 处理武器射击的定时逻辑
        /// </summary>
        private void Update()
        {
            // 增加射击计时器，累计经过的时间
            fireTimer += Time.deltaTime;

            // 检查是否达到射击间隔，如果达到则执行射击操作
            if (fireTimer >= weaponStrategy.FireRate)
            {
                weaponStrategy.Fire(firePoint, layer);
                fireTimer = 0f;
            }
        }
    }
}
