using Cat.Flags;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Cleanup
{
    public class FlaggedObjectDestroyer : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            FlaggedObject flaggedObject = GetComponent<FlaggedObject>();

            if (flaggedObject.CheckFlag()) Destroy(gameObject);
        }
    }
}
