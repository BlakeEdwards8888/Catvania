using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Player
{
    public class PlayerSwordThrowState : PlayerBaseState
    {
        readonly int SwordThrowHash = Animator.StringToHash("SwordThrow");
        public PlayerSwordThrowState(PlayerStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter()
        {
            stateMachine.Animator.Play(SwordThrowHash);
            stateMachine.SetCanAttack(false);
        }

        public override void Tick(float deltaTime)
        {
            Move(0);

            HandleFallSpeed();

            AnimatorStateInfo anim = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

            if (anim.IsTag("Special") && anim.normalizedTime >= 1)
            {
                ReturnToLocomotion();
            }
        }

        public override void Exit() {}
    }
}
