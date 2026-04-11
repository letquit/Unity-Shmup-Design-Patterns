using UnityEngine;

namespace Shmup
{
    /// <summary>
    /// 表示敌人类型的脚本化对象，用于定义敌人的预制件、武器和移动速度等属性
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyType", menuName = "Shmup/EnemyType", order = 0)]
    public class EnemyType : ScriptableObject
    {
        /// <summary>
        /// 敌人游戏对象的预制件
        /// </summary>
        public GameObject enemyPrefab;
        
        /// <summary>
        /// 敌人武器游戏对象的预制件
        /// </summary>
        public GameObject weaponPrefab;
        
        /// <summary>
        /// 敌人的移动速度
        /// </summary>
        public float speed;
    }
}
