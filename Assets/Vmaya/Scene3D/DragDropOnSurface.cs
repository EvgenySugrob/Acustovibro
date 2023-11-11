using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vmaya.Scene3D
{
    public class DragDropOnSurface : DragDropThreshold
    {
        [SerializeField]
        protected bool _relativeReorientation;

        private RaycastHit _surface;
        private Vector3 _prevNormal;
        protected RaycastHit surface => _surface;

        protected override Vector3 getMousePosition()
        {
            if (findSurface())
                return _surface.point;
            else return getMousePositionOnPlane(_surface.point, _surface.normal);
        }

        protected bool findSurface()
        {
            RaycastHit hit;
            if (nearestSurface(out hit))
            {
                _surface = hit;
                return true;
            }
            return false;
        }

        protected virtual void DoChangeSurface()
        {
            Vector3 axis = Vector3.Cross(_surface.normal, _prevNormal).normalized;
            if (_relativeReorientation)
            {
                float angle = Vector3.SignedAngle(_surface.normal, _prevNormal, -axis);
                transform.rotation = Quaternion.AngleAxis(angle, axis) * transform.rotation;
            } else transform.rotation = Quaternion.LookRotation(_surface.normal);

            _prevNormal = _surface.normal;
        }

        protected override Vector3 calculateCenterOffset()
        {

            Vector3 offset = default;
            if (_surface.normal.sqrMagnitude == 0)
            {
                if (findSurface())
                    return calculateCenterOffset();
            }
            else
            {
                if (!_surface.normal.Equals(_prevNormal))
                    DoChangeSurface();

                Plane plane = new Plane(_surface.normal, getPosition());
                float enter;
                Ray ray = hitDetector.mRay();
                plane.Raycast(ray, out enter);

                offset = transform.InverseTransformPoint(ray.GetPoint(enter));
                _prevNormal = _surface.normal;
            }

            return offset;
        }

        protected bool nearestSurface(out RaycastHit hitResult)
        {
            float minDistance = float.MaxValue;
            hitResult = default;
            bool result = false;
            Camera cam = CameraManager.getCurrent();

            RaycastHit[] hits = Physics.RaycastAll(hitDetector.mRay(), float.MaxValue, cam.cullingMask);
            foreach (RaycastHit hit in hits)
                if (!hit.transform.IsChildOf(transform))
                {
                    float distance = (cam.transform.position - hit.point).magnitude;
                    if ((minDistance > distance) && isSuitableSurface(hit))
                    {
                        minDistance = distance;
                        hitResult = hit;
                        result = true;
                    }
                }

            return result;
        }

        protected override void doDrag()
        {
            if (!_surface.normal.Equals(_prevNormal))
                DoChangeSurface();

            setPosition(getPosition() + currentCenterMovement);
        }

        protected virtual bool isSuitableSurface(RaycastHit hit)
        {
            return true;
        }
    }
}
