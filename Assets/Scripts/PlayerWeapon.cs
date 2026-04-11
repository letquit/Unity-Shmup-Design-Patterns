using UnityEngine;

namespace Shmup
{
    /// <summary>
    /// 玩家武器组件，负责处理玩家武器的射击逻辑
    /// </summary>
    public class PlayerWeapon : Weapon
    {
        private InputReader input;
        private float fireTimer;

        /// <summary>
        /// 初始化输入读取器组件
        /// </summary>
        private void Awake() => input = GetComponent<InputReader>();

        /// <summary>
        /// 更新武器状态并处理射击逻辑
        /// </summary>
        private void Update()
        {
            // 累加射击计时器
            fireTimer += Time.deltaTime;

            // 检查输入和射击间隔条件
            if (input.Fire && fireTimer >= weaponStrategy.FireRate)
            {
                // 执行射击操作
                weaponStrategy.Fire(firePoint, layer);
                // 重置射击计时器
                fireTimer = 0f;
            }
        }
    }
}
