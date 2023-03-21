using Cat.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Utils
{
    public class AnimationHelaerCaller : MonoBehaviour
    {
        [SerializeField] Healer healer;

        public void Heal()
        {
            healer.Heal();
        }
    }
}
