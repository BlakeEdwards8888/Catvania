using Cat.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Effects
{
    public class SpriteFlasher: MonoBehaviour
    {
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] float defaultFlashHoldTime = 0.1f;
        [SerializeField] float defaultFlashDuration = 0.1f;

        Coroutine currentCoroutine = null;

        private void OnEnable()
        {
            GetComponent<Health>().onTakeDamage += Flash;
        }

        public void Flash(float flashHoldTime, float flashDuration)
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            currentCoroutine = StartCoroutine(FlashCoroutine(flashHoldTime, flashDuration));
    }

        void Flash(float f)
        {
            if(currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            currentCoroutine = StartCoroutine(FlashCoroutine(defaultFlashHoldTime, defaultFlashDuration));
        }

        IEnumerator FlashCoroutine(float flashHoldTime, float flashDuration)
        {
            float fade = 1;

            spriteRenderer.material.SetFloat("_Fade", fade);

            yield return new WaitForSeconds(flashHoldTime);

            while (!Mathf.Approximately(fade, 0))
            {
                fade = Mathf.MoveTowards(fade, 0, Time.deltaTime / flashDuration);

                spriteRenderer.material.SetFloat("_Fade", fade);

                yield return null;
            }

            spriteRenderer.material.SetFloat("_Fade", 0);
        }

        private void OnDisable()
        {
            GetComponent<Health>().onTakeDamage -= Flash;
        }
    }
}
