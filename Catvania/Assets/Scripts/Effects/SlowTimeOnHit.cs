using Cat.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Effects
{
    public class SlowTimeOnHit : MonoBehaviour
    {
        [SerializeField] float slowTimeScale = 0.5f;
        [SerializeField] float slowDownDuration = 0.1f;

        private void OnEnable()
        {
            GetComponent<Health>().onTakeDamage += Slow;
        }

        private void Slow(float hitstunDuration)
        {
            StartCoroutine(SlowCoroutine());
        }

        IEnumerator SlowCoroutine()
        {
            float timeSinceSlowed = 0;

            Time.timeScale = slowTimeScale;

            while(timeSinceSlowed < slowDownDuration)
            {
                timeSinceSlowed += Time.unscaledDeltaTime;
                yield return null;
            }

            Time.timeScale = 1;
        }

        private void OnDisable()
        {
            GetComponent<Health>().onTakeDamage -= Slow;
        }
    }
}
