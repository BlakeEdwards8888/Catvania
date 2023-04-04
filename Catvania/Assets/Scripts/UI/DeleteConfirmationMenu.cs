using Cat.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Cat.UI
{
    public class DeleteConfirmationMenu : MonoBehaviour
    {
        [SerializeField] TMP_Text messageText;

        string fileName;

        public void ToggleUI(string fileName, string displayName)
        {
            this.fileName = fileName;
            messageText.SetText("are you sure you want to delete " + displayName + "?");
            GetComponent<UIToggler>().ToggleUI();
        }

        public void Delete()
        {
            SavingSystem.Instance.Delete(fileName);
        }
    }
}
