using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cat.Controls
{
    public class InputReader : MonoBehaviour, Controls.IPlayerActions
    {
        public Vector2 MovementValue { get; private set; }

        Controls controls;

        public event Action jumpEvent;
        public event Action jumpCancelEvent;
        public event Action attackEvent;
        public event Action specialEvent;
        public event Action healEvent;

        void Start()
        {
            controls = new Controls();
            controls.Player.SetCallbacks(this);

            controls.Player.Enable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MovementValue = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                jumpEvent?.Invoke();
            }else if (context.canceled)
            {
                jumpCancelEvent?.Invoke();
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            attackEvent?.Invoke();
        }

        public void OnSpecial(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            specialEvent?.Invoke();
        }

        public void OnHeal(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            healEvent?.Invoke();
        }
    }
}
