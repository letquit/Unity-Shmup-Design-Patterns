using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shmup
{
    /// <summary>
    /// 玩家HUD界面管理器，负责更新游戏界面上的生命值、燃料值和分数显示
    /// </summary>
    public class PlayerHUD : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private Image fuelBar;
        [SerializeField] private TextMeshProUGUI scoreText;

        /// <summary>
        /// 每帧更新HUD界面信息，包括生命值条、燃料值条和分数文本
        /// </summary>
        private void Update()
        {
            // 更新生命值进度条
            healthBar.fillAmount = GameManager.Instance.Player.GetHealthNormalized();
            // 更新燃料值进度条
            fuelBar.fillAmount = GameManager.Instance.Player.GetFuelNormalized();
            // 更新分数显示文本
            scoreText.text = $"Score: {GameManager.Instance.GetScore()}";
        }
    }
}
