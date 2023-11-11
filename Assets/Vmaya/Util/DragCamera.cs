using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vmaya.Util
{
    public class DragCamera : BaseCameraControl
    {

        private Vector3 _prevPosition;

        private Vector3 _moveTo;

        private Vector3 _moveStart;

        protected override void beginDrag()
        {
            base.beginDrag();
            _prevPosition = getMousePosition();
            _moveTo = transform.position;
        }

        protected override void drag()
        {
            _moveTo += _prevPosition - getMousePosition();
            _moveStart = transform.position;
            beginEase();
        }

        public void setPositionXZTo(Vector3 pos)
        {
            _moveTo = new Vector3(pos.x, transform.position.y, pos.z);
            _moveStart = transform.position;
            beginEase();
        }

        protected override void stopDrag()
        {
            base.stopDrag();
            transform.position = _moveTo;
        }

        protected override void easeMethod(float value)
        {
            transform.position = Vector3.Lerp(_moveStart, _moveTo, value);
            _prevPosition = getMousePosition();
        }
    }
}
