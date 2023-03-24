using Cat.Controls;
using UnityEngine;

namespace Cat.UI
{
    public class PausePanel : MonoBehaviour
    {
        InputReader playerInputReader;

        private void Awake()
        {
            GameObject player = GameObject.FindWithTag("Player");
            playerInputReader = player.GetComponent<InputReader>();
        }

        private void OnEnable()
        {
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
