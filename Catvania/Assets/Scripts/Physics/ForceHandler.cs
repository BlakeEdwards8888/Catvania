using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Physics
{
    public class ForceHandler : MonoBehaviour
    {
        Vector2 force;
        
        private Vector2 dampingVelocity;
        Rigidbody2D rb2d;
        float smoothTime;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            force = Vector2.SmoothDamp(force, Vector2.zero, ref dampingVelocity, smoothTime);

            if(force.magnitude <= 0.1f)
            {
                force = Vector2.zero;
            }
        }

        public void AddForce(Vector2 force)
        {
            smoothTime = force.magnitude/100;   //Set smoothTime to be 1/100th of the force magnitude so that more powerful forces take longer to return to zero

            Vector2 tempVelocity = rb2d.velocity;
            tempVelocity.y += force.y;
            rb2d.velocity = tempVelocity;

            force.y = 0;

            this.force += force;
        }

        public void AddForce(float x, float y)
        {
            AddForce(new Vector2(x, y));
        }

        //This is to be used in animation events
        public void AddHorizontalForce(float x)
        {
            AddForce(x * transform.localScale.x, 0);
        }

        public Vector2 GetForce()
        {
            return force;
        }

        public void ResetVerticalVelocity()
        {
            Vector2 tempVelocity = rb2d.velocity;
            tempVelocity.y = 0;
            rb2d.velocity = tempVelocity;
        }
    }
}
