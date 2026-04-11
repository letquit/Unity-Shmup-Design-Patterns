using UnityEngine;

namespace Shmup
{
    /// <summary>
    /// 燃料道具类，继承自Item基类
    /// </summary>
    public class FuelItem : Item
    {
        /// <summary>
        /// 当碰撞触发时调用的方法
        /// </summary>
        /// <param name="other">触发碰撞的其他碰撞体</param>
        private void OnTriggerEnter(Collider other)
        {
            // 获取玩家组件并添加燃料
            other.GetComponent<Player>().AddFuel(amount);
            // 销毁当前燃料道具对象
            Destroy(gameObject);
        }
    }
}
