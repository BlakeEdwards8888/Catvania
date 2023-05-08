using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Cat.Flags
{
    [ExecuteAlways]
    public class FlaggedObject : MonoBehaviour
    {
        [SerializeField] string prefix = "";
        [SerializeField] string flagKey = "";

        static Dictionary<string, FlaggedObject> globalLookup = new Dictionary<string, FlaggedObject>();

        public string GetFlagKey()
        {
            return flagKey;
        }

        public bool CheckFlag()
        {
            return Resources.Load<FlagSystem>("Default Flag System").CheckFlag(flagKey);
        }

        public void SetFlag(bool value)
        {
            Resources.Load<FlagSystem>("Default Flag System").SetFlag(flagKey, value);
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Application.IsPlaying(gameObject)) return;
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property = serializedObject.FindProperty("flagKey");

            if (string.IsNullOrEmpty(property.stringValue) || !IsUnique(property.stringValue)
                || !property.stringValue.Contains(prefix))
            {
                property.stringValue = prefix;
                property.stringValue += System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }

            globalLookup[property.stringValue] = this;
        }
#endif

        private bool IsUnique(string candidate)
        {
            if (!globalLookup.ContainsKey(candidate)) return true;

            if (globalLookup[candidate] == this) return true;

            if (globalLookup[candidate] == null)
            {
                globalLookup.Remove(candidate);
                return true;
            }

            if (globalLookup[candidate].GetFlagKey() != candidate)
            {
                globalLookup.Remove(candidate);
                return true;
            }

            return false;
        }
    }
}
