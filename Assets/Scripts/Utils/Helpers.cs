using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// 通用辅助类。
    /// 包含项目中常用的静态工具方法。
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// 退出游戏。
        /// 根据运行平台自动适配退出逻辑：
        /// - 在编辑器中：停止播放模式
        /// - 在构建版本中：关闭应用程序
        /// </summary>
        public static void QuitGame()
        {
#if UNITY_EDITOR
            // 如果在 Unity 编辑器中，停止播放模式
            UnityEditor.EditorApplication.isPlaying = false;
#else
            // 如果在打包后的版本（PC/移动端等），关闭应用程序
            Application.Quit();
#endif
        }
    }
}