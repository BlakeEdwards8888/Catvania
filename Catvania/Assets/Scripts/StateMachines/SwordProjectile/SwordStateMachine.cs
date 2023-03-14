using Cat.Combat;
using Cat.Controls;
using Cat.Movement;
using Cat.StateMachines.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Sword
{
    public class SwordStateMachine: StateMachine
    {
        [field: SerializeField] public float TimeUntilReturn { get; private set; }
        [field: SerializeField] public float ReturnMoveSpeed { get; private set; }
        [field: SerializeField] public Rigidbody2D Rb2d { get; private set; }
        [field: SerializeField] public ContactFilter2D GroundFilter { get; private set; }
        [field: SerializeField] public GameObject Platform { get; private set; }
        [field: SerializeField] public Hitbox Hitbox { get; private set; }
        [field: SerializeField] public InputReader InputReader { get; private set; }
        [field: SerializeField] public ParticleSystem HitEffect { get; private set; }
        [field: SerializeField] public Collider2D HitboxCollider { get; private set; }

        private void Start()
        {
            SwitchState(new SwordProjectileState(this));
        }

        public void ReturnToPlayer()
        {
            PlayerStateMachine player = FindObjectOfType<PlayerStateMachine>();
            player.SetCanAttack(true);
            Destroy(gameObject);
        }
    }
}
