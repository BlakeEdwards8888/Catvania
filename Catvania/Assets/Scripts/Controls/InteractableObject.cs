using UnityEngine;
using UnityEngine.InputSystem;

namespace Cat.Controls
{
    public abstract class InteractableObject : MonoBehaviour
    {
        [SerializeField] InputAction interaction;
        [SerializeField] float activationRadius;
        [SerializeField] GameObject instructionalDisplay;

        protected Transform playerTransform;

        protected virtual void OnEnable()
        {
            interaction.Enable();
            interaction.performed += Interaction_Performed;
        }

        protected virtual void Awake()
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }

        protected virtual void Update()
        {
            instructionalDisplay.SetActive(IsInteractable());
        }

        private void Interaction_Performed(InputAction.CallbackContext context)
        {
            if (!IsInteractable()) return;
            if (Time.timeScale == 0) return;

            ExecuteAction();
        }

        protected abstract void ExecuteAction();

        protected virtual bool IsInteractable()
        {
            return PlayerIsInRange();
        }

        private bool PlayerIsInRange()
        {
            return Vector2.Distance(transform.position, playerTransform.position) <= activationRadius;
        }

        protected virtual void OnDisable()
        {
            interaction.performed -= Interaction_Performed;
            interaction.Disable();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, activationRadius);
        }
    }
}
