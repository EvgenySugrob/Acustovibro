using UnityEngine;
using UnityEngine.InputSystem;
using Vmaya.Util;

namespace Vmaya.Scene3D
{
    public class DragPlace : MonoBehaviour
    {
        [SerializeField]
        private Transform dragable;
        [SerializeField]
        private int mouseButtonCode;
        [SerializeField]
        private float _limitPower;
        [SerializeField]
        private float startThreshold;
        [SerializeField]
        private Vector3 spaceThreshold;

        private Vector3 _startMousePos;
        private Vector3 _prevPos;
        private Vector3 _delta = default;

        private bool _isDrag;

        private void Update()
        {
            if (VMouse.GetMouseButtonDown(mouseButtonCode) && allowDragging())
            {
                doMouseDown();
                _isDrag = true;
            } else if (VMouse.GetMouseButtonUp(mouseButtonCode)) _isDrag = false;

            if (_isDrag) doDrag();
        }

        public void doMouseDown()
        {
            _prevPos = getMousePosition();
            _startMousePos = VMouse.mousePosition;
        }

        protected Vector3 getMousePosition()
        {
            Plane plane = new Plane(transform.up, transform.position);
            float distance;
            Ray ray = hitDetector.mRay();
            plane.Raycast(ray, out distance);
            return ray.GetPoint(distance);
        }

        protected Vector3 LimitSpace(Vector3 _delta)
        {
            float LimitComponent(float p, float limit, float delta)
            {
                float np = p + delta;
                if (np < -limit) 
                    delta -= limit + np;
                else if (np > limit) 
                    delta -= np - limit;
                return delta;
            }
            if (spaceThreshold.sqrMagnitude > 0)
            {
                _delta.x = LimitComponent(dragable.position.x, spaceThreshold.x, _delta.x);
                _delta.y = LimitComponent(dragable.position.y, spaceThreshold.y, _delta.y);
                _delta.z = LimitComponent(dragable.position.z, spaceThreshold.z, _delta.z);
            }

            return _delta;
        }

        protected void doDrag()
        {
            if (allowDragging())
            {
                Vector3 v = getMousePosition();
                _delta = LimitSpace(_prevPos - v);
                if (_limitPower > 0)
                    _delta = _delta.normalized * Mathf.Min(_limitPower, _delta.magnitude);

                 dragable.position += _delta;
                _prevPos = v + _delta;
            }

        }

        protected virtual bool allowDragging()
        {
            Vector3 startDelta = _startMousePos - VMouse.mousePosition;
            return !hitDetector.isOverGUI() && !VKeyboard.GetKey(Key.LeftShift) && 
                    !BaseDragDrop.isDragging && !(hitDetector.Focus is IDragControl) && 
                    (startDelta.magnitude > startThreshold);
        }
    }
}