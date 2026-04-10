using System.Collections.Generic;
using Eflatun.SceneReference;
using MEC;
using UnityEngine.SceneManagement;

namespace Shmup
{
    public static class Loader
    {
        private static SceneReference loadingScene = new (SceneGuidToPathMapProvider.ScenePathToGuidMap["Assets/Scenes/Loading.unity"]);
        private static SceneReference targetScene;

        public static void Load(SceneReference scene)
        {
            targetScene = scene;
            
            SceneManager.LoadScene(loadingScene.Name);
            Timing.RunCoroutine(LoadTargetScene());
        }

        private static IEnumerator<float> LoadTargetScene()
        {
            yield return Timing.WaitForOneFrame;
            SceneManager.LoadScene(targetScene.Name);
        }
    }
}