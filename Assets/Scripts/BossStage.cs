using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shmup
{
    /// <summary>
    /// Boss关卡管理器，负责管理Boss阶段的敌人系统和状态
    /// </summary>
    public class BossStage : MonoBehaviour
    {
        /// <summary>
        /// 敌人系统列表，包含该Boss阶段的所有敌人
        /// </summary>
        public List<Enemy> enemySystems;
        
        /// <summary>
        /// Boss是否处于无敌状态
        /// </summary>
        public bool IsBossInvulnerable = true;

        /// <summary>
        /// 初始化方法，在对象创建时执行
        /// </summary>
        private void Awake()
        {
            // 遍历所有敌人系统，初始状态下将它们都设置为非激活状态
            foreach (var system in enemySystems)
            {
                system.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 初始化Boss阶段，激活所有敌人系统
        /// </summary>
        public void InitializeStage()
        {
            // 遍历所有敌人系统，将它们设置为激活状态以开始战斗
            foreach (var system in enemySystems)
            {
                system.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// 检查Boss阶段是否完成
        /// </summary>
        /// <returns>如果所有敌人都被击败或不存在，则返回true；否则返回false</returns>
        public bool IsStageComplete()
        {
            // 检查所有敌人系统，如果所有敌人的血量归零或对象为空，则认为阶段完成
            return enemySystems.All(system => system == null || !(system.GetHealthNormalized() > 0));
        }
    }
}
