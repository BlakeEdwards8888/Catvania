using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Cameras
{
    public class BossDeathCamera : MonoBehaviour
    {
        [SerializeField] Color backgroundColor;
        [SerializeField] LayerMask cullingMask;

        Color cam_backgroundColor;
        LayerMask cam_cullingMask;

        CinemachineVirtualCamera vcam;

        bool isActive;

        private void Awake()
        {
            vcam = GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            if (!CinemachineCore.Instance.IsLive(vcam) && isActive)
            {
                Camera.main.backgroundColor = cam_backgroundColor;
                Camera.main.cullingMask = cam_cullingMask;
                isActive = false;
            }
        }

        //this is called by the virtual camera's OnCameraLive event
        public void ActivateCamera()
        {
            cam_backgroundColor = Camera.main.backgroundColor;
            cam_cullingMask = Camera.main.cullingMask;

            Camera.main.backgroundColor = backgroundColor;
            Camera.main.cullingMask = cullingMask;

            isActive = true;
        }
    }
}
