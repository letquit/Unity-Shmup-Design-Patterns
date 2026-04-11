using System.Collections.Generic;
using Eflatun.SceneReference;
using MEC;
using UnityEngine.SceneManagement;

namespace Shmup
{
    /// <summary>
    /// 场景加载器类，负责管理场景的异步加载流程
    /// </summary>
    public static class Loader
    {
        private static SceneReference loadingScene = new (SceneGuidToPathMapProvider.ScenePathToGuidMap["Assets/Scenes/Loading.unity"]);
        private static SceneReference targetScene;

        /// <summary>
        /// 加载指定场景
        /// </summary>
        /// <param name="scene">要加载的目标场景引用</param>
        public static void Load(SceneReference scene)
        {
            targetScene = scene;
            
            // 先加载loading场景
            SceneManager.LoadScene(loadingScene.Name);
            // 启动协程加载目标场景
            Timing.RunCoroutine(LoadTargetScene());
        }

        /// <summary>
        /// 异步加载目标场景的协程方法
        /// </summary>
        /// <returns>等待帧数的枚举器</returns>
        private static IEnumerator<float> LoadTargetScene()
        {
            // 等待一帧以确保场景切换完成
            yield return Timing.WaitForOneFrame;
            SceneManager.LoadScene(targetScene.Name);
        }
    }
}
