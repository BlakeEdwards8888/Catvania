using Cat.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Player
{
    public class PlayerUpstrikeState : PlayerBaseState
    {
        readonly int UpstrikeHash = Animator.StringToHash("Upstrike");

        Attack attack;

        public PlayerUpstrikeState(PlayerStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter()
        {
            stateMachine.SetCanUpstrike(false);
            attack = stateMachine.GetAttack("Upstrike");
            stateMachine.Hitbox.Setup(attack);
            stateMachine.ForceHandler.ResetVerticalVelocity();
            stateMachine.ForceHandler.AddForce(3 * stateMachine.transform.localScale.x, 6);
            stateMachine.Animator.Play(UpstrikeHash);
        }

        public override void Tick(float deltaTime)
        {
            Move(0);

            if(stateMachine.Rb2d.velocity.y <= 0)
            {
                ReturnToLocomotion();
            }
        }

        public override void Exit()
        {
            
        }
    }
}
