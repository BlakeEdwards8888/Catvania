using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Crusader
{
    public class CrusaderFlurryState : CrusaderBaseState
    {
        readonly int FlurryHash = Animator.StringToHash("Flurry");

        public CrusaderFlurryState(CrusaderStateMachine stateMachine) : base(stateMachine) {}

        bool teleportedIn = false;

        public override void Enter()
        {
            CalculcateSpawnPosition();
            FacePlayer();
            stateMachine.Rb2d.bodyType = RigidbodyType2D.Kinematic;
            stateMachine.Animator.Play(TeleportInFromAboveHash);
        }

        public override void Tick(float deltaTime)
        {
            AnimatorStateInfo anim = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
            
            if (anim.IsTag("TeleportIn") && anim.normalizedTime >= 1 && !teleportedIn)
            {
                stateMachine.Animator.Play(FlurryHash);
                teleportedIn = true;
            }

            if (anim.IsTag("Attack") && anim.normalizedTime >= 1 && teleportedIn)
            {
                stateMachine.SwitchState(new CrusaderTeleportState(stateMachine));
            }
        }

        public override void Exit()
        {
            stateMachine.Rb2d.bodyType = RigidbodyType2D.Dynamic;
        }

        private void CalculcateSpawnPosition()
        {
            Transform playerTransform = GameObject.FindWithTag("Player").transform;

            float randomNum = UnityEngine.Random.value; //calculate a random number to determine which side to start on

            stateMachine.transform.position = TrySpawn(playerTransform, randomNum < 0.4f ? -2 : 2);
        }

        private Vector2 TrySpawn(Transform playerTransform, float xOffset)
        {
            Vector2 tempPosition = new Vector2(playerTransform.position.x + xOffset, 0.6f); //0.6 is the y origin so that the 1x1 wall check is slightly off the ground

            if (Physics2D.BoxCast(tempPosition, new Vector2(1, 1), 0, Vector2.up, 0.1f, stateMachine.GroundLayerMask))
            {
                return TrySpawn(playerTransform, xOffset * -1);
            }

            tempPosition.y = 0; //reset the y to zero because we want to spawn on the ground

            return tempPosition;
        }
    }
}
