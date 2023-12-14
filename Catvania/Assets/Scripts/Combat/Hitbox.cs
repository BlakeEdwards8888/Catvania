using Cat.Physics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Combat
{
    public class Hitbox : MonoBehaviour
    {
        [SerializeField] int damage;
        [SerializeField] Vector2 knockback;
        [SerializeField] float hitstunDuration;
        [SerializeField] GameObject hitEffect;
        [Tooltip("Indicates which entitites this hitbox does NOT hit")]
        [SerializeField] string targetFilter;

        Vector2 directionToCollision;

        public event Action onHit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == targetFilter) return;

            directionToCollision = (other.transform.position - transform.position).normalized;

            if (other.TryGetComponent(out Health health))
            {
                if (health.IsInvulnerable() || health.IsDead()) return;

                health.TakeDamage(damage, hitstunDuration);
            }

            if (other.TryGetComponent(out ForceHandler forceHandler))
            {
                forceHandler.ResetVerticalVelocity();

                float horizontalForceDirection = directionToCollision.x > 0 ? 1 : -1;
                forceHandler.AddForce(horizontalForceDirection * knockback.x, knockback.y);
            }

            if(health != null || forceHandler != null)
            {
                if(hitEffect != null)
                    Instantiate(hitEffect, other.ClosestPoint(transform.position), CalculateRotation(directionToCollision));
                
                onHit?.Invoke();
            }
        }

        private Quaternion CalculateRotation(Vector2 direction)
        {
            return Quaternion.LookRotation(Vector3.forward, direction);
        }

        public void Setup(Attack attack)
        {
            damage = attack.Damage;
            knockback = attack.Knockback;
            hitstunDuration = attack.HitstunDuration;
        }

        public Vector2 GetDirectionToCollision()
        {
            return directionToCollision;
        }
    }
}
