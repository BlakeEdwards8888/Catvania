using Cat.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Player
{
    public class PlayerAttackDState : PlayerBaseState
    {
        private const float HitBounce = 3;
        readonly int AttackHash = Animator.StringToHash("AttackD");

        Attack attack;

        public PlayerAttackDState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.Hitbox.onHit += OnHit;

            attack = stateMachine.GetAttack("AttackD");

            stateMachine.Hitbox.Setup(attack);
            stateMachine.Animator.Play(AttackHash);
        }

        public override void Tick(float deltaTime)
        {
            Move(stateMachine.FreeMoveSpeed);

            HandleFallSpeed();

            AnimatorStateInfo anim = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

            if (anim.IsTag("Attack") && anim.normalizedTime >= 1)
            {
                ReturnToLocomotion();
            }
        }

        public override void Exit() 
        {
            stateMachine.Hitbox.onHit -= OnHit;
        }

        private void OnHit()
        {
            stateMachine.ForceHandler.ResetVerticalVelocity();
            stateMachine.ForceHandler.AddForce(0, attack.RecoilForce.y);
        }
    }
}
