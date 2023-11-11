using System;
using UnityEngine;
using Vmaya.Util;

namespace Vmaya.Scene3D
{
    public class DragDropOnPlane : DragDropThreshold
    {

        [SerializeField]
        private Vector3 _dragPlane = Vector3.up;

        protected Vector3 DragPlace => _startPlane;
        private Vector3 _startPlane;

        protected virtual void OnValidate()
        {
            if (_dragPlane.sqrMagnitude > 0) _dragPlane = _dragPlane.normalized;
        }

        protected override Vector3 getMousePosition()
        {
            return getMousePositionOnPlane(getPosition(), _startPlane);
        }

        protected override void beginDrag()
        {
            if (_dragPlane.sqrMagnitude > 0) _startPlane = _dragPlane;
            else _startPlane = (Camera.main.transform.position - transform.position).normalized;
            base.beginDrag();
        }

        protected override void doDrag()
        {
            setPosition(getPosition() + currentCenterMovement);
        }
    }
}
