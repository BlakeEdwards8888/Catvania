using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Stone
{
    public class StoneIdleState : StoneBaseState
    {
        readonly int IdleHash = Animator.StringToHash("Idle");

        float timeSinceIdle = 0;

        public StoneIdleState(StoneStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter()
        {
            stateMachine.Animator.Play(IdleHash);
            stateMachine.Health.onTakeDamage += EnterHitstun;
        }

        public override void Tick(float deltaTime)
        {
            HandleFallSpeed();
            Move(0, Vector2.zero);

            timeSinceIdle += deltaTime;

            if (timeSinceIdle >= stateMachine.AttackCooldown)
            {
                stateMachine.SwitchState(new StonePatrolState(stateMachine));
            }
        }

        public override void Exit()
        {
            stateMachine.Health.onTakeDamage -= EnterHitstun;
        }
    }
}
