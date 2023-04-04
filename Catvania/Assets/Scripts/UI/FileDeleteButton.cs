using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.UI
{
    public class FileDeleteButton : MonoBehaviour
    {
        [SerializeField] FileSelectButton fileSelectButton;

        public void OnClick()
        {
            DeleteConfirmationMenu deleteConfirmationMenu = FindObjectOfType<DeleteConfirmationMenu>();

            deleteConfirmationMenu.ToggleUI(fileSelectButton.GetFileName(), fileSelectButton.GetDisplayName());
        }
    }
}
