using Cat.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Player
{
    public class PlayerAttackFState: PlayerBaseState
    {
        readonly int AttackHash = Animator.StringToHash("AttackF");

        Attack attack;

        public PlayerAttackFState(PlayerStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter()
        {
            stateMachine.Hitbox.onHit += OnHit;

            attack = stateMachine.GetAttack("AttackF");

            stateMachine.Hitbox.Setup(attack);
            stateMachine.Animator.Play(AttackHash);
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

        public override void Exit() 
        {
            stateMachine.Hitbox.onHit -= OnHit;
        }

        private void OnHit()
        {
            Vector2 directionToCollision = stateMachine.Hitbox.GetDirectionToCollision();

            Vector2 recoilForce = attack.RecoilForce * directionToCollision;

            stateMachine.ForceHandler.AddForce(recoilForce);
        }
    }
}
