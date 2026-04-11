using System;
using UnityEngine;

namespace Shmup
{
    /// <summary>
    /// 玩家控制器。
    /// 处理玩家的输入、移动、边界限制以及机身倾斜动画。
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f; // 移动速度
        [SerializeField] private float smoothness = 0.1f; // 移动平滑度（插值时间）
        [SerializeField] private float leanAngle = 15f; // 最大倾斜角度（用于转向视觉效果）
        [SerializeField] private float leanSpeed = 5f; // 倾斜恢复速度

        [SerializeField] private GameObject model; // 玩家模型（预留）

        [Header("Camera Bounds")]
        [SerializeField] private Transform cameraFollow; // 跟随摄像机的位置（用于计算边界）
        [SerializeField] private float minX = -8f; // 相对于摄像机的最小 X 偏移
        [SerializeField] private float maxX = 8f; // 相对于摄像机的最大 X 偏移
        [SerializeField] private float minY = -4f; // 相对于摄像机的最小 Y 偏移
        [SerializeField] private float maxY = 4f; // 相对于摄像机的最大 Y 偏移

        private InputReader input; // 输入读取器组件

        private Vector3 currentVelocity; // 当前平滑速度（用于 SmoothDamp）
        private Vector3 targetPosition; // 目标位置（用于处理输入后的理想位置）

        private void Start()
        {
            input = GetComponent<InputReader>();
        }

        private void Update()
        {
            // 1. 根据输入计算目标位置
            // 使用 Time.deltaTime 确保帧率无关的平滑移动
            targetPosition += new Vector3(input.Move.x, input.Move.y, 0f) * (speed * Time.deltaTime);
            
            // 2. 计算基于摄像机视角的玩家边界
            var minPlayerX = cameraFollow.position.x + minX;
            var maxPlayerX = cameraFollow.position.x + maxX;
            var minPlayerY = cameraFollow.position.y + minY;
            var maxPlayerY = cameraFollow.position.y + maxY;
            
            // 3. 限制玩家位置，使其不能移出摄像机视野
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPlayerX, maxPlayerX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPlayerY, maxPlayerY);
            
            // 4. 平滑移动玩家到目标位置
            // SmoothDamp 提供比 Lerp 更自然的减速效果
            transform.position =
                Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothness);
            
            // 5. 计算倾斜旋转效果
            // 根据水平移动方向（input.Move.x）计算目标倾斜角度
            // 向左移动（x < 0）时，角度为正；向右移动（x > 0）时，角度为负
            var targetRotationAngle = -input.Move.x * leanAngle;

            // 获取当前 Y 轴旋转角度
            var currentYRotation = transform.localEulerAngles.y;
            // 平滑插值当前旋转到目标旋转
            var newYRotation = Mathf.LerpAngle(currentYRotation, targetRotationAngle, leanSpeed * Time.deltaTime);
            
            // 6. 应用旋转效果
            // 只修改 Y 轴旋转（假设模型朝向正确），X 和 Z 保持为 0
            transform.localEulerAngles = new Vector3(0f, newYRotation, 0f);
        }
    }
}