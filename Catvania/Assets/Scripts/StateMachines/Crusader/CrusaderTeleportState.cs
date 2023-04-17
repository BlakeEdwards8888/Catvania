using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Crusader
{
    public class CrusaderTeleportState : CrusaderBaseState
    {
        readonly int IdleHash = Animator.StringToHash("Idle");

        float timeSinceTeleported = 0;

        public CrusaderTeleportState(CrusaderStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter() 
        {
            stateMachine.Animator.Play(IdleHash);
            stateMachine.SpriteRenderer.enabled = false;
            stateMachine.BodyHitbox.gameObject.SetActive(false);
            stateMachine.BodyCollider.enabled = false;
        }

        public override void Tick(float deltaTime)
        {
            timeSinceTeleported += deltaTime;

            if(timeSinceTeleported >= stateMachine.TeleportationDuration)
            {
                InitiateNextState();
            }
        }

        public override void Exit() 
        {
            stateMachine.SpriteRenderer.enabled = true;
            stateMachine.BodyHitbox.gameObject.SetActive(true);
            stateMachine.BodyCollider.enabled = true;
        }

        private void InitiateNextState()
        {
            stateMachine.SwitchState(new CrusaderFlurryState(stateMachine));
        }
    }
}
