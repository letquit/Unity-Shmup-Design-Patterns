using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shmup
{
    /// <summary>
    /// 输入读取器组件，负责处理玩家输入动作
    /// </summary>
    [RequireComponent(typeof(PlayerInput))]
    public class InputReader : MonoBehaviour
    {
        private PlayerInput playerInput;
        private InputAction moveAction;
        private InputAction fireAction;

        /// <summary>
        /// 获取当前移动输入向量
        /// </summary>
        public Vector2 Move => moveAction.ReadValue<Vector2>();

        /// <summary>
        /// 获取当前射击输入状态
        /// </summary>
        public bool Fire => fireAction.ReadValue<float>() > 0f;

        private void Start()
        {
            // 初始化输入系统组件和动作映射
            playerInput = GetComponent<PlayerInput>();
            moveAction = playerInput.actions["Move"];
            fireAction = playerInput.actions["Fire"];
        }
    }
}
