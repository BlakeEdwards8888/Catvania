using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Audio
{
    public class SFXHandler : MonoBehaviour
    {
        [System.Serializable]
        public struct SoundMapping
        {
            public string soundName;
            public AudioClip sound;
        }

        [SerializeField] SoundMapping[] sfx;

        Dictionary<string, AudioClip> soundLookup = null;

        public AudioClip GetSound(string soundName)
        {
            if (soundLookup == null) BuildSoundLookup();

            return soundLookup[soundName];
        }

        private void BuildSoundLookup()
        {
            soundLookup = new Dictionary<string, AudioClip>();

            foreach (SoundMapping sound in sfx)
            {
                soundLookup.Add(sound.soundName, sound.sound);
            }
        }
    }
}
