using Cat.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.UI
{
    public class SaveConfirmationDisplay : MonoBehaviour
    {
        [SerializeField] float displayDuration;

        UIToggler uiToggler;
        SavePoint savePoint;

        private void OnEnable()
        {
            if (savePoint == null) return;

            savePoint.onSaved += SavePoint_OnSaved;
        }

        private void Awake()
        {
            savePoint = FindObjectOfType<SavePoint>();
            uiToggler = GetComponent<UIToggler>();
        }

        private void SavePoint_OnSaved()
        {
            StartCoroutine(DisplayCoroutine());
        }

        IEnumerator DisplayCoroutine()
        {
            uiToggler.ToggleUI();

            yield return new WaitForSeconds(displayDuration);

            uiToggler.ToggleUI();
        }

        private void OnDisable()
        {
            if (savePoint == null) return;

            savePoint.onSaved -= SavePoint_OnSaved;
        }
    }
}
