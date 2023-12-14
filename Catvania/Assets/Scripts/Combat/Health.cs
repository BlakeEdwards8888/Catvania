using Cat.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Combat
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] int maxHealth;

        int currentHealth;
        bool isInvulnerable;

        public event Action<float> onTakeDamage;
       
        public event Action onDeath;
        public event Action onHealthChanged;
        public event Action onMaxHealthChanged;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage, float hitstunDuration)
        {
            if (IsDead()) return;

            currentHealth = Mathf.Max(0, currentHealth - damage);

            if(currentHealth == 0)
            {
                onDeath?.Invoke();
            }
            else
            {
                onTakeDamage?.Invoke(hitstunDuration);
            }

            onHealthChanged?.Invoke();
        }

        public void Heal(int amount)
        {
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

            onHealthChanged?.Invoke();
        }

        public void AddMaxHealth(int healthIncrease)
        {
            maxHealth += healthIncrease;
            onMaxHealthChanged?.Invoke();
            Heal(maxHealth);
        }

        public int GetMaxHealth()
        {
            return maxHealth;
        }

        public int GetCurrentHealth()
        {
            return currentHealth;
        }

        public bool IsInvulnerable()
        {
            return isInvulnerable;
        }

        public bool IsDead()
        {
            return currentHealth == 0;
        }

        public object CaptureState()
        {
            return maxHealth;
        }

        public void RestoreState(object state)
        {
            maxHealth = (int)state;
        }

        public void SetIsInvulnerable(bool value)
        {
            isInvulnerable = value;
        }
    }
}
