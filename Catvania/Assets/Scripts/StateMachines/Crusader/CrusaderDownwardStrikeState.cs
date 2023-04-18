using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Crusader
{
    public class CrusaderDownwardStrikeState : CrusaderBaseState
    {
        readonly int DownwardStrikeHash = Animator.StringToHash("DownwardStrike-Loop");

        public CrusaderDownwardStrikeState(CrusaderStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter()
        {
            stateMachine.Animator.Play(DownwardStrikeHash);

            Transform playerTransform = GameObject.FindWithTag("Player").transform;

            stateMachine.transform.position = new Vector3(playerTransform.position.x,
                stateMachine.DownwardStrikeHeight, 0);
        }

        public override void Tick(float deltaTime)
        {
            Move(stateMachine.DownwardStrikeSpeed, Vector2.down);

            if (IsGrounded())
            {
                Move(0, Vector2.up);
                stateMachine.SwitchState(new CrusaderLandingState(stateMachine));
            }
        }

        public override void Exit() {}
    }
}
