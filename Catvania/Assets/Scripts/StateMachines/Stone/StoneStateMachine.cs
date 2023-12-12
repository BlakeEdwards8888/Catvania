using Cat.Audio;
using Cat.Combat;
using Cat.Flags;
using Cat.Movement;
using Cat.Physics;
using UnityEngine;

namespace Cat.StateMachines.Stone
{
    public class StoneStateMachine : StateMachine
    {
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Health Health { get; private set; }
        [field: SerializeField] public Rigidbody2D Rb2d { get; private set; }
        [field: SerializeField] public float FallingGravityScale { get; private set; }
        [field: SerializeField] public Mover Mover { get; private set; }
        [field: SerializeField] public ForceHandler ForceHandler { get; private set; }
        [field: SerializeField] public float WalkSpeed { get; private set; }
        [field: SerializeField] public ContactFilter2D GroundFilter { get; private set; }
        [field: SerializeField] public float AggroRange { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float AttackCooldown { get; private set; }
        [field: SerializeField] public GameObject DeathEffect { get; private set; }
        [field: SerializeField] public SoundEmitter SoundEmitter { get; private set; }
        [field: SerializeField] public SFXHandler SFXHandler { get; private set; }

        private void OnEnable()
        {
            Health.onDeath += OnDeath;
        }

        private void Start()
        {
            SwitchState(new StonePatrolState(this));
        }

        public void PlaySound(string soundName)
        {
            SoundEmitter.PlaySound(SFXHandler.GetSound(soundName));
        }

        void OnDeath()
        {
            GetComponent<FlaggedObject>().SetFlag(true);
            Instantiate(DeathEffect, transform.position, Quaternion.identity);
            PlaySound("Death");
            Destroy(gameObject);
        }

        private void OnDisable()
        {
            Health.onDeath -= OnDeath;
        }
    }
}
