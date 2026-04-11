using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// Vector3 扩展方法类。
    /// 提供便捷的向量操作方法，简化常见的向量修改和运算逻辑。
    /// </summary>
    public static class Vector3Extensions
    {
        /// <summary>
        /// 创建一个新的向量，并有选择性地覆盖指定的分量。
        /// 类似于 C# 9.0 的 with 表达式，用于不可变地修改向量。
        /// </summary>
        /// <param name="vector">原始向量</param>
        /// <param name="x">新的 X 值（如果为 null 则保持原值）</param>
        /// <param name="y">新的 Y 值（如果为 null 则保持原值）</param>
        /// <param name="z">新的 Z 值（如果为 null 则保持原值）</param>
        /// <returns>修改后的新向量</returns>
        /// <example>
        /// 示例：将向量的 Y 轴设为 0，保持 X 和 Z 不变
        /// var flatPos = position.With(y: 0);
        /// </example>
        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            // 使用空合并运算符，如果传入参数则使用参数，否则使用原向量的对应分量
            return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
        }

        /// <summary>
        /// 创建一个新的向量，并在原始向量的基础上增加指定的分量。
        /// 用于沿特定轴向进行增量移动，避免编写冗长的加法代码。
        /// </summary>
        /// <param name="vector">原始向量</param>
        /// <param name="x">X 轴的增量值（如果为 null 则加 0）</param>
        /// <param name="y">Y 轴的增量值（如果为 null 则加 0）</param>
        /// <param name="z">Z 轴的增量值（如果为 null 则加 0）</param>
        /// <returns>增加后的新向量</returns>
        /// <example>
        /// 示例：沿 X 轴向右移动 5 个单位
        /// var movedPos = position.Add(x: 5);
        /// </example>
        public static Vector3 Add(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            // 将原分量与增量相加，如果增量为 null 则默认为 0
            return new Vector3(vector.x + (x ?? 0), vector.y + (y ?? 0), vector.z + (z ?? 0));
        }
    }
}