using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Bat
{
    public class BatChaseState : BatBaseState
    {
        private const float DirectionChangeLatency = 0.5f;
        readonly int WakeUpHash = Animator.StringToHash("WakeUp");
        readonly int FlyHash = Animator.StringToHash("Fly");

        bool shouldWakeUp;

        Transform playerTransform;

        Vector3 groundOffset = new Vector3(0, 0.5f, 0);
        Vector2 movementDirection;

        Vector2 referenceVelocity;

        Vector2 directionToPlayer;

        public BatChaseState(BatStateMachine stateMachine, bool shouldWakeUp) : base(stateMachine)
        {
            this.shouldWakeUp = shouldWakeUp;
        }

        public override void Enter()
        {
            stateMachine.Animator.Play(shouldWakeUp ? WakeUpHash : FlyHash);
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        public override void Tick(float deltaTime)
        {
            Vector2 directionToPlayer  = ((playerTransform.position + groundOffset) - stateMachine.transform.position).normalized;

            movementDirection = Vector2.SmoothDamp(movementDirection, directionToPlayer, ref referenceVelocity,
                DirectionChangeLatency);

            FaceMovementDirection();

            Move(stateMachine.ChaseSpeed, movementDirection);
        }

        public override void Exit()
        {
            
        }

        void FaceMovementDirection()
        {
            float movingX = movementDirection.x > 0 ? 1 : -1;

            stateMachine.transform.localScale = new Vector3(movingX, 1, 1);
        }
    }
}
