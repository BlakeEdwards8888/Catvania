using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Projectiles
{
    public class BoomerangProjectile : MonoBehaviour
    {
        [SerializeField] float timeUntilReturn;

        float timeSinceSpawned = 0;
        bool isReturning;

        private void Update()
        {
            timeSinceSpawned += Time.deltaTime;

            if(timeSinceSpawned >= timeUntilReturn && !isReturning)
            {
                FlipVelocity();
                isReturning = true;
            }
        }

        private void FlipVelocity()
        {
            var rb2d = GetComponent<Rigidbody2D>();

            rb2d.velocity *= -1;
        }
    }
}
