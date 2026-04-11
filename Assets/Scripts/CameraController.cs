using System;
using UnityEngine;

namespace Shmup
{
    /// <summary>
    /// 相机控制器，用于控制游戏相机跟随玩家并沿战场移动
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float speed = 2f;

        /// <summary>
        /// 初始化相机位置，将其设置为与玩家相同的x、y坐标，保持原有的z坐标
        /// </summary>
        private void Start() =>
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

        /// <summary>
        /// 在每一帧的最后更新相机位置，使相机以恒定速度向上移动
        /// </summary>
        private void LateUpdate()
        {
            // Move the camera along the battlefield at a constant speed
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
    }
}
