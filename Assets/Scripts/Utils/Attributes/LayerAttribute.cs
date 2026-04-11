using UnityEngine;

// 定义命名空间 Utilities，用于组织工具类代码
namespace Utilities
{
    // 声明一个公共类 LayerAttribute，继承自 Unity 的 PropertyAttribute
    // PropertyAttribute 是创建自定义属性的基础类，用于控制序列化属性在 Inspector 中的显示方式
    public class LayerAttribute : PropertyAttribute
    {
        // 当前类体为空，这意味着它使用默认的构造函数和行为。
        // 它的主要作用仅仅是作为一个“标记”。
        // （通常在这里会添加构造函数来接收参数，或者添加字段来存储配置信息）
    }
}