using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Bat
{
    public class BatHitstunState : BatBaseState
    {
        readonly int HitstunHash = Animator.StringToHash("Hitstun");

        float timeStunned;
        float stunDuration = 0.2f;

        public BatHitstunState(BatStateMachine stateMachine, float stunDuration) : base(stateMachine)
        {
            this.stunDuration = stunDuration;
        }

        public override void Enter()
        {
            stateMachine.Animator.Play(HitstunHash);
        }

        public override void Tick(float deltaTime)
        {
            Move(0, Vector2.zero);

            timeStunned += deltaTime;

            if (timeStunned >= stunDuration)
            {
                stateMachine.SwitchState(new BatChaseState(stateMachine, false));
            }
        }

        public override void Exit()
        {
            
        }
    }
}
