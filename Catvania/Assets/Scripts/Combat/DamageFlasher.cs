using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Combat
{
    public class DamageFlasher : MonoBehaviour
    {
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] float flashHoldTime = 0.1f;
        [SerializeField] float flashDuration = 0.1f;

        Coroutine currentCoroutine = null;

        private void OnEnable()
        {
            GetComponent<Health>().onTakeDamage += Flash;
        }

        void Flash(float f)
        {
            if(currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            currentCoroutine = StartCoroutine(FlashCoroutine());
        }

        IEnumerator FlashCoroutine()
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
