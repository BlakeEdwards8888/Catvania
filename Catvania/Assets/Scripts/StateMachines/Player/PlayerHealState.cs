using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Player
{
    public class PlayerHealState : PlayerBaseState
    {
        readonly int HealHash = Animator.StringToHash("Heal");

        public PlayerHealState(PlayerStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter() 
        {
            stateMachine.Animator.Play(HealHash);
        }

        public override void Tick(float deltaTime)
        {
            Move(0);

            HandleFallSpeed();

            AnimatorStateInfo anim = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

            if (anim.IsTag("Heal") && anim.normalizedTime >= 1)
            {
                ReturnToLocomotion();
            }
        }

        public override void Exit() {}
    }
}
