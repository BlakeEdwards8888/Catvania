using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Skelecat
{
    public class SkelecatAttackState : SkelecatBaseState
    {
        readonly int AttackHash = Animator.StringToHash("Attack");

        float attackSoundDelay = 0.42f;
        float timeSinceEntered = 0;
        bool playedSound;

        public SkelecatAttackState(SkelecatStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.Animator.Play(AttackHash);
        }

        public override void Tick(float deltaTime)
        {
            HandleFallSpeed();

            Move(0, Vector2.zero);

            if(timeSinceEntered >= attackSoundDelay && !playedSound)
            {
                stateMachine.PlaySound("Attack");
                playedSound = true;
            }
            else
            {
                timeSinceEntered += deltaTime;
            }

            AnimatorStateInfo anim = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

            if (anim.IsTag("Attack") && anim.normalizedTime >= 1)
            {
                ReturnToLocomotion();
            }
        }

        public override void Exit()
        {

        }
    }
}
