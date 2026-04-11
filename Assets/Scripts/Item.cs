using System;
using UnityEngine;

namespace Shmup
{
    /// <summary>
    /// 物品类的抽象基类，继承自MonoBehaviour
    /// 用于定义游戏中各种可收集或使用的物品的基础属性和行为
    /// </summary>
    public abstract class Item : MonoBehaviour
    {
        /// <summary>
        /// 物品的数量值，默认为10f
        /// 该字段在Unity编辑器中可序列化，允许在Inspector中进行调整
        /// </summary>
        [SerializeField] protected float amount = 10f;
    }
}
