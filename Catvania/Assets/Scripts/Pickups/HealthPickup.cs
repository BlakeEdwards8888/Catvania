using Cat.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Pickups
{
    public class HealthPickup : Pickup
    {
        [SerializeField] int healthIncrease = 50;
        protected override void OnPickUp(Collider2D other)
        {
            other.GetComponent<Health>().AddMaxHealth(healthIncrease);
        }
    }
}
