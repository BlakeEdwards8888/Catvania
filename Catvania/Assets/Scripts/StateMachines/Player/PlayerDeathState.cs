using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Player
{
    public class PlayerDeathState : PlayerBaseState
    {
        readonly int DeathHash = Animator.StringToHash("Death");

        public PlayerDeathState(PlayerStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter() 
        {
            stateMachine.Animator.Play(DeathHash);
            stateMachine.PlaySound("Death");
        }

        public override void Tick(float deltaTime)
        {
            Move(0);
        }

        public override void Exit() {}
    }
}
