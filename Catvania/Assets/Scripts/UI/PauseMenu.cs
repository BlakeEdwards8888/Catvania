using Cat.Controls;
using UnityEngine;

namespace Cat.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] UIToggler uiToggler;

        private void OnEnable()
        {
            GetComponent<InputReader>().pauseEvent += InputReader_PauseEvent;
        }

        private void InputReader_PauseEvent()
        {
            uiToggler.ToggleUI();
        }

        private void OnDisable()
        {
            GetComponent<InputReader>().pauseEvent -= InputReader_PauseEvent;
        }
    }
}
