using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Crusader
{
    public class CrusaderLandingState : CrusaderBaseState
    {
        readonly int LandingHash = Animator.StringToHash("DownwardStrike-Landing");

        float timeSinceLanded;

        public CrusaderLandingState(CrusaderStateMachine stateMachine) : base(stateMachine){}

        public override void Enter()
        {
            //snap to the ground
            Vector3 tempPos = stateMachine.transform.position;
            tempPos.y = 0;
            stateMachine.transform.position = tempPos;


            stateMachine.Animator.Play(LandingHash);
        }

        public override void Tick(float deltaTime)
        {
            timeSinceLanded += deltaTime;

            if(timeSinceLanded >= stateMachine.DownwardStrikeCooldown)
            {
                stateMachine.SwitchState(new CrusaderTeleportState(stateMachine, stateMachine.GetTeleportationDuration()));
            }
        }

        public override void Exit() {}
    }
}
