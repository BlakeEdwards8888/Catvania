using Cat.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Stone
{
    public class StoneChaseState : StoneBaseState
    {
        readonly int WalkHash = Animator.StringToHash("Walk");

        Vector2 movingDirection;
        Transform playerTransform;
        Vector2 aggroRange;

        public StoneChaseState(StoneStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            stateMachine.Animator.Play(WalkHash);
            stateMachine.Health.onTakeDamage += EnterHitstun;
            aggroRange = new Vector2(stateMachine.HorizontalAggroRange, stateMachine.VerticalAggroRange);
        }

        public override void Tick(float deltaTime)
        {
            HandleFallSpeed();

            movingDirection = CalculateDirectionToPlayer(playerTransform.position);
            FaceDirection(movingDirection);

            Move(stateMachine.WalkSpeed, movingDirection);

            if (!IsPlayerInRange(playerTransform.position, aggroRange) || playerTransform.GetComponent<Health>().IsDead())
            {
                stateMachine.SwitchState(new StonePatrolState(stateMachine));
            }

            if (Mathf.Abs(stateMachine.transform.position.x - playerTransform.position.x) <= stateMachine.AttackRange)
            {
                stateMachine.SwitchState(new StoneAttackState(stateMachine));
            }
        }

        public override void Exit()
        {
            stateMachine.Health.onTakeDamage -= EnterHitstun;
        }
    }
}
