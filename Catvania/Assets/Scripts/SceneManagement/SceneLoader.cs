using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cat.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        public IEnumerator LoadScene(int scene)
        {
            yield return SceneManager.LoadSceneAsync(scene);
        }
    }
}
