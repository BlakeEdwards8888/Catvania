using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Stone
{
    public abstract class StoneBaseState : State
    {
        protected StoneStateMachine stateMachine;

        public StoneBaseState(StoneStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        protected void EnterHitstun(float duration)
        {
            stateMachine.SwitchState(new StoneHitstunState(stateMachine, duration));
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

        protected void Move(float speed, Vector2 direction)
        {
            stateMachine.Mover.Move(CalculateMovement(speed, direction) + stateMachine.ForceHandler.GetForce());
        }

        protected bool IsPlayerInRange(Vector3 playerPosition, Vector2 range)
        {
            Transform transform = stateMachine.transform;

            float minX = transform.position.x - range.x;
            float maxX = transform.position.x + range.x;
            float minY = transform.position.y - range.y;
            float maxY = transform.position.y + range.y;

            bool isXInRange = playerPosition.x > minX && playerPosition.x < maxX;
            bool isYInRange = playerPosition.y > minY && playerPosition.y < maxY;

            return isXInRange && isYInRange;
        }

        protected bool IsPlayerInRange(Vector3 playerPosition, float range)
        {
            return Vector2.Distance(playerPosition, stateMachine.transform.position) <= range;
        }

        protected Vector2 CalculateDirectionToPlayer(Vector3 playerPosition)
        {
            float movingX = playerPosition.x > stateMachine.transform.position.x ? 1 : -1;

            return new Vector2(movingX, 0);
        }

        protected void FaceDirection(Vector3 direction)
        {
            stateMachine.transform.localScale = new Vector3(direction.x, 1, 1);
        }

        private Vector2 CalculateMovement(float speed, Vector2 direction)
        {
            Vector2 gravity = new Vector2(0, stateMachine.Rb2d.velocity.y);
            return (direction * speed) + gravity;
        }
    }
}
