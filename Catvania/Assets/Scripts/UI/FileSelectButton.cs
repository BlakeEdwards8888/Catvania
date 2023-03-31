using Cat.Saving;
using System.Collections;
using System.Collections.Generic;
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
    }
}
