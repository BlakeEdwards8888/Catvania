using UnityEngine;
using UnityEngine.Rendering;

namespace Cat.Rendering
{
    public class VolumeProfileSwitcher : MonoBehaviour
    {
        Volume globalVolume;

        private void Awake()
        {
            globalVolume = GetComponent<Volume>();
        }

        public void Switch(VolumeProfile profile)
        {
            globalVolume.profile = profile;
        }
    }
}
