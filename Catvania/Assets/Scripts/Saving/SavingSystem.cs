using Cat.Flags;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cat.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        Transform playerTransform;

        private void Awake()
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }

        public IEnumerator LoadLastScene(string saveFile)
        {
            Dictionary<string, object> state = LoadFile(saveFile);
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            if (state.ContainsKey("lastSceneBuildIndex"))
            {
                buildIndex = (int)state["lastSceneBuildIndex"];
            }
            yield return SceneManager.LoadSceneAsync(buildIndex);

            playerTransform.position = FindObjectOfType<SavePoint>().transform.position;

            RestoreState(state);
        }

        public void Save(string saveFile)
        {
            Dictionary<string, object> state = LoadFile(saveFile);
            CaptureState(state);
            SaveFile(saveFile, state);
        }

        public void Load(string saveFile)
        {
            RestoreState(LoadFile(saveFile));
        }

        private Dictionary<string, object> LoadFile(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            if (!File.Exists(path))
            {
                return new Dictionary<string, object>();
            }
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        private void SaveFile(string saveFile, object state)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Saving to " + path);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        private void CaptureState(Dictionary<string, object> state)
        {
            state["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
            state["flagData"] = Resources.Load<FlagSystem>("Default Flag System").CaptureState();
            state["playerData"] = playerTransform.GetComponent<SaveableEntity>().CaptureState();
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            Resources.Load<FlagSystem>("Default Flag System").RestoreState(state["flagData"]);
            playerTransform.GetComponent<SaveableEntity>().RestoreState(state["playerData"]);
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}