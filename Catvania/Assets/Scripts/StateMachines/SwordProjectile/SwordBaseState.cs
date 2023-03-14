using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Sword
{
    public abstract class SwordBaseState: State
    {
        protected SwordStateMachine stateMachine;

        public SwordBaseState(SwordStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
    }
}
