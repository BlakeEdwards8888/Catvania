using Cat.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Effects
{
    public class TimeManipulator: MonoBehaviour
    {
        float timeScale = 0.5f;
        float manipulationDuration = 0.1f;

        public void StartManipulatingTime(float timeScale, float manipulationDuration, Action onManipulationFinished = null)
        {
            this.timeScale = timeScale;
            this.manipulationDuration = manipulationDuration;

            StartCoroutine(ManipulationCoroutine(onManipulationFinished));
        }

        IEnumerator ManipulationCoroutine(Action callback)
        {
            float timeSinceSlowed = 0;

            Time.timeScale = timeScale;

            while(timeSinceSlowed < manipulationDuration)
            {
                timeSinceSlowed += Time.unscaledDeltaTime;
                yield return null;
            }

            Time.timeScale = 1;
            callback?.Invoke();
        }
    }
}
