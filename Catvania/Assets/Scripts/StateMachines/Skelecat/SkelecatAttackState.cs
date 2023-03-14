using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Skelecat
{
    public class SkelecatAttackState : SkelecatBaseState
    {
        readonly int AttackHash = Animator.StringToHash("Attack");

        public SkelecatAttackState(SkelecatStateMachine stateMachine) : base(stateMachine)
        {
        }

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
                ReturnToLocomotion();
            }
        }

        public override void Exit()
        {

        }
    }
}
