using System;
using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Shmup
{
    /// <summary>
    /// 主菜单用户界面控制器
    /// 负责处理主菜单中的按钮点击事件和游戏初始化设置
    /// </summary>
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private SceneReference startingLevel;
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;

        /// <summary>
        /// 在对象初始化时调用，设置按钮点击事件监听器并重置时间比例
        /// </summary>
        private void Awake()
        {
            // 为播放按钮添加点击事件监听器，用于加载起始关卡
            playButton.onClick.AddListener(() => Loader.Load(startingLevel));
            
            // 为退出按钮添加点击事件监听器，用于退出游戏
            quitButton.onClick.AddListener(() => Helpers.QuitGame());
            
            // 重置时间比例为正常速度
            Time.timeScale = 1f;
        }
    }
}
