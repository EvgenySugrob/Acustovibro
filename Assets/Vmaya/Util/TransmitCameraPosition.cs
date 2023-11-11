using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vmaya.Util
{
    public class TransmitCameraPosition : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;
        public Camera Camera => _camera;

        protected virtual void Start()
        {
            CameraManager.instance.onChangeCamera.AddListener(onChangeCamera);
        }

        private void onChangeCamera()
        {
            if (_camera == CameraManager.getCurrent())
            {
                Vector3 pos = CameraManager.getLastCurrent().transform.position;
                transform.position = new Vector3(pos.x, transform.position.y, pos.z);
            }
            afterChangeCamera();
        }

        protected virtual void afterChangeCamera()
        {
        }
    }
}
