using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Bat
{
    public class BatSleepState : BatBaseState
    {
        Transform playerTransform;

        public BatSleepState(BatStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        public override void Tick(float deltaTime)
        {
            if(Vector2.Distance(stateMachine.transform.position, playerTransform.position) <= stateMachine.AggroRange)
            {
                stateMachine.SwitchState(new BatChaseState(stateMachine, true));
            }
        }

        public override void Exit()
        {
            
        }
    }
}
