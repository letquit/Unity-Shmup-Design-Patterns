using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shmup
{
    public class PlayerHUD : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private Image fuelBar;
        [SerializeField] private TextMeshProUGUI scoreText;

        private void Update()
        {
            healthBar.fillAmount = GameManager.Instance.Player.GetHealthNormalized();
            fuelBar.fillAmount = GameManager.Instance.Player.GetFuelNormalized();
            scoreText.text = $"Score: {GameManager.Instance.GetScore()}";
        }
    }
}