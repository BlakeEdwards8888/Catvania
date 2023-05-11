using Cat.Combat;
using Cat.Saving;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cat.UI
{
    public class PlayerDeathScreen : MonoBehaviour
    {
        const float FADE_IN_DELAY = 2f;
        const float FADE_IN_DURATION = 1f;

        [SerializeField] InputAction inputAction;
        [SerializeField] TMP_Text continueText;

        Fader fader;
        Health playerHealth;

        private void OnEnable()
        {
            playerHealth.onDeath += PlayerHealth_OnDeath;
            inputAction.performed += InputAction_Performed;
        }

        private void Awake()
        {
            fader = GetComponent<Fader>();
            playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void Start()
        {
            //check if player is dead in case they got knocked into a loading zone
            //with the hit that killed them
            if (playerHealth.IsDead())
            {
                PlayerHealth_OnDeath();
            }
        }

        void PlayerHealth_OnDeath()
        {
            StartCoroutine(DeathScreenCoroutine());
        }

        IEnumerator DeathScreenCoroutine()
        {
            yield return new WaitForSeconds(FADE_IN_DELAY);

            yield return fader.FadeIn(FADE_IN_DURATION);

            continueText.gameObject.SetActive(true);

            inputAction.Enable();

            yield return null;
        }

        void InputAction_Performed(InputAction.CallbackContext context)
        {
            SavingSystem.Instance.LoadLastScene();
        }

        private void OnDisable()
        {
            playerHealth.onDeath -= PlayerHealth_OnDeath;
            inputAction.performed -= InputAction_Performed;
            inputAction.Disable();
        }
    }
}
