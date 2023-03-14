using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Sword
{
    public class SwordProjectileState: SwordBaseState
    {
        private const float GroundCheckDistance = 0.1f;

        float timeSinceEnteredState = 0;

        public SwordProjectileState(SwordStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter()
        {
            
        }

        public override void Tick(float deltaTime)
        {
            timeSinceEnteredState += deltaTime;

            if(timeSinceEnteredState >= stateMachine.TimeUntilReturn)
            {
                stateMachine.SwitchState(new SwordReturnState(stateMachine));
            }

            if (CollidedWithGroundInFront())
            {
                stateMachine.SwitchState(new SwordPlatformState(stateMachine));
            }
        }

        private bool CollidedWithGroundInFront()
        {
            RaycastHit2D[] results = new RaycastHit2D[1];
            return stateMachine.Rb2d.Cast(Vector2.right * stateMachine.transform.lossyScale,
                stateMachine.GroundFilter, results, GroundCheckDistance) > 0;
        }

        public override void Exit() {}
    }
}
