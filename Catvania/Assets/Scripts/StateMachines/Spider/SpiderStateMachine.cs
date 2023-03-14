using Cat.Movement;
using Cat.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Spider
{
    public class SpiderStateMachine : StateMachine
    {
        [field: SerializeField] public Mover Mover { get; private set; }
        [field: SerializeField] public ForceHandler ForceHandler { get; private set; }
        [field: SerializeField] public Rigidbody2D Rb2d { get; private set; }

        private void Start()
        {
            SwitchState(new SpiderFreeMoveState(this));
        }
    }
}
