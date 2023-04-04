using UnityEngine;
using UnityEngine.EventSystems;

namespace Cat.UI
{
    public class ButtonMenu: MonoBehaviour
    {
        [SerializeField] GameObject firstButton;

        private void OnEnable()
        {
            SetSelectedGameObjectInEventSystem();
        }

        public void SetSelectedGameObjectInEventSystem()
        {
            EventSystem.current.SetSelectedGameObject(firstButton);
        }
    }
}
