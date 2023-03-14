using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Player
{
    public class PlayerFallState : PlayerBaseState
    {
        readonly int FallHash = Animator.StringToHash("Fall");

        public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter()
        {
            stateMachine.InputReader.attackEvent += OnAttack;
            stateMachine.InputReader.jumpEvent += TryDoubleJump;
            stateMachine.InputReader.specialEvent += OnSpecial;

            stateMachine.Animator.Play(FallHash);
            stateMachine.Rb2d.gravityScale = stateMachine.FallingGravityScale;
        }

        public override void Tick(float deltaTime)
        {
            Move(stateMachine.FreeMoveSpeed);
            FaceMovementDirection();

            if (IsGrounded())
            {
                stateMachine.SwitchState(new PlayerFreeMoveState(stateMachine));
            }
        }

        public override void Exit()
        {
            stateMachine.InputReader.attackEvent -= OnAttack;
            stateMachine.InputReader.jumpEvent -= TryDoubleJump;
            stateMachine.InputReader.specialEvent -= OnSpecial;

            stateMachine.Rb2d.gravityScale = 1;
        }
    }
}
