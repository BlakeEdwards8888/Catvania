using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Spider
{
    public abstract class SpiderBaseState : State
    {
        protected SpiderStateMachine stateMachine;

        public SpiderBaseState(SpiderStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        protected void Move(float speed)
        {
            stateMachine.Mover.Move(CalculateMovement(speed) + stateMachine.ForceHandler.GetForce());
        }

        private Vector2 CalculateMovement(float speed)
        {
            return new Vector2(speed, stateMachine.Rb2d.velocity.y);
        }
    }
}
