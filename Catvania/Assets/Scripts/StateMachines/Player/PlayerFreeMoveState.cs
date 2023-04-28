using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Player
{
    public class PlayerFreeMoveState : PlayerBaseState
    {
        readonly int WalkingBoolHash = Animator.StringToHash("IsWalking");
        readonly int IdleHash = Animator.StringToHash("Idle");

        public PlayerFreeMoveState(PlayerStateMachine stateMachine) : base(stateMachine){}

        public override void Enter() 
        {
            stateMachine.InputReader.jumpEvent += OnJump;
            stateMachine.InputReader.attackEvent += OnAttack;
            stateMachine.InputReader.specialEvent += OnSpecial;
            stateMachine.InputReader.healEvent += OnHeal;

            stateMachine.Animator.Play(IdleHash);
        }

        public override void Tick(float deltaTime)
        {
            Move(stateMachine.FreeMoveSpeed);
            FaceMovementDirection();

            stateMachine.Animator.SetBool(WalkingBoolHash,
                Mathf.Abs(stateMachine.InputReader.MovementValue.x) > InputValueBuffer);

            

            if (stateMachine.Rb2d.velocity.y < 0)
            {
                stateMachine.SwitchState(new PlayerFallState(stateMachine));
            }
        }

        public override void Exit()
        {
            stateMachine.InputReader.jumpEvent -= OnJump;
            stateMachine.InputReader.attackEvent -= OnAttack;
            stateMachine.InputReader.specialEvent -= OnSpecial;
            stateMachine.InputReader.healEvent += OnHeal;
        }

        private void OnJump()
        {
            if (IsGrounded())
            {
                stateMachine.ForceHandler.ResetVerticalVelocity();
                stateMachine.ForceHandler.AddForce(0, stateMachine.JumpForce);
                stateMachine.SwitchState(new PlayerJumpState(stateMachine, true));
            }
        }
        
    }
}
