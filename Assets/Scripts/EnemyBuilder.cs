using UnityEngine;
using UnityEngine.Splines;
using Utilities;

namespace Shmup
{
    /// <summary>
    /// 敌人构建器。
    /// 采用建造者模式（Builder Pattern），通过链式调用流畅地配置并生成敌人。
    /// 封装了复杂的组件初始化逻辑（如路径动画设置）。
    /// </summary>
    public class EnemyBuilder
    {
        private GameObject enemyPrefab; // 敌人基础预制体
        private SplineContainer spline; // 飞行路径（样条曲线）
        private GameObject weaponPrefab; // 武器预制体（预留）
        private float speed; // 移动速度

        /// <summary>
        /// 设置敌人的基础预制体。
        /// </summary>
        /// <param name="prefab">敌人模型预制体</param>
        /// <returns>返回当前构建器实例，支持链式调用</returns>
        public EnemyBuilder SetBasePrefab(GameObject prefab)
        {
            enemyPrefab = prefab;
            return this;
        }

        /// <summary>
        /// 设置敌人的飞行路径。
        /// </summary>
        /// <param name="spline">样条曲线容器</param>
        /// <returns>返回当前构建器实例，支持链式调用</returns>
        public EnemyBuilder SetSpline(SplineContainer spline)
        {
            this.spline = spline;
            return this;
        }

        /// <summary>
        /// 设置敌人的武器预制体。
        /// </summary>
        /// <param name="prefab">武器预制体</param>
        /// <returns>返回当前构建器实例，支持链式调用</returns>
        public EnemyBuilder SetWeaponPrefab(GameObject prefab)
        {
            weaponPrefab = prefab;
            return this;
        }

        /// <summary>
        /// 设置敌人的移动速度。
        /// </summary>
        /// <param name="speed">速度值</param>
        /// <returns>返回当前构建器实例，支持链式调用</returns>
        public EnemyBuilder SetSpeed(float speed)
        {
            this.speed = speed;
            return this;
        }

        /// <summary>
        /// 执行构建：实例化敌人并配置其组件。
        /// </summary>
        /// <returns>配置完成的敌人 GameObject</returns>
        public GameObject Build()
        {
            // 1. 实例化基础预制体
            GameObject instance = GameObject.Instantiate(enemyPrefab);

            // 2. 获取或添加路径动画组件（使用扩展方法 GetOrAdd）
            SplineAnimate splineAnimate = instance.GetOrAdd<SplineAnimate>();

            // 3. 配置路径动画参数
            splineAnimate.Container = spline; // 指定路径
            splineAnimate.AnimationMethod = SplineAnimate.Method.Speed; // 使用速度模式（匀速）
            splineAnimate.ObjectUpAxis = SplineAnimate.AlignAxis.ZAxis; // 设置模型的上方向轴（修正朝向）
            splineAnimate.ObjectForwardAxis = SplineAnimate.AlignAxis.YAxis; // 设置模型的前方向轴（修正朝向）
            splineAnimate.MaxSpeed = speed; // 应用速度

            // 4. 修正初始位置：将敌人放置在路径的起点（0.0 处）
            instance.transform.position = spline.EvaluatePosition(0f);

            // 5. 开始动画
            splineAnimate.Play();

            return instance;
        }
    }
}