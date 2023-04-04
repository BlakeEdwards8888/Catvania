using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cat.Cleanup
{
    public class DestroyOnSceneLoaded : MonoBehaviour
    {
        [SerializeField] int destructionSceneBuildIndex;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += SceneManager_SceneLoaded;
        }

        private void SceneManager_SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == destructionSceneBuildIndex) Destroy(gameObject);
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= SceneManager_SceneLoaded;
        }
    }
}
