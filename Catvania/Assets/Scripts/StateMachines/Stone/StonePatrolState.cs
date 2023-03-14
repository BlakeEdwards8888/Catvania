using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Stone
{
    public class StonePatrolState : StoneBaseState
    {
        private const float GroundCheckDistance = 0.1f;

        readonly int WalkHash = Animator.StringToHash("Walk");

        Vector2 movingDirection;

        private Vector3 originPositionOffset = new Vector3(0.5f, 0, 0);

        Transform playerTransform;

        public StonePatrolState(StoneStateMachine stateMachine) : base(stateMachine){}

        public override void Enter()
        {
            stateMachine.Animator.Play(WalkHash);
            stateMachine.Health.onTakeDamage += EnterHitstun;
            movingDirection = new Vector2(stateMachine.transform.localScale.x, 0);
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        public override void Tick(float deltaTime)
        {
            HandleFallSpeed();

            Move(stateMachine.WalkSpeed, movingDirection);
            FaceMovementDirection();

            if (CollidedWithGroundInFront() || !IsGroundInFront())
            {
                Flip();
            }

            Vector2 directionToPlayer = (stateMachine.transform.position - playerTransform.position).normalized;

            if (Vector2.Distance(stateMachine.transform.position + originPositionOffset, playerTransform.position) <= stateMachine.AggroRange)
            {
                stateMachine.SwitchState(new StoneChaseState(stateMachine));
            }
        }

        public override void Exit()
        {
            stateMachine.Health.onTakeDamage -= EnterHitstun;
        }

        private bool CollidedWithGroundInFront()
        {
            RaycastHit2D[] results = new RaycastHit2D[1];
            return stateMachine.Rb2d.Cast(Vector2.right * stateMachine.transform.lossyScale,
                stateMachine.GroundFilter, results, GroundCheckDistance) > 0;
        }

        private bool IsGroundInFront()
        {
            return (Physics2D.Raycast((stateMachine.transform.position + (originPositionOffset * movingDirection.x)),
                Vector2.down, GroundCheckDistance, stateMachine.GroundFilter.layerMask));
        }

        void Flip()
        {
            movingDirection *= -1;
        }

        void FaceMovementDirection()
        {
            stateMachine.transform.localScale = new Vector3(movingDirection.x, 1, 1);
        }
    }
}
