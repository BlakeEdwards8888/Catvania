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

        public event Action<float> onTakeDamage;
        public event Action onDeath;
        public event Action onHealthChanged;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage, float hitstunDuration)
        {
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

        public int GetMaxHealth()
        {
            return maxHealth;
        }

        public int GetCurrentHealth()
        {
            return currentHealth;
        }

        public object CaptureState()
        {
            return maxHealth;
        }

        public void RestoreState(object state)
        {
            maxHealth = (int)state;
        }
    }
}
