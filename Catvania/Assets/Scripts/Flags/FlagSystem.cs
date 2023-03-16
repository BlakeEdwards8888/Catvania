using Cat.Saving;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Flags
{
    [CreateAssetMenu(fileName = "New Flag System", menuName = "Flag System")]
    public class FlagSystem : ScriptableObject, ISaveable
    {
        [SerializeField] Flag[] flags;

        Dictionary<string, bool> flagLookup;



        public bool CheckFlag(string flagID)
        {
            if (flagLookup == null) BuildLookup();

            return flagLookup[flagID];
        }

        private void BuildLookup()
        {
            flagLookup = new Dictionary<string, bool>();

            foreach(Flag flag in flags)
            {
                flagLookup.Add(flag.id, flag.value);
            }
        }

        public void SetFlag(string id, bool value)
        {
            if (flagLookup == null) BuildLookup();

            flagLookup[id] = value;
        }

        public object CaptureState()
        {
            if (flagLookup == null) BuildLookup();

            return flagLookup;
        }

        public void RestoreState(object state)
        {
            flagLookup = new Dictionary<string, bool>((Dictionary<string, bool>)state);
        }
    }
}
