using UnityEngine;
using Cat.Controls;
using Cat.Combat;
using System;

namespace Cat.Saving
{
    public class SavePoint : InteractableObject
    {
        [SerializeField] float saveCooldown;

        float timeSinceSaved = Mathf.Infinity;

        public event Action onSaved;

        protected override void Update()
        {
            base.Update();
            timeSinceSaved += Time.deltaTime;
        }

        protected override void ExecuteAction()
        {
            RestorePlayerHealth();
            Save();

            timeSinceSaved = 0;
        }

        protected override bool IsInteractable()
        {
            return timeSinceSaved >= saveCooldown && base.IsInteractable();
        }

        private void RestorePlayerHealth()
        {
            Health playerHealth = playerTransform.GetComponent<Health>();
            Healer playerHealer = playerTransform.GetComponent<Healer>();

            playerHealth.Heal(playerHealth.GetMaxHealth());
            playerHealer.RestoreAllHeals();
        }

        private void Save()
        {
            FindObjectOfType<SavingSystem>().Save();

            onSaved?.Invoke();
        }
    }
}
