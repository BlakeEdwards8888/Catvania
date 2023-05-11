using Cat.Flags;
using System;
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
        const int DEFAULT_LAST_SCENE = 1;

        public static SavingSystem Instance;

        Transform playerTransform;
        string currentSaveFile;

        public event Action onSceneLoaded;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void LoadLastScene()
        {
            StartCoroutine(LoadLastSceneCoroutine(currentSaveFile));
        }

        public IEnumerator LoadLastSceneCoroutine(string saveFile)
        {
            Dictionary<string, object> state = LoadFile(saveFile);

            int buildIndex = DEFAULT_LAST_SCENE;

            if (state.ContainsKey("lastSceneBuildIndex"))
            {
                buildIndex = (int)state["lastSceneBuildIndex"];
            }

            yield return SceneManager.LoadSceneAsync(buildIndex);

            playerTransform = GameObject.FindWithTag("Player").transform;

            if (state.ContainsKey("lastSceneBuildIndex"))
            {
                playerTransform.position = FindObjectOfType<SavePoint>().transform.position;
            }

            RestoreState(state);

            onSceneLoaded?.Invoke();
        }

        public void Save()
        {
            Dictionary<string, object> state = LoadFile(currentSaveFile);
            CaptureState(state);
            SaveFile(currentSaveFile, state);
        }

        public void Load(string saveFile)
        {
            RestoreState(LoadFile(saveFile));
        }

        public void Delete(string saveFile)
        {
            File.Delete(GetPathFromSaveFile(saveFile));
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
            if(state.ContainsKey("flagData"))
            Resources.Load<FlagSystem>("Default Flag System").RestoreState(state["flagData"]);

            if(state.ContainsKey("playerData"))
            playerTransform.GetComponent<SaveableEntity>().RestoreState(state["playerData"]);
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }

        public void SetCurrentSaveFile(string saveFile)
        {
            currentSaveFile = saveFile;
        }
    }
}
