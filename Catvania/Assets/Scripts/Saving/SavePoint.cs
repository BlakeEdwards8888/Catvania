using UnityEngine;
using Cat.Controls;
using Cat.Combat;
using System;
using Cat.Flags;

namespace Cat.Saving
{
    public class SavePoint : InteractableObject
    {
        [SerializeField] float saveCooldown;

        float timeSinceSaved = Mathf.Infinity;
        AudioSource audioSource;

        public event Action onSaved;

        protected override void OnEnable()
        {
            base.OnEnable();
            SavingSystem.Instance.onSceneLoaded += SavingSystem_OnSceneLoaded;
        }

        protected override void Awake()
        {
            base.Awake();
            audioSource = GetComponent<AudioSource>();
        }

        protected override void Update()
        {
            base.Update();
            timeSinceSaved += Time.deltaTime;
        }

        protected override void ExecuteAction()
        {
            RestoreAllEnemies();
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

        void RestoreAllEnemies()
        {
            FlagSystem flagSystem = Resources.Load<FlagSystem>("Default Flag System");

            flagSystem.SetAllFlagsWithPrefix("ENEMY_", false);
        }

        private void Save()
        {
            FindObjectOfType<SavingSystem>().Save();
            audioSource.Play();

            onSaved?.Invoke();
        }

        void SavingSystem_OnSceneLoaded()
        {
            RestorePlayerHealth();
            RestoreAllEnemies();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            SavingSystem.Instance.onSceneLoaded -= SavingSystem_OnSceneLoaded;
        }
    }
}
