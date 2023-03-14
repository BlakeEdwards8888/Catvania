using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Skelecat
{
    public abstract class SkelecatBaseState : State
    {
        private const float GroundCheckDistance = 0.1f;

        protected SkelecatStateMachine stateMachine;

        public SkelecatBaseState(SkelecatStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
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

        protected void ReturnToLocomotion()
        {
            stateMachine.SwitchState(new SkelecatPatrolState(stateMachine));
        }

        protected bool IsGrounded()
        {
            return Physics2D.Raycast(stateMachine.transform.position, Vector2.down, GroundCheckDistance, stateMachine.GroundFilter.layerMask);
        }
        protected void EnterHitstun(float duration)
        {
            stateMachine.SwitchState(new SkelecatHitstunState(stateMachine, duration));
        }
    }
}
