using Cat.Combat;
using Cat.Movement;
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
        [field: SerializeField] public LayerMask GroundLayerMask { get; private set; }
        [field: SerializeField] public ContactFilter2D GroundContactFilter { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public float DownwardStrikeHeight { get; private set; }
        [field: SerializeField] public float DownwardStrikeSpeed { get; private set; }
        [field: SerializeField] public float DownwardStrikeCooldown { get; private set; }
        [field: SerializeField] public Mover Mover { get; private set; }
        

        private void Start()
        {
            SwitchState(new CrusaderTeleportState(this));
        }
    }
}
