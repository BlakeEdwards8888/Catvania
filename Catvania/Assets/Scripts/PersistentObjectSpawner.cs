using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjectSpawner : MonoBehaviour
{
    static bool spawned;

    [SerializeField] GameObject persistentObjectsPrefab;

    private void Awake()
    {
        if (spawned)
        {
            Destroy(gameObject);
        }
        else
        {
            Instantiate(persistentObjectsPrefab, transform);
            DontDestroyOnLoad(gameObject);
            spawned = true;
        }
    }

}
