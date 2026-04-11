using UnityEngine;
using UnityEngine.UI;

namespace Shmup
{
    /// <summary>
    /// Boss血条管理器，负责显示Boss的生命值状态
    /// </summary>
    public class BossHealthBar : MonoBehaviour
    {
        [SerializeField] private Boss boss;
        [SerializeField] private Image healthBar;

        /// <summary>
        /// 初始化方法，在对象唤醒时订阅Boss的血量变化事件
        /// </summary>
        private void Awake()
        {
            boss.OnHealthChanged += OnHealthChanged;
        }
        
        /// <summary>
        /// 当Boss血量发生变化时的回调方法，更新血条的填充比例
        /// </summary>
        private void OnHealthChanged()
        {
            healthBar.fillAmount = boss.GetHealthNormalized();
        }
    }
}
