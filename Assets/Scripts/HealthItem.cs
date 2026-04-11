using UnityEngine;

namespace Shmup
{
    /// <summary>
    /// 血包道具类，继承自Item基类
    /// 用于恢复玩家生命值的可拾取物品
    /// </summary>
    public class HealthItem : Item
    {
        /// <summary>
        /// 当其他碰撞体进入触发器时调用的回调方法
        /// </summary>
        /// <param name="other">进入触发器的碰撞体对象</param>
        /// <remarks>
        /// 该方法会获取碰撞体所属的Player组件并增加相应生命值，然后销毁当前血包对象
        /// </remarks>
        private void OnTriggerEnter(Collider other)
        {
            // 获取玩家组件并增加生命值，然后销毁血包对象
            other.GetComponent<Player>().AddHealth((int) amount);
            Destroy(gameObject);
        }
    }
}
