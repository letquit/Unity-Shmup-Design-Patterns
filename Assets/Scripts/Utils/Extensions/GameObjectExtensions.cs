using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// GameObject 扩展方法类。
    /// 包含用于简化 GameObject 操作的静态扩展方法。
    /// </summary>
    public static class GameObjectExtensions
    {
        /// <summary>
        /// 获取或添加组件。
        /// 尝试获取指定类型的组件，如果不存在则自动添加该组件。
        /// 这是一种常见的“防御性编程”模式，确保组件一定存在。
        /// </summary>
        /// <typeparam name="T">组件类型（必须是 Component 的子类）</typeparam>
        /// <param name="gameObject">目标 GameObject 实例</param>
        /// <returns>已存在的组件或新添加的组件</returns>
        public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
        {
            // 尝试获取组件
            T component = gameObject.GetComponent<T>();
            // 如果组件存在则返回，否则添加新组件并返回
            return component != null ? component : gameObject.AddComponent<T>();
        }
    }
}