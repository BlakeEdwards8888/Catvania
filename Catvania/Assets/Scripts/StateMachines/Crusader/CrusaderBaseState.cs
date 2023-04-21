using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.StateMachines.Crusader
{
    public abstract class CrusaderBaseState : State
    {
        private const float GroundCheckDistance = 0.1f;

        protected CrusaderStateMachine stateMachine;

        public CrusaderBaseState(CrusaderStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        protected void Move(float speed, Vector2 direction)
        {
            stateMachine.Mover.Move(direction * speed);
        }

        protected bool IsGrounded()
        {
            RaycastHit2D[] results = new RaycastHit2D[1];

            if (stateMachine.Rb2d.Cast(-Vector2.up, stateMachine.GroundContactFilter, results, GroundCheckDistance) > 0)
            {
                return true;
            }

            return false;
        }

        protected void FacePlayer()
        {
            Transform playerTransform = GameObject.FindWithTag("Player").transform;

            float xScale = playerTransform.position.x < stateMachine.transform.position.x ? -1 : 1;

            Vector3 tempScale = new Vector3(xScale, stateMachine.transform.localScale.y, stateMachine.transform.localScale.z);

            stateMachine.transform.localScale = tempScale;
        }
    }
}
