using Cat.Audio;
using Cat.Combat;
using Cat.Controls;
using Cat.Effects;
using Cat.Flags;
using Cat.Movement;
using Cat.Physics;
using Cat.Saving;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Player
{
    public class PlayerStateMachine : StateMachine
    {
        [System.Serializable]
        public struct AttackMapping
        {
            public string attackName;
            public Attack attack;
        }

        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public InputReader InputReader { get; private set; }
        [field: SerializeField] public float FreeMoveSpeed { get; private set; }
        [field: SerializeField] public float DashSpeed { get; private set; }
        [field: SerializeField] public float JumpForce { get; private set; }
        [field: SerializeField] public float FallingGravityScale { get; private set; }
        [field: SerializeField] public float DashGravityScale { get; private set; }
        [field: SerializeField] public Mover Mover { get; private set; }
        [field: SerializeField] public Rigidbody2D Rb2d { get; private set; }
        [field: SerializeField] public ContactFilter2D GroundFilter { get; private set; }
        [field: SerializeField] public ForceHandler ForceHandler { get; private set; }
        [field: SerializeField] public AttackMapping[] Attacks { get; private set; }
        [field: SerializeField] public Hitbox Hitbox { get; private set; }
        [field: SerializeField] public TrailRenderer DashTrail { get; private set; }
        [field: SerializeField] public ParticleSystem DashParticles { get; private set; }
        [field: SerializeField] public FlagSystem FlagSystem { get; private set; }
        [field: SerializeField] public Health Health { get; private set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField] public Healer Healer { get; private set; }
        [field: SerializeField] public TimeManipulator TimeManipulator { get; private set; }
        [field: SerializeField] public SoundEmitter SoundEmitter { get; private set; }
        [field: SerializeField] public SFXHandler SFXHandler { get; private set; }

        [SerializeField] float invulnerabilityDuration = 1;
        [SerializeField] float hitTimeScale = 0.2f;
        [SerializeField] float hitTimeManipulationDuration = 0.2f;

        Dictionary<string, Attack> attackLookup = null;


        bool canDoubleJump;
        bool canUpstrike;
        bool canAttack = true;


        private void OnEnable()
        {
            GetComponent<Health>().onTakeDamage += Health_OnTakeDamage;
            GetComponent<Health>().onDeath += OnDeath;
            SavingSystem.Instance.onSceneLoaded += Start;
        }

        private void Start()
        {
            SwitchState(new PlayerFreeMoveState(this));
        }

        private void Health_OnTakeDamage(float duration)
        {
            TimeManipulator.StartManipulatingTime(hitTimeScale, hitTimeManipulationDuration);
            SwitchState(new PlayerHitstunState(this, duration));
        }

        void OnDeath()
        {
            SwitchState(new PlayerDeathState(this));
        }

        public Attack GetAttack(string attackName)
        {
            if (attackLookup == null) BuildAttackLookup();

            return attackLookup[attackName];
        }

        private void BuildAttackLookup()
        {
            attackLookup = new Dictionary<string, Attack>();

            foreach(AttackMapping attack in Attacks)
            {
                attackLookup.Add(attack.attackName, attack.attack);
            }
        }

        public void PlaySound(string soundName)
        {
            SoundEmitter.PlaySound(SFXHandler.GetSound(soundName));
        }

        public IEnumerator<object> InvulnerabilityCoroutine()
        {
            Color spriteColorCache = SpriteRenderer.color;

            spriteColorCache.a = 0.5f;
            SpriteRenderer.color = spriteColorCache;

            yield return new WaitForSeconds(invulnerabilityDuration);

            spriteColorCache.a = 1;
            SpriteRenderer.color = spriteColorCache;

            Health.SetIsInvulnerable(false);
        }

        public void SetCanDoubleJump(bool value)
        {
            canDoubleJump = value;
        }

        public bool CanDoubleJump()
        {
            return canDoubleJump && FlagSystem.CheckFlag("HAS_DOUBLE_JUMP");
        }
        
        public void SetCanAttack(bool value)
        {
            canAttack = value;
        }

        public bool CanAttack()
        {
            return canAttack;
        } 
        
        public void SetCanUpstrike(bool value)
        {
            canUpstrike = value;
        }

        public bool CanUpstrike()
        {
            return canUpstrike;
        }

        private void OnDisable()
        {
            GetComponent<Health>().onTakeDamage -= Health_OnTakeDamage;
            GetComponent<Health>().onDeath -= OnDeath;
            SavingSystem.Instance.onSceneLoaded -= Start;
        }
    }
}
