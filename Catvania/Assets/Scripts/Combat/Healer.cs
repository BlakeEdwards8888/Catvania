using Cat.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Combat
{
    public class Healer : MonoBehaviour
    {
        [SerializeField] int healCount;
        [SerializeField] int healAmount;

        int healsRemaining;

        public event Action healEvent;

        private void OnEnable()
        {
            GetComponent<InputReader>().healEvent += OnHeal;
        }

        private void Awake()
        {
            healsRemaining = healCount;
        }

        void OnHeal()
        {
            Health health = GetComponent<Health>();

            if (healsRemaining == 0 || health.GetMaxHealth() == health.GetCurrentHealth() || health.GetCurrentHealth() == 0) return;

            health.Heal(healAmount);

            healsRemaining--;

            healEvent?.Invoke();
        }

        private void OnDisable()
        {
            GetComponent<InputReader>().healEvent -= OnHeal;
        }

        public int GetHealCount()
        {
            return healCount;
        }

        public int GetHealsRemaining()
        {
            return healsRemaining;
        }

        public void RestoreAllHeals()
        {
            healsRemaining = healCount;
            healEvent?.Invoke();
        }
    }
}
