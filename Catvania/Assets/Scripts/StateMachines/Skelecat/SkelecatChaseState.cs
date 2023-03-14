using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Skelecat
{
    public class SkelecatChaseState : SkelecatBaseState
    {
        readonly int RunHash = Animator.StringToHash("Run");

        Vector2 movingDirection;
        Transform playerTransform;

        public SkelecatChaseState(SkelecatStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            stateMachine.Animator.Play(RunHash);
            stateMachine.Health.onTakeDamage += EnterHitstun;
        }

        public override void Tick(float deltaTime)
        {
            HandleFallSpeed();

            CalculateMovingDirection();
            FaceMovementDirection();

            Move(stateMachine.RunSpeed, movingDirection);

            if (Vector2.Distance(stateMachine.transform.position, playerTransform.position) > stateMachine.AggroRange)
            {
                stateMachine.SwitchState(new SkelecatPatrolState(stateMachine));
            }

            if (Mathf.Abs(stateMachine.transform.position.x - playerTransform.position.x) <= stateMachine.AttackRange)
            {
                stateMachine.SwitchState(new SkelecatAttackState(stateMachine));
            }
        }

        public override void Exit()
        {
            stateMachine.Health.onTakeDamage -= EnterHitstun;
        }

        private void CalculateMovingDirection()
        {
            float movingX = playerTransform.position.x > stateMachine.transform.position.x ? 1 : -1;

            movingDirection = new Vector2(movingX, 0);
        }

        void FaceMovementDirection()
        {
            stateMachine.transform.localScale = new Vector3(movingDirection.x, 1, 1);
        }
    }
}
