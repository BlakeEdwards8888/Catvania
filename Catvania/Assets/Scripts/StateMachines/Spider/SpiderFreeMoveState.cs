using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Spider {
    public class SpiderFreeMoveState : SpiderBaseState
    {
        public SpiderFreeMoveState(SpiderStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter() {}

        public override void Tick(float deltaTime) 
        {
            Move(0);
        }

        public override void Exit() {}
    }
}
