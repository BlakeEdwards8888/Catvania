using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Combat
{
    [System.Serializable]
    public class Attack
    {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public Vector2 Knockback { get; private set; }        
        [field: SerializeField] public Vector2 RecoilForce { get; private set; }        
        [field: SerializeField] public float HitstunDuration { get; private set; }        
    }
}
