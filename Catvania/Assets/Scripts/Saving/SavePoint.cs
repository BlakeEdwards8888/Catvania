using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cat.Controls;
using System;
using Cat.Combat;

namespace Cat.Saving
{
    public class SavePoint : MonoBehaviour
    {
        [SerializeField] float activationRadius;
        [SerializeField] float saveCooldown;

        InputReader inputReader;
        Transform playerTransform;
        float timeSinceSaved = Mathf.Infinity;

        private void Awake()
        {
            inputReader = GetComponent<InputReader>();
            playerTransform = GameObject.FindWithTag("Player").transform;
        }

        private void Update()
        {
            timeSinceSaved += Time.deltaTime;

            if(Vector2.Distance(transform.position, playerTransform.position) <= activationRadius
                && inputReader.MovementValue.y > 0.5f
                && timeSinceSaved >= saveCooldown)
            {
                RestorePlayerHealth();
                Save();
                timeSinceSaved = 0;
            }
        }

        private void RestorePlayerHealth()
        {
            Health playerHealth = playerTransform.GetComponent<Health>();
            Healer playerHealer = playerTransform.GetComponent<Healer>();

            playerHealth.Heal(playerHealth.GetMaxHealth());
            playerHealer.RestoreAllHeals();
        }

        void Save()
        {
            FindObjectOfType<SavingSystem>().Save();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, activationRadius);
        }
    }
}
