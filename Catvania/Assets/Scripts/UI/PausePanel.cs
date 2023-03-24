using Cat.Controls;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cat.UI
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField] GameObject firstButton;

        InputReader playerInputReader;

        private void Awake()
        {
            GameObject player = GameObject.FindWithTag("Player");
            playerInputReader = player.GetComponent<InputReader>();
        }

        private void OnEnable()
        {
            EventSystem.current.SetSelectedGameObject(firstButton);
            Time.timeScale = 0;
            playerInputReader.GetControls().Disable();
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
            playerInputReader.GetControls().Enable();
        }
    }
}
