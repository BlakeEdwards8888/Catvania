using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Cat.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum PortalIdentifier
        {
            A, B, C, D
        }

        [SerializeField] int sceneToLoad;
        [SerializeField] PortalIdentifier identifier;
        [SerializeField] Transform spawnPoint;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine(TransitionToNewScene());
            }
        }

        IEnumerator TransitionToNewScene()
        {
            DontDestroyOnLoad(gameObject);

            yield return GetComponent<SceneLoader>().LoadScene(sceneToLoad);

            Portal otherPortal = FindOtherPortal();

            Transform playerTransform = GameObject.FindWithTag("Player").transform;

            playerTransform.position = otherPortal.GetSpawnPostion();
            playerTransform.localScale = otherPortal.GetSpawnScale();

            Destroy(gameObject);
        }

        private Portal FindOtherPortal()
        {
            foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.identifier != identifier) continue;

                return portal;
            }

            Debug.LogError("Other portal not found");
            return null;
        }

        public Vector3 GetSpawnScale()
        {
            return spawnPoint.localScale;
        }

        public Vector3 GetSpawnPostion()
        {
            return spawnPoint.position;
        }
    }
}
