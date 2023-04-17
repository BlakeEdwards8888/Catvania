using Cat.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Crusader
{
    public class CrusaderStateMachine : StateMachine
    {
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField] public Hitbox BodyHitbox { get; private set; }
        [field: SerializeField] public Collider2D BodyCollider { get; private set; }
        [field: SerializeField] public float TeleportationDuration { get; private set; }
        [field: SerializeField] public Rigidbody2D Rb2d { get; private set; }
        [field: SerializeField] public LayerMask GroundFilter { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }

        private void Start()
        {
            SwitchState(new CrusaderTeleportState(this));
        }
    }
}
