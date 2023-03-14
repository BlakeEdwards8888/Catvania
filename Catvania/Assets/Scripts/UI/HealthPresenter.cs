using Cat.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cat.UI
{
    public class HealthPresenter : MonoBehaviour
    {
        private const float FillSmoothTime = 0.1f;

        [SerializeField] Health health;
        [SerializeField] Slider healthBar;
        [SerializeField] float borderWidth;

        float targetValue;

        float fillVelocity;

        private void OnEnable()
        {
            health.onHealthChanged += UpdateHealthBar;
        }

        private void Start()
        {
            UpdateHealthBarInstantly();
        }

        void UpdateHealthBar()
        {
            targetValue = CalculateHealthPercentage();
        }

        void UpdateHealthBarInstantly()
        {
            GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, health.GetMaxHealth() + borderWidth);
            targetValue = CalculateHealthPercentage();
            healthBar.value = targetValue;
        }

        private float CalculateHealthPercentage()
        {
            return (float)health.GetCurrentHealth() / health.GetMaxHealth() * 100;
        }

        private void Update()
        {
            if (healthBar.value == targetValue) return;

            healthBar.value = Mathf.SmoothDamp(healthBar.value, targetValue, ref fillVelocity, FillSmoothTime);

            if(Mathf.Approximately(healthBar.value, targetValue))
            {
                healthBar.value = targetValue;
            }
        }

        private void OnDisable()
        {
            health.onHealthChanged -= UpdateHealthBar;
        }
    }
}
