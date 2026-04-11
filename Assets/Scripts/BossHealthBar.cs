using UnityEngine;
using UnityEngine.UI;

namespace Shmup
{
    public class BossHealthBar : MonoBehaviour
    {
        [SerializeField] private Boss boss;
        [SerializeField] private Image healthBar;

        private void Awake()
        {
            boss.OnHealthChanged += OnHealthChanged;
        }
        
        private void OnHealthChanged()
        {
            healthBar.fillAmount = boss.GetHealthNormalized();
        }
    }
}