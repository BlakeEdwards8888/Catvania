using Cat.Audio;
using Cat.Combat;
using Cat.Flags;
using Cat.Movement;
using Cat.Physics;
using UnityEngine;

namespace Cat.StateMachines.Bat
{
    public class BatStateMachine : StateMachine
    {
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public float ChaseSpeed { get; private set; }
        [field: SerializeField] public float AggroRange { get; private set; }
        [field: SerializeField] public Mover Mover { get; private set; }
        [field: SerializeField] public ForceHandler ForceHandler { get; private set; }
        [field: SerializeField] public Rigidbody2D Rb2d { get; private set; }
        [field: SerializeField] public GameObject DeathEffect { get; private set; }
        [field: SerializeField] public SoundEmitter SoundEmitter { get; private set; }
        [field: SerializeField] public SFXHandler SFXHandler { get; private set; }

        private void OnEnable()
        {
            GetComponent<Health>().onTakeDamage += EnterHitstun;
            GetComponent<Health>().onDeath += OnDeath;
        }

        private void Start()
        {
            SwitchState(new BatSleepState(this));
        }

        public void PlaySound(string soundName)
        {
            SoundEmitter.PlaySound(SFXHandler.GetSound(soundName));
        }

        private void EnterHitstun(float stunDuration)
        {
            SwitchState(new BatHitstunState(this, stunDuration));
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
            GetComponent<Health>().onTakeDamage -= EnterHitstun;
            GetComponent<Health>().onDeath -= OnDeath;
        }
    }
}
