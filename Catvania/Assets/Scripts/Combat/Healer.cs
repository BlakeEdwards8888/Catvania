using Cat.Controls;
using Cat.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Combat
{
    public class Healer : MonoBehaviour, ISaveable
    {
        [SerializeField] int healCount;
        [SerializeField] int healAmount;

        int healsRemaining;

        public event Action healEvent;

        private void OnEnable()
        {
            //GetComponent<InputReader>().healEvent += OnHeal;
        }

        private void Awake()
        {
            healsRemaining = healCount;
        }

        public void Heal()
        {
            Health health = GetComponent<Health>();

            health.Heal(healAmount);

            healsRemaining--;

            healEvent?.Invoke();
        }

        public bool CanHeal()
        {
            Health health = GetComponent<Health>();

            if (healsRemaining == 0 || health.GetMaxHealth() == health.GetCurrentHealth() 
                || health.GetCurrentHealth() == 0) return false;

            return true;
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

        public object CaptureState()
        {
            return healCount;
        }

        public void RestoreState(object state)
        {
            healCount = (int)state;
        }

        private void OnDisable()
        {
            //GetComponent<InputReader>().healEvent -= OnHeal;
        }
    }
}
