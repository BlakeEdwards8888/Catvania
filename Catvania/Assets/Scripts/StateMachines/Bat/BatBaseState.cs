using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Bat
{
    public abstract class BatBaseState : State
    {
        protected BatStateMachine stateMachine;

        public BatBaseState(BatStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        protected void Move(float speed, Vector2 direction)
        {
            stateMachine.Mover.Move((speed * direction) + stateMachine.ForceHandler.GetForce());
        }
    }
}
