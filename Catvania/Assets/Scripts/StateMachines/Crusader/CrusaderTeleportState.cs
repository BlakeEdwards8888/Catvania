using System;
using UnityEngine;

namespace Cat.StateMachines.Crusader
{
    public class CrusaderTeleportState : CrusaderBaseState
    {
        readonly int TeleportOutHash = Animator.StringToHash("TeleportOut");

        float timeSinceTeleported = 0;

        public CrusaderTeleportState(CrusaderStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter() 
        {
            stateMachine.Animator.Play(TeleportOutHash);
            stateMachine.BodyHitbox.gameObject.SetActive(false);
            stateMachine.BodyCollider.enabled = false;
        }

        public override void Tick(float deltaTime)
        {
            timeSinceTeleported += deltaTime;

            if(timeSinceTeleported >= stateMachine.TeleportationDuration)
            {
                InitiateNestState();
            }
        }

        public override void Exit() 
        {
            stateMachine.BodyHitbox.gameObject.SetActive(true);
            stateMachine.BodyCollider.enabled = true;
        }

        private void InitiateNestState()
        {
            stateMachine.SwitchState(stateMachine.GetNextAttackState());
        }
    }
}
