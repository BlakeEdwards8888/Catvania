using Cat.Audio;
using Cat.Combat;
using Cat.Controls;
using UnityEngine;

namespace Cat.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] UIToggler uiToggler;
        [SerializeField] AudioClip pauseOn, pauseOff;

        Health playerHealth;
        SoundEmitter soundEmitter;

        private void OnEnable()
        {
            GetComponent<InputReader>().pauseEvent += InputReader_PauseEvent;
        }

        private void Awake()
        {
            playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
            soundEmitter = GetComponent<SoundEmitter>();
        }

        private void InputReader_PauseEvent()
        {
            if (playerHealth.IsDead()) return;
            if (Time.timeScale < 1 && !uiToggler.GetToggleState()) return;

            soundEmitter.PlaySound(uiToggler.GetToggleState() ? pauseOff : pauseOn);
            uiToggler.ToggleUI();
        }

        private void OnDisable()
        {
            GetComponent<InputReader>().pauseEvent -= InputReader_PauseEvent;
        }
    }
}
