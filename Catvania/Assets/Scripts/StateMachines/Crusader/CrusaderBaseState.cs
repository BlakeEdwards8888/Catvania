using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Crusader
{
    public abstract class CrusaderBaseState : State
    {
        protected CrusaderStateMachine stateMachine;

        public CrusaderBaseState(CrusaderStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
    }
}
