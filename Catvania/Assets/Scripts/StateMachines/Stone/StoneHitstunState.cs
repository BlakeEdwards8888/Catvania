using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Stone
{
    public class StoneHitstunState : StoneBaseState
    {
        readonly int HitstunHash = Animator.StringToHash("Hitstun");

        float timeStunned;
        float stunDuration = 0.2f;

        public StoneHitstunState(StoneStateMachine stateMachine, float stunDuration) : base(stateMachine)
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
                stateMachine.SwitchState(new StonePatrolState(stateMachine));
            }
        }

        public override void Exit()
        {

        }
    }
}
