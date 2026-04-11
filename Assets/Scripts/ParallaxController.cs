using System;
using UnityEngine;

namespace Shmup
{
    /// <summary>
    /// 视差控制器组件，用于实现背景层的视差滚动效果
    /// </summary>
    public class ParallaxController : MonoBehaviour
    {
        [SerializeField] private Transform[] backgrounds;   // 背景层级的数组
        [SerializeField] private float smoothing = 10f;     // 视差效果的平滑度
        [SerializeField] private float multiplier = 15f;    // 每一层视差效果的递增倍率

        private Transform cam;  // 主摄像机的引用
        private Vector3 previousCamPos; // 上一帧摄像机的位置

        private void Awake() => cam = Camera.main.transform;
        
        private void Start() => previousCamPos = cam.position;

        /// <summary>
        /// 更新每一帧的视差效果
        /// </summary>
        private void Update()
        {
            // 计算并应用每一层背景的视差偏移
            for (var i = 0; i < backgrounds.Length; i++)
            {
                var parallax = (previousCamPos.y - cam.position.y) * (i * multiplier);
                var targetY = backgrounds[i].position.y + parallax;

                var targetPosition = new Vector3(backgrounds[i].position.x, targetY, backgrounds[i].position.z);

                backgrounds[i].position =
                    Vector3.Lerp(backgrounds[i].position, targetPosition, smoothing * Time.deltaTime);
            }
            
            previousCamPos = cam.position;
        }
    }
}
