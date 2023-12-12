using Cat.Combat;
using UnityEngine;

namespace Cat.StateMachines.Crusader
{
    public class CrusaderDownwardStrikeState : CrusaderBaseState
    {
        readonly int DownwardStrikeHash = Animator.StringToHash("DownwardStrike-Loop");

        Attack attack;

        public CrusaderDownwardStrikeState(CrusaderStateMachine stateMachine) : base(stateMachine) {}

        bool teleportedIn = false;

        public override void Enter()
        {            
            Transform playerTransform = GameObject.FindWithTag("Player").transform;

            stateMachine.transform.position = new Vector3(playerTransform.position.x,
                stateMachine.DownwardStrikeHeight, 0);

            attack = stateMachine.GetAttack("DownwardStrike");
            stateMachine.SwordHitbox.Setup(attack);

            stateMachine.Animator.Play(TeleportInFromBelowHash);

            stateMachine.PlaySound("Downstrike");
        }

        public override void Tick(float deltaTime)
        {
            if (!teleportedIn)
            {
                AnimatorStateInfo anim = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

                if (anim.IsTag("TeleportIn") && anim.normalizedTime >= 1)
                {
                    stateMachine.Animator.Play(DownwardStrikeHash);
                    teleportedIn = true;
                }

                return;
            }

            Move(stateMachine.DownwardStrikeSpeed, Vector2.down);

            if (IsGrounded())
            {
                Move(0, Vector2.up);
                stateMachine.SwitchState(new CrusaderLandingState(stateMachine));
            }
        }

        public override void Exit() {}
    }
}
