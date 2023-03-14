using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Player
{
    public class PlayerJumpState : PlayerBaseState
    {
        readonly int JumpHash = Animator.StringToHash("Jump");
        bool isCancellable;

        public PlayerJumpState(PlayerStateMachine stateMachine, bool isCancellable = false) : base(stateMachine) 
        {
            this.isCancellable = isCancellable;
        }

        public override void Enter()
        {
            stateMachine.InputReader.jumpCancelEvent += CancelJump;
            stateMachine.InputReader.attackEvent += OnAttack;
            stateMachine.InputReader.jumpEvent += TryDoubleJump;
            stateMachine.InputReader.specialEvent += OnSpecial;

            stateMachine.Animator.Play(JumpHash);
        }

        public override void Tick(float deltaTime)
        {
            Move(stateMachine.FreeMoveSpeed);
            FaceMovementDirection();

            if (stateMachine.Rb2d.velocity.y < 0)
            {
                stateMachine.SwitchState(new PlayerFallState(stateMachine));
            }
        }

        public override void Exit() 
        {
            stateMachine.InputReader.jumpCancelEvent -= CancelJump;
            stateMachine.InputReader.attackEvent -= OnAttack;
            stateMachine.InputReader.jumpEvent -= TryDoubleJump;
            stateMachine.InputReader.specialEvent -= OnSpecial;
        }

        private void CancelJump()
        {
            if (isCancellable)
            {
                Vector2 velocity = stateMachine.Rb2d.velocity;
                velocity.y /= 2;
                stateMachine.Rb2d.velocity = velocity;
            }
        }
    }
}
