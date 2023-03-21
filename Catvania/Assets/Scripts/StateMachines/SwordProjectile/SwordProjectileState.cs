using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Sword
{
    public class SwordProjectileState: SwordBaseState
    {
        readonly int ProjectileHash = Animator.StringToHash("Projectile");
        private const float GroundCheckDistance = 0.1f;

        float timeSinceEnteredState = 0;

        public SwordProjectileState(SwordStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter()
        {
            stateMachine.Animator.Play(ProjectileHash);
        }

        public override void Tick(float deltaTime)
        {
            timeSinceEnteredState += deltaTime;

            if(timeSinceEnteredState >= stateMachine.TimeUntilReturn)
            {
                stateMachine.SwitchState(new SwordReturnState(stateMachine));
            }

            RaycastHit2D[] results = new RaycastHit2D[1];

            if (CollidedWithGroundInFront(ref results))
            {
                SnapEntryPointToHit(results[0]);
                stateMachine.SwitchState(new SwordPlatformState(stateMachine));
            }
        }

        public override void Exit() { }

        private void SnapEntryPointToHit(RaycastHit2D hit)
        {
            stateMachine.transform.position = hit.point + ((Vector2)stateMachine.transform.position - (Vector2)stateMachine.EntryPoint.position);
        }

        private bool CollidedWithGroundInFront(ref RaycastHit2D[] results)
        {
            return stateMachine.Rb2d.Cast(Vector2.right * stateMachine.transform.lossyScale,
                stateMachine.GroundFilter, results, GroundCheckDistance) > 0;
        }
    }
}
