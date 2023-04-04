using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cat.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        public void StartLoadingScene(int scene)
        {
            StartCoroutine(LoadScene(scene));
        }

        public IEnumerator LoadScene(int scene)
        {
            yield return SceneManager.LoadSceneAsync(scene);
        }
    }
}
