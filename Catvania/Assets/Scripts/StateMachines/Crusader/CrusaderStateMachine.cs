using Cat.Combat;
using Cat.Effects;
using Cat.Movement;
using Cat.Physics;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Crusader
{
    public class CrusaderStateMachine : StateMachine
    {
        public enum AttackState
        {
            Flurry,
            DownwardStrike,
            SwordThrow
        }

        [System.Serializable]
        public struct AttackCycle
        {
            public AttackState attackState;
            public float teleportationDuration;
        }

        [System.Serializable]
        public struct AttackMapping
        {
            public string attackName;
            public Attack attack;
        }

        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField] public Hitbox BodyHitbox { get; private set; }
        [field: SerializeField] public Collider2D BodyCollider { get; private set; }
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
        [field: SerializeField] public Health Health { get; private set; }
        [field: SerializeField] public ForceHandler ForceHandler { get; private set; }
        [field: SerializeField] public SpriteFlasher SpriteFlasher { get; private set; }
        [field: SerializeField] public TimeManipulator TimeManipulator { get; private set; }
        [field: SerializeField] public Hitbox SwordHitbox { get; private set; }
        [field: SerializeField] public AttackMapping[] Attacks { get; private set; }

        [Range(0,100)]
        [SerializeField] float attackPhaseSwitchHealthThresholdPercentage = 75;
        [SerializeField] AttackCycle[] earlyAttackPattern;
        [SerializeField] AttackCycle[] lateAttackPattern;
        
        [SerializeField] float initialTeleportationDuration;

        int attackPatternIndex = 0;
        AttackCycle[] currentAttackPattern;
        Dictionary<string, Attack> attackLookup = null;

        private void OnEnable()
        {
            Health.onDeath += Health_OnDeath;
            Health.onTakeDamage += Health_OnTakeDamage;
        }

        private void Start()
        {
            currentAttackPattern = earlyAttackPattern;
            SwitchState(new CrusaderTeleportState(this, initialTeleportationDuration));
        }

        public CrusaderBaseState GetNextAttackState()
        {
            CrusaderBaseState nextState = null;

            switch (currentAttackPattern[attackPatternIndex].attackState)
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

            return nextState;
        }

        public void IterateAttackPattern()
        {
            if (attackPatternIndex == currentAttackPattern.Length - 1)
            {
                attackPatternIndex = 0;
            }
            else
            {
                attackPatternIndex++;
            }
        }

        public float GetTeleportationDuration()
        {
            return currentAttackPattern[attackPatternIndex].teleportationDuration;
        }

        private void Health_OnDeath()
        {
            SwitchState(new CrusaderDeathState(this));
        }

        private void Health_OnTakeDamage(float hitstunDuration)
        {
            if(Health.GetCurrentHealth() <= CalculatePhaseSwitchHealthThreshold() &&
                currentAttackPattern == earlyAttackPattern)
            {
                currentAttackPattern = lateAttackPattern;
                attackPatternIndex = 0;
            }
        }

        private float CalculatePhaseSwitchHealthThreshold()
        {
            float thresholdMultiplier = attackPhaseSwitchHealthThresholdPercentage / 100;
            return Health.GetMaxHealth() * thresholdMultiplier;
        }

        public Attack GetAttack(string attackName)
        {
            if (attackLookup == null) BuildAttackLookup();

            return attackLookup[attackName];
        }

        private void BuildAttackLookup()
        {
            attackLookup = new Dictionary<string, Attack>();

            foreach (AttackMapping attack in Attacks)
            {
                attackLookup.Add(attack.attackName, attack.attack);
            }
        }

        private void OnDisable()
        {
            Health.onDeath -= Health_OnDeath;
            Health.onTakeDamage -= Health_OnTakeDamage;
        }
    }
}
