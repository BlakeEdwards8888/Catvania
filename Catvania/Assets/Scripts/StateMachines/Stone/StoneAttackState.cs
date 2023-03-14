using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Stone
{
    public class StoneAttackState : StoneBaseState
    {
        readonly int AttackHash = Animator.StringToHash("Attack");

        public StoneAttackState(StoneStateMachine stateMachine) : base(stateMachine){}

        public override void Enter()
        {
            stateMachine.Animator.Play(AttackHash);
        }

        public override void Tick(float deltaTime)
        {
            HandleFallSpeed();

            Move(0, Vector2.zero);

            AnimatorStateInfo anim = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

            if (anim.IsTag("Attack") && anim.normalizedTime >= 1)
            {
                stateMachine.SwitchState(new StoneIdleState(stateMachine));
            }
        }

        public override void Exit()
        {

        }
    }
}
