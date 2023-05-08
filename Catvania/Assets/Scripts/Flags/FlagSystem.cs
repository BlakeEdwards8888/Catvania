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

            if (!flagLookup.ContainsKey(flagID))
            {
                flagLookup[flagID] = false;
            }

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

        public void SetAllFlagsWithPrefix(string prefix, bool value)
        {
            if (flagLookup == null) BuildLookup();

            Dictionary<string, bool> flagLookupCache = new Dictionary<string,bool>(flagLookup);

            foreach(KeyValuePair<string, bool> kvp in flagLookupCache)
            {
                if (!kvp.Key.Contains(prefix)) continue;

                flagLookup[kvp.Key] = value;
            }
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
