using Cat.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cat.UI
{
    public class ButtonSoundHandler : MonoBehaviour, ISelectHandler
    {
        [SerializeField] AudioClip selectedSound;

        SoundEmitter soundEmitter;

        private void Awake()
        {
            soundEmitter = GetComponent<SoundEmitter>();
        }

        public void OnSelect(BaseEventData eventData)
        {
            soundEmitter.PlaySound(selectedSound);
        }
    }
}
