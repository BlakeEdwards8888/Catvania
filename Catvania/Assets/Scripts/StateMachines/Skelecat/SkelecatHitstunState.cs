using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Skelecat
{
    public class SkelecatHitstunState : SkelecatBaseState
    {
        readonly int HitstunHash = Animator.StringToHash("Hitstun");

        float timeStunned;
        float stunDuration = 0.2f;

        public SkelecatHitstunState(SkelecatStateMachine stateMachine, float stunDuration) : base(stateMachine) 
        {
            this.stunDuration = stunDuration;
        }

        public override void Enter()
        {
            stateMachine.Animator.Play(HitstunHash);
            stateMachine.PlaySound("Hurt");
        }

        public override void Tick(float deltaTime)
        {
            Move(0, Vector2.zero);

            HandleFallSpeed();

            timeStunned += deltaTime;

            if (timeStunned >= stunDuration)
            {
                ReturnToLocomotion();
            }
        }

        public override void Exit()
        {
            
        }
    }
}
