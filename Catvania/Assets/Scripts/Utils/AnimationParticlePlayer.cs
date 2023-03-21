using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Utils
{
    public class AnimationParticlePlayer: MonoBehaviour
    {
        [SerializeField] ParticleSystem particleEffect;

        //function for animation event
        public void PlayParticleEffect()
        {
            particleEffect.Play();
        }
    }
}
