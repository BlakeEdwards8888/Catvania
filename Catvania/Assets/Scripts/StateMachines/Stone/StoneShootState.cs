using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Stone
{
    public class StoneShootState : StoneBaseState
    {
        readonly int ShootHash = Animator.StringToHash("Shoot");

        Transform playerTransform;

        public StoneShootState(StoneStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            FaceDirection(CalculateDirectionToPlayer(playerTransform.position));
            stateMachine.Animator.Play(ShootHash);
            stateMachine.Health.onTakeDamage += EnterHitstun;
        }

        public override void Tick(float deltaTime)
        {
            HandleFallSpeed();

            Move(0, Vector2.zero);

            AnimatorStateInfo anim = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

            if (anim.IsTag("Attack") && anim.normalizedTime >= 1)
            {
                stateMachine.SwitchState(new StoneIdleState(stateMachine, stateMachine.ShootCooldown));
            }
        }

        public override void Exit()
        {
            stateMachine.Health.onTakeDamage -= EnterHitstun;
        }
    }
}
