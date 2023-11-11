using UnityEngine;
using Vmaya.Util;

namespace Vmaya.Scene3D
{
    public class RotateDragArrow : BaseDragDrop
    {
        private PulseRotate _pulseRotate;
        protected EntityRotateManager _rotateManager;
        protected IRotatable _rotatable => _rotateManager.Current;

        [SerializeField]
        private float _step;
        protected float Step => _step;

        private float _start;
        private float _startAngle;
        private Quaternion _startQ;

        private void Awake()
        {
            _rotateManager = GetComponentInParent<EntityRotateManager>();
            if (!_rotateManager)
                Debug.LogError("The parent must have an EntityRotateManager");

            _pulseRotate = GetComponentInParent<PulseRotate>();
        }

        public override void doClick()
        {
            base.doClick();
            RotateStep();
        }

        public override void doOver()
        {
            base.doOver();
            if (_pulseRotate) _pulseRotate.enabled = false;
        }

        public override void doOut()
        {
            base.doOut();
            Utils.setTimeout(this, afterOut, 0.5f);
        }

        private void afterOut()
        {
            if (_pulseRotate && !(focus && focus.transform.transform.IsChildOf(_rotateManager.transform)) && !BaseDragDrop.isDragging)
                _pulseRotate.enabled = true;
        }

        protected float hitAngle()
        {
            Vector3 up = _rotateManager.Current.getAxis();
            Vector3 baseDirect = Quaternion.LookRotation(up) * Vector3.right;

            Plane plane = new Plane(up, _rotateManager.CurrentComponent.transform.position);
            float distance;
            Ray ray = hitDetector.mRay();
            if (plane.Raycast(ray, out distance))
            {
                Vector3 p = ray.GetPoint(distance) - transform.position;
                return Vector3.SignedAngle(p.normalized, baseDirect, up);
            }

            return 0;
        }

        public override void doMouseDown()
        {
            base.doMouseDown();
            _start = hitAngle();
            _startQ = _rotateManager.arrowAround.transform.rotation;
            _startAngle = _rotateManager.Current.getAngle();
        }

        protected float Diff => _start - hitAngle();

        protected override bool Dragging()
        {
            return Mathf.Abs(Diff) > 0.1f;
        }

        protected override void doDrag()
        {
            float diff = Diff;

            float absStep = Mathf.Abs(_step);
            if (absStep > 0) diff = Mathf.Round(diff / absStep) * absStep;
            _rotateManager.arrowAround.transform.rotation = Quaternion.AngleAxis(diff, _rotatable.getAxis()) * _startQ;
            _rotatable.setAngle(_startAngle + diff);
        }

        protected virtual void RotateStep()
        {
            _rotatable.setAngle(_startAngle + _step);
        }
    }
}
