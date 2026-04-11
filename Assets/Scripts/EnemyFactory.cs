using UnityEngine;
using UnityEngine.Splines;

namespace Shmup
{
    /// <summary>
    /// 敌人工厂类，用于创建不同类型的敌人对象
    /// </summary>
    public class EnemyFactory
    {
        /// <summary>
        /// 创建敌人对象
        /// </summary>
        /// <param name="enemyType">敌人类型，包含预制体和速度等信息</param>
        /// <param name="spline">敌人移动路径的样条线容器</param>
        /// <returns>创建的敌人游戏对象</returns>
        public GameObject CreateEnemy(EnemyType enemyType, SplineContainer spline)
        {
            // 使用建造者模式配置敌人属性
            EnemyBuilder builder = new EnemyBuilder()
                .SetBasePrefab(enemyType.enemyPrefab)
                .SetSpline(spline)
                .SetSpeed(enemyType.speed);
            
            return builder.Build();
        }
    }
}
