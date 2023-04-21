using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Crusader
{
    public class CrusaderSwordThrowState : CrusaderBaseState
    {
        readonly int SwordThrowHash = Animator.StringToHash("SwordThrow-Enter");

        float timeSinceThrown = 0;

        public CrusaderSwordThrowState(CrusaderStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter()
        {
            SpawnAtRandomSpawnPoint();
            FacePlayer();
            stateMachine.Animator.Play(SwordThrowHash);
        }

        public override void Tick(float deltaTime)
        {
            timeSinceThrown += deltaTime;

            if(timeSinceThrown >= stateMachine.SwordThrowWaitTime)
            {
                stateMachine.SwitchState(new CrusaderTeleportState(stateMachine));
            }
        }

        public override void Exit() {}

        private void SpawnAtRandomSpawnPoint()
        {
            float randomNum = UnityEngine.Random.value;

            stateMachine.transform.position = randomNum > 0.5f ? 
                stateMachine.LeftSwordThrowSpawnPoint.position : 
                stateMachine.RightSwordThrowSpawnPoint.position;
        }
    }
}
