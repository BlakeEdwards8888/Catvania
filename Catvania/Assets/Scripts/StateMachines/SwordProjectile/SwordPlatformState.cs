using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Sword
{
    public class SwordPlatformState : SwordBaseState
    {
        public SwordPlatformState(SwordStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter()
        {
            stateMachine.Rb2d.velocity = Vector2.zero;
            stateMachine.Hitbox.enabled = false;
            stateMachine.Platform.SetActive(true);
            stateMachine.HitboxCollider.enabled = false;
            stateMachine.Rb2d.bodyType = RigidbodyType2D.Kinematic;
            stateMachine.HitEffect.Play();
            stateMachine.transform.position += CalculateOffset();
            stateMachine.InputReader.specialEvent += ReturnToPlayer;
        }

        public override void Tick(float deltaTime)
        {

        }

        public override void Exit()
        {
            stateMachine.Hitbox.enabled = true;
            stateMachine.Platform.SetActive(false);
            stateMachine.HitboxCollider.enabled = true;
            stateMachine.Rb2d.bodyType = RigidbodyType2D.Dynamic;
            stateMachine.InputReader.specialEvent -= ReturnToPlayer;
        }

        private Vector3 CalculateOffset()
        {
            return new Vector3(0.1f * stateMachine.transform.localScale.x, 0, 0);
        }

        private void ReturnToPlayer()
        {
            stateMachine.SwitchState(new SwordReturnState(stateMachine));
        }
    }
}
