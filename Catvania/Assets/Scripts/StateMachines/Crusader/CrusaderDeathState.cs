using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Crusader
{
    public class CrusaderDeathState : CrusaderBaseState
    {
        readonly int DeathLoopHash = Animator.StringToHash("Death-Loop");
        readonly int DeathEndHash = Animator.StringToHash("Death-End");

        bool landed;
        float groundCheckDelay = 0.1f;

        float timeSinceDied;

        public CrusaderDeathState(CrusaderStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter()
        {
            Animator cameraAnimator = GameObject.FindGameObjectWithTag("StateDrivenCamera").GetComponent<Animator>();

            stateMachine.Animator.Play(DeathLoopHash);
            stateMachine.Health.SetIsInvulnerable(true);
            stateMachine.SpriteFlasher.Flash(0.15f, 0.1f);
            stateMachine.PlaySound("Death");
            stateMachine.TimeManipulator.StartManipulatingTime(0, 2f, 
                () => { cameraAnimator.Play("MainState"); });

            cameraAnimator.Play("BossDeathState");
        }


        public override void Tick(float deltaTime)
        {
            Move();

            timeSinceDied += deltaTime;

            if (timeSinceDied >= groundCheckDelay && IsGrounded() && !landed)
            {
                stateMachine.Animator.Play(DeathEndHash);
                landed = true;
            }
        }

        public override void Exit() {}
    }
}
