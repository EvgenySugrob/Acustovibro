using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vmaya.Scene3D;

namespace Vmaya.Util
{
    public class RotateAround : BaseCameraControl
    {
        [SerializeField]
        private bool rotateAroundCenter;

        [SerializeField]
        private float _factor = 1;

        [SerializeField]
        private float _angleStep = 1;

        private float _prevAngle;
        private float _startAngle;
        private Quaternion _rotateTo;
        private Quaternion _rotateStart;

        public Vector3 Axis => getAxis();

        protected virtual Vector3 getAxis()
        {
            return Vector3.up;
        }

        public float getAngle()
        {
            Vector3 mp = getMousePosition();
            return Vector3.SignedAngle(Vector3.forward, mp - transform.position, Axis);
        }

        protected override void beginDrag()
        {
            base.beginDrag();
            if (rotateAroundCenter)
                _prevAngle = getAngle();
            else _prevAngle = VMouse.mousePosition.x;

            _startAngle = CalcCurrentAngle();
        }

        public float CalcCurrentAngle()
        {
            return Vector3.SignedAngle(Vector3.forward, transform.forward, Axis);
        }

        protected override void drag()
        {
            float delta;
            if (rotateAroundCenter)
                delta = _prevAngle - getAngle();
            else delta = VMouse.mousePosition.x - _prevAngle;

            setRotateTo(_startAngle + delta * _factor);
        }

        public void setRotateTo(float a_rotateToAngle)
        {
            _rotateTo = Quaternion.AngleAxis(Vmaya.Utils.roundToStep(a_rotateToAngle, _angleStep), Axis);
            _rotateStart = transform.localRotation;
            beginEase();
        }

        protected override void stopDrag()
        {
            base.stopDrag();
            transform.localRotation = _rotateTo;
        }

        protected override void easeMethod(float value)
        {
            transform.localRotation = Quaternion.Lerp(_rotateStart, _rotateTo, value);
        }
    }
}
