using Cat.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Pickups
{
    public class FlaskPickup : Pickup
    {
        protected override void OnPickUp(Collider2D other)
        {
            other.GetComponent<Healer>().AddHeal();
        }
    }
}
