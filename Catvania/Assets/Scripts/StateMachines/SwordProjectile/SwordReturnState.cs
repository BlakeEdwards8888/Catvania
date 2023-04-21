using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Sword
{
    public class SwordReturnState : SwordBaseState
    {
        readonly int ProjectileHash = Animator.StringToHash("Projectile");

        private const float StoppingDistance = 0.1f;
        private  Vector3 PlayerPositionOffset = new Vector3(0,0.3f,0);

        Transform playerTransform;

        public SwordReturnState(SwordStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter() 
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            stateMachine.Animator.Play(ProjectileHash);
        }

        public override void Tick(float deltaTime)
        {
            stateMachine.Rb2d.velocity = CalculateDirectionToPlayer() * stateMachine.ReturnMoveSpeed;

            if(Vector2.Distance(playerTransform.position + PlayerPositionOffset, stateMachine.transform.position) <= StoppingDistance)
            {
                stateMachine.ReturnToPlayer();
            }
        }

        private Vector2 CalculateDirectionToPlayer()
        {
            return (playerTransform.position + PlayerPositionOffset - stateMachine.transform.position).normalized;
        }

        public override void Exit() {}
    }
}
