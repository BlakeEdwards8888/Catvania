using Cat.Combat;
using Cat.Movement;
using Cat.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Skelecat
{
    public class SkelecatStateMachine : StateMachine
    {
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Mover Mover { get; private set; }
        [field: SerializeField] public ForceHandler ForceHandler { get; private set; }
        [field: SerializeField] public float WalkSpeed { get; private set; }
        [field: SerializeField] public float RunSpeed { get; private set; }
        [field: SerializeField] public float FallingGravityScale { get; private set; }
        [field: SerializeField] public Rigidbody2D Rb2d { get; private set; }
        [field: SerializeField] public ContactFilter2D GroundFilter { get; private set; }
        [field: SerializeField] public Health Health { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float AttackRate { get; private set; }
        [field: SerializeField] public float AggroRange { get; private set; }
        [field: SerializeField] public GameObject DeathEffect { get; private set; }

        private void OnEnable()
        {
            Health.onDeath += OnDeath;
        }

        private void Start()
        {
            SwitchState(new SkelecatPatrolState(this));
        }

        void OnDeath()
        {
            Instantiate(DeathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        private void OnDisable()
        {
            Health.onDeath -= OnDeath;
        }
    }
}
