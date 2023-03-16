using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Core
{
    public class CinemachineTargetFinder : MonoBehaviour
    {
        private void Start()
        {
            CinemachineVirtualCamera playerVCam = GetComponent<CinemachineVirtualCamera>();

            playerVCam.m_Follow = GameObject.FindWithTag("Player").transform;
        }
    }
}
