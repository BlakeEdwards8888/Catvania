using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Player
{
    public abstract class PlayerBaseState : State
    {
        private const float GroundCheckDistance = 0.1f;
        protected const float InputValueBuffer = 0.2f;

        protected PlayerStateMachine stateMachine;


        public PlayerBaseState(PlayerStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        protected bool IsGrounded()
        {
            RaycastHit2D[] results = new RaycastHit2D[1];

            if(stateMachine.Rb2d.Cast(-Vector2.up, stateMachine.GroundFilter, results, GroundCheckDistance) > 0)
            {
                stateMachine.SetCanDoubleJump(true);
                stateMachine.SetCanUpstrike(true);
                return true;
            }

            return false;
        }

        protected void Move(float speed)
        {
            stateMachine.Mover.Move(CalculateMovement(speed, CalculateMoveDirection()) + stateMachine.ForceHandler.GetForce());
        }

        protected void Move(float speed, Vector2 direction)
        {
            stateMachine.Mover.Move(CalculateMovement(speed, direction) + stateMachine.ForceHandler.GetForce());
        }

        protected void ReturnToLocomotion()
        {
            if (IsGrounded())
            {
                stateMachine.SwitchState(new PlayerFreeMoveState(stateMachine));
            }
            else if(stateMachine.Rb2d.velocity.y > 0)
            {
                stateMachine.SwitchState(new PlayerJumpState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFallState(stateMachine));
            }
        }

        private Vector2 CalculateMovement(float speed, Vector2 direction)
        {
            Vector2 gravity = new Vector2(0, stateMachine.Rb2d.velocity.y);
            return (direction * speed) + gravity;
        }

        private Vector2 CalculateMoveDirection()
        {
            float x = 0;

            if (stateMachine.InputReader.MovementValue.x > InputValueBuffer)
            {
                x = 1;
            }
            else if (stateMachine.InputReader.MovementValue.x < -InputValueBuffer)
            {
                x = -1;
            }

            return new Vector2(x, 0);
        }

        protected void FaceMovementDirection()
        {
            Vector2 movementDirection = CalculateMoveDirection();

            if(movementDirection.x < 0)
            {
                stateMachine.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(movementDirection.x > 0)
            {
                stateMachine.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        protected void HandleFallSpeed()
        {
            if (stateMachine.Rb2d.velocity.y < 0)
            {
                stateMachine.Rb2d.gravityScale = stateMachine.FallingGravityScale;
            }
            else
            {
                stateMachine.Rb2d.gravityScale = 1;
            }
        }

        protected void OnAttack()
        {
            if (!stateMachine.CanAttack()) return;

            if(!IsGrounded() && stateMachine.InputReader.MovementValue.y < -InputValueBuffer)
            {
                stateMachine.SwitchState(new PlayerAttackDState(stateMachine));
            }
            else if (stateMachine.InputReader.MovementValue.y > InputValueBuffer)
            {
                stateMachine.SwitchState(new PlayerAttackUState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerAttackFState(stateMachine));
            }
        }

        protected void OnSpecial()
        {
            if (!stateMachine.CanAttack()) return;

            if (stateMachine.InputReader.MovementValue.y < -InputValueBuffer
                && stateMachine.FlagSystem.CheckFlag("HAS_DASH"))
            {
                stateMachine.SwitchState(new PlayerDashState(stateMachine));
            }
            else if(stateMachine.CanUpstrike() && stateMachine.InputReader.MovementValue.y > InputValueBuffer
                && stateMachine.FlagSystem.CheckFlag("HAS_UPSTRIKE"))
            {
                stateMachine.SwitchState(new PlayerUpstrikeState(stateMachine));
            }
            else if(stateMachine.FlagSystem.CheckFlag("HAS_SWORD_THROW"))
            {
                stateMachine.SwitchState(new PlayerSwordThrowState(stateMachine));
            }
        }

        protected void OnHeal()
        {
            if (!stateMachine.Healer.CanHeal()) return;

            stateMachine.SwitchState(new PlayerHealState(stateMachine));
        }

        protected void TryDoubleJump()
        {
            if (stateMachine.CanDoubleJump())
            {
                stateMachine.ForceHandler.ResetVerticalVelocity();
                stateMachine.ForceHandler.AddForce(0, stateMachine.JumpForce);
                stateMachine.SetCanDoubleJump(false);
                stateMachine.SwitchState(new PlayerJumpState(stateMachine, true));
            }
        }
    }
}
