using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.UI
{
    public class UIToggler : MonoBehaviour
    {
        [SerializeField] GameObject uiPanel;

        public void ToggleUI()
        {
            uiPanel.SetActive(!uiPanel.activeInHierarchy);
        }
    }
}
