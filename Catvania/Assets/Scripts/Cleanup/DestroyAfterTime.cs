using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Cleanup
{
    public class DestroyAfterTime : MonoBehaviour
    {
        [SerializeField] float aliveTime;

        float timeSinceSpawned;

        // Update is called once per frame
        void Update()
        {
            timeSinceSpawned += Time.deltaTime;

            if(timeSinceSpawned >= aliveTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
