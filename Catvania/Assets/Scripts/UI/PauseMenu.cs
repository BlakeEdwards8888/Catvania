using Cat.Combat;
using Cat.Controls;
using UnityEngine;

namespace Cat.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] UIToggler uiToggler;

        Health playerHealth;

        private void OnEnable()
        {
            GetComponent<InputReader>().pauseEvent += InputReader_PauseEvent;
        }

        private void Awake()
        {
            playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void InputReader_PauseEvent()
        {
            if (playerHealth.IsDead()) return;

            uiToggler.ToggleUI();
        }

        private void OnDisable()
        {
            GetComponent<InputReader>().pauseEvent -= InputReader_PauseEvent;
        }
    }
}
