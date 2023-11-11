using MyBox;
using System.Collections.Generic;
using UnityEngine;
using Vmaya.Util;

namespace Vmaya.Scene3D
{
    public abstract class DragDropThreshold : BaseDragDrop
    {

        [SerializeField]
        private Collider _boundsCollider;

        [SerializeField]
        [Tooltip("Threshold to start dragging in pixels")]
        private float _mouseThresholdStart = 1f;

        [SerializeField]
        [Tooltip("Limit offset per second")]
        private float _movementLimit = 2f;

        [SerializeField]
        private bool _findCollisions = true;

        private Vector3 _centerOffset;
        protected Vector3 centerOffset => _centerOffset;

        protected Vector3 currentCenterMovement => calculateMovement();

        private Vector3 _mouseStart;
        protected Collider boundsCollider => getCollider();
        protected ILimiter limiter => getLimiter();

        protected abstract Vector3 getMousePosition();
        private Bounds _bounds;
        private Vector3 _boundsOffset;

        protected Collider getCollider()
        {
            if (!_boundsCollider)
                _boundsCollider = GetComponent<BoxCollider>();

            return _boundsCollider;
        }

        protected virtual void Start()
        {
            getCollider();
        }

        public static Vector3 getMousePositionOnPlane(Vector3 planePos, Vector3 planeNormal)
        {
            Plane plane = new Plane(planeNormal, planePos);
            float distance;
            Ray ray = hitDetector.mRay();
            plane.Raycast(ray, out distance);
            return ray.GetPoint(distance);
        }

        protected Vector3 calculateMovement()
        {
            Vector3 newPos = getMousePosition() - transform.TransformVector(_centerOffset);
            Vector3 movement = newPos - getPosition();

            if (_movementLimit > 0)
                movement = movement.normalized * Mathf.Min(movement.magnitude, _movementLimit * Time.deltaTime);

            newPos = Restrict(getPosition() + movement);
            
            return newPos - getPosition();
        }

        protected virtual ILimiter getLimiter()
        {
            return GetComponentInParent<ILimiter>();
        }

        protected virtual Vector3 getPosition()
        {
            return transform.position;
        }

        protected virtual void setPosition(Vector3 newPos)
        {
            transform.position = newPos;
        }

        protected virtual Vector3 Restrict(Vector3 newPosition)
        {
            Vector3 delta = newPosition - getPosition();

            if (delta.sqrMagnitude > 0)
            {
                if (_findCollisions && boundsCollider)
                {
                    Utils.findFutureCollisions(boundsCollider, ref delta);
                    newPosition = getPosition() + delta;
                }

                if (limiter != null)
                {
                    _bounds.center = getPosition() - _boundsOffset;
                     newPosition = limiter.checkLimits(_bounds, delta) + _boundsOffset;
                }
            }
            return newPosition;
        }

        public override void doMouseDown()
        {
            base.doMouseDown();
            _mouseStart = VMouse.mousePosition;
        }

        protected override bool Dragging()
        {
            return (_mouseStart - VMouse.mousePosition).magnitude >= _mouseThresholdStart;
        }

        protected virtual Vector3 calculateCenterOffset()
        {
            return transform.InverseTransformVector(getMousePosition() - getPosition());
        }

        protected void RefreshCenterOffset()
        {
            _centerOffset = calculateCenterOffset();
        }

        protected Bounds calcBounds()
        {
            return boundsCollider.bounds;
        }

        protected override void beginDrag()
        {
            if (boundsCollider)
            {
                _bounds = calcBounds(); 
                _boundsOffset = getPosition() - _bounds.center;
            }
            RefreshCenterOffset();
            base.beginDrag();
        }
    }
}
