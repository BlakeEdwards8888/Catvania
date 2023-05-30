using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Player
{
    public class PlayerHitstunState : PlayerBaseState
    {
        readonly int HitstunHash = Animator.StringToHash("Hitstun");

        float timeStunned;
        float stunDuration = 0.2f;

        public PlayerHitstunState(PlayerStateMachine stateMachine, float stunDuration) : base(stateMachine)
        {
            this.stunDuration = stunDuration;
        }

        public override void Enter()
        {
            stateMachine.Animator.Play(HitstunHash);
            stateMachine.Health.SetIsInvulnerable(true);
            stateMachine.PlaySound("Hurt");
        }

        public override void Tick(float deltaTime)
        {
            Move(0);

            HandleFallSpeed();

            timeStunned += deltaTime;

            if(timeStunned >= stunDuration)
            {
                ReturnToLocomotion();
            }
        }

        public override void Exit() 
        {
            stateMachine.StartCoroutine(stateMachine.InvulnerabilityCoroutine());
        }
    }
}
