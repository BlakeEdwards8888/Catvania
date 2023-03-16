using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Flags
{
    [CreateAssetMenu(fileName = "New Flag System", menuName = "Flag System")]
    public class FlagSystem : ScriptableObject
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
    }
}
