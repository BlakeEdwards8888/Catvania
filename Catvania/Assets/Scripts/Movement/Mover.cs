using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Movement
{
    public class Mover : MonoBehaviour
    {
        Vector2 motion;

        public void Move(Vector2 motion)
        {
            this.motion = motion;
        }

        private void FixedUpdate()
        {
            GetComponent<Rigidbody2D>().velocity = motion;

            motion = Vector2.zero;
        }
    }
}
