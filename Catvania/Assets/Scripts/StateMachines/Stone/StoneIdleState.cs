using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Stone
{
    public class StoneIdleState : StoneBaseState
    {
        readonly int IdleHash = Animator.StringToHash("Idle");

        float timeSinceIdle = 0;
        float duration;

        public StoneIdleState(StoneStateMachine stateMachine, float duration) : base(stateMachine) 
        {
            this.duration = duration;
        }

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

            if (timeSinceIdle >= duration)
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
