using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Audio
{
    public class SoundEmitter : MonoBehaviour
    {
        [SerializeField] AudioSource soundPrefab;

        public void PlaySound(AudioClip audioClip)
        {
            AudioSource sound = Instantiate(soundPrefab, transform.position, Quaternion.identity);

            sound.clip = audioClip;
            sound.Play();
        }
    }
}
