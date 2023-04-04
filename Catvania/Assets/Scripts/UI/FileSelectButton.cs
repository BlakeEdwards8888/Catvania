using Cat.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Cat.UI
{
    public class FileSelectButton : MonoBehaviour
    {
        [SerializeField] string fileName;

        public void OnClick()
        {
            SavingSystem savingSystem = SavingSystem.Instance;

            savingSystem.StartCoroutine(savingSystem.LoadLastScene(fileName));
        }

        public string GetFileName()
        {
            return fileName;
        }

        public string GetDisplayName()
        {
            TMP_Text tmpText = GetComponentInChildren<TMP_Text>();
            return tmpText.text;
        }
    }
}
