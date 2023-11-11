using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vmaya.Scene3D;

namespace Vmaya.Util
{
    public abstract class BaseCameraControl : MonoBehaviour
    {
        [SerializeField]
        private int _mouseButton;
        protected int mouseButton => _mouseButton;

        [SerializeField]
        private Camera _myCamera;
        protected Camera myCamera => _myCamera;

        [SerializeField]
        [Range(0.00001f, 1)]
        private float _smooth = 0.5f;

        [SerializeField]
        private EasingFunction.Ease _easeMethod;

        private float _easeIndex;

        private bool _isDrag;
        protected bool isDrag => _isDrag;

        protected Vector3 getMousePosition()
        {
            Ray ray = _myCamera.ScreenPointToRay(VMouse.mousePosition);
            float distance;
            getPlane().Raycast(ray, out distance);
            return ray.GetPoint(distance);
        }

        public Plane getPlane()
        {
            return new Plane(transform.up, transform.position);
        }

        private bool isActive()
        {
            return CameraManager.getCurrent() == _myCamera;
        }

        protected void Update()
        {
            if (isActive())
            {
                if (_isDrag)
                {
                    if (VMouse.GetMouseButtonUp(_mouseButton))
                        stopDrag();
                    else drag();
                }
                else if (!hitDetector.isOverGUI() && VMouse.GetMouseButtonDown(_mouseButton)) beginDrag();
            }

            if (_easeIndex > 0)
            {
                _easeIndex -= _smooth * Time.deltaTime * 10f;
                easeMethod(EasingFunction.GetEasingFunction(_easeMethod)(1, 0,  _easeIndex));
            }
        }

        protected void beginEase()
        {
            _easeIndex = 1;
        }

        protected abstract void easeMethod(float value);

        protected virtual void beginDrag()
        {
            _isDrag = true;
        }

        protected abstract void drag();

        protected virtual void stopDrag()
        {
            _isDrag = false;
        }

    }
}
