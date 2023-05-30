using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Player
{
    public class PlayerAttackUState : PlayerBaseState
    {
        readonly int AttackHash = Animator.StringToHash("AttackU");

        public PlayerAttackUState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.Hitbox.Setup(stateMachine.GetAttack("AttackU"));
            stateMachine.Animator.Play(AttackHash);
            stateMachine.PlaySound("Attack");
        }

        public override void Tick(float deltaTime)
        {
            if (IsGrounded())
            {
                Move(0);
            }
            else
            {
                Move(stateMachine.FreeMoveSpeed);
            }

            HandleFallSpeed();

            AnimatorStateInfo anim = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

            if (anim.IsTag("Attack") && anim.normalizedTime >= 1)
            {
                ReturnToLocomotion();
            }
        }

        public override void Exit() { }
    }
}
