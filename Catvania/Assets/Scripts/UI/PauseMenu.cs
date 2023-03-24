using Cat.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] GameObject pausePanel;

        private void OnEnable()
        {
            GetComponent<InputReader>().pauseEvent += ToggleUI;
        }

        public void ToggleUI()
        {
            pausePanel.SetActive(!pausePanel.activeInHierarchy);
        }

        private void OnDisable()
        {
            GetComponent<InputReader>().pauseEvent -= ToggleUI;
        }
    }
}
