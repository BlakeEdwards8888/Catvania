using Cat.Combat;
using Cat.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Crusader
{
    public class CrusaderStateMachine : StateMachine
    {
        enum AttackState
        {
            Flurry,
            DownwardStrike,
            SwordThrow
        }

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
        [field: SerializeField] public Transform LeftSwordThrowSpawnPoint { get; private set; }
        [field: SerializeField] public Transform RightSwordThrowSpawnPoint { get; private set; }
        [field: SerializeField] public float SwordThrowWaitTime { get; private set; }

        [SerializeField] AttackState[] attackPattern;

        int attackPatternIndex = 0;

        private void Start()
        {
            SwitchState(new CrusaderTeleportState(this));
        }

        public CrusaderBaseState GetNextAttackState()
        {
            CrusaderBaseState nextState = null;

            switch (attackPattern[attackPatternIndex])
            {
                case AttackState.Flurry:
                    nextState = new CrusaderFlurryState(this);
                    break;
                case AttackState.DownwardStrike:
                    nextState = new CrusaderDownwardStrikeState(this);
                    break;
                case AttackState.SwordThrow:
                    nextState = new CrusaderSwordThrowState(this);
                    break;                    
            }

            if(attackPatternIndex == attackPattern.Length - 1)
            {
                attackPatternIndex = 0;
            }
            else
            {
                attackPatternIndex++;
            }

            return nextState;
        }
    }
}
