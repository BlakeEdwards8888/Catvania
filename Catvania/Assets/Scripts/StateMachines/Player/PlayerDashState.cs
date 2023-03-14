using Cat.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace Cat.StateMachines.Player
{
    public class PlayerDashState : PlayerBaseState
    {
        readonly int DashHash = Animator.StringToHash("Dash");
        private const float GroundCheckDistance = 0.1f;
        private const float StunDuration = 0.4f;

        Vector2 movementDirection;
        Attack attack;

        public PlayerDashState(PlayerStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter()
        {
            stateMachine.InputReader.jumpEvent += OnJump;
            stateMachine.Hitbox.onHit += OnCollision;

            stateMachine.Animator.Play(DashHash);
            attack = stateMachine.GetAttack("DashAttack");
            stateMachine.Hitbox.Setup(attack);
            movementDirection = new Vector2(stateMachine.transform.localScale.x, 0);
            stateMachine.Rb2d.gravityScale = stateMachine.DashGravityScale;
            stateMachine.DashTrail.enabled = true;
            stateMachine.DashParticles.Play();
        }

        public override void Tick(float deltaTime)
        {
            Move(stateMachine.DashSpeed, movementDirection);

            stateMachine.DashParticles.enableEmission = IsGrounded();

            if (CollidedWithGroundInFront())
            {
                OnCollision();
            }
        }

        public override void Exit()
        {
            stateMachine.InputReader.jumpEvent -= OnJump;
            stateMachine.Hitbox.onHit -= OnCollision;

            stateMachine.Rb2d.gravityScale = 1;
            stateMachine.DashParticles.Stop();
            stateMachine.DashTrail.enabled = false;
        }

        private bool CollidedWithGroundInFront()
        {
            RaycastHit2D[] results = new RaycastHit2D[1];
            return stateMachine.Rb2d.Cast(Vector2.right * stateMachine.transform.lossyScale,
                stateMachine.GroundFilter, results, GroundCheckDistance) > 0;
        }

        private void OnCollision()
        {
            stateMachine.ForceHandler.AddForce(attack.RecoilForce * stateMachine.transform.localScale);
            stateMachine.SwitchState(new PlayerHitstunState(stateMachine, StunDuration));
        }

        private void OnJump()
        {
            if (IsGrounded() || stateMachine.CanDoubleJump())
            {
                stateMachine.ForceHandler.ResetVerticalVelocity();
                stateMachine.ForceHandler.AddForce(stateMachine.Rb2d.velocity.x - (stateMachine.FreeMoveSpeed * movementDirection.x), stateMachine.JumpForce);

                if(!IsGrounded()) stateMachine.SetCanDoubleJump(false);

                stateMachine.SwitchState(new PlayerJumpState(stateMachine, true));
            }
        }
    }
}
