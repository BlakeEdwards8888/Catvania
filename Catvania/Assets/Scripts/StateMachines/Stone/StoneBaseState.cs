using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Stone
{
    public abstract class StoneBaseState : State
    {
        protected StoneStateMachine stateMachine;

        public StoneBaseState(StoneStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        protected void EnterHitstun(float duration)
        {
            stateMachine.SwitchState(new StoneHitstunState(stateMachine, duration));
        }

        protected void HandleFallSpeed()
        {
            if (stateMachine.Rb2d.velocity.y < 0)
            {
                stateMachine.Rb2d.gravityScale = stateMachine.FallingGravityScale;
            }
            else
            {
                stateMachine.Rb2d.gravityScale = 1;
            }
        }

        protected void Move(float speed, Vector2 direction)
        {
            stateMachine.Mover.Move(CalculateMovement(speed, direction) + stateMachine.ForceHandler.GetForce());
        }

        private Vector2 CalculateMovement(float speed, Vector2 direction)
        {
            Vector2 gravity = new Vector2(0, stateMachine.Rb2d.velocity.y);
            return (direction * speed) + gravity;
        }
    }
}
