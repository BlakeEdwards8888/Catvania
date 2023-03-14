using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines
{
    public abstract class StateMachine : MonoBehaviour
    {
        State currentState;

        public void SwitchState(State newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }

        void Update()
        {
            currentState?.Tick(Time.deltaTime);
        }

        private void OnDestroy()
        {
            currentState?.Exit();
        }
    }
}
