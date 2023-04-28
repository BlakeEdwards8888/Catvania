using System;
using UnityEngine;

namespace Cat.StateMachines.Crusader
{
    public class CrusaderTeleportState : CrusaderBaseState
    {
        readonly int TeleportOutHash = Animator.StringToHash("TeleportOut");

        float duration = 0.5f;
        float timeSinceTeleported = 0;

        public CrusaderTeleportState(CrusaderStateMachine stateMachine, float duration) : base(stateMachine) 
        {
            this.duration = duration;
        }

        public override void Enter() 
        {
            stateMachine.IterateAttackPattern();
            stateMachine.Animator.Play(TeleportOutHash);
            stateMachine.BodyCollider.enabled = false;
        }

        public override void Tick(float deltaTime)
        {
            timeSinceTeleported += deltaTime;

            if(timeSinceTeleported >= duration)
            {
                InitiateNestState();
            }
        }

        public override void Exit() 
        {
            stateMachine.BodyCollider.enabled = true;
        }

        private void InitiateNestState()
        {
            stateMachine.SwitchState(stateMachine.GetNextAttackState());
        }
    }
}
