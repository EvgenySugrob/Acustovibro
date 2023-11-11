using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vmaya.Scene3D
{
    public class DragDropRB : DragDropOnPlane
    {
        [SerializeField]
        private float _dragForce = 1;
        protected Rigidbody rigidBody => GetComponent<Rigidbody>();
        protected override void doDrag()
        {
            if (rigidBody)
            {
                Vector3 point = transform.TransformPoint(centerOffset);
                rigidBody.AddForceAtPosition(currentCenterMovement * _dragForce, point, ForceMode.VelocityChange);
            }
            else base.doDrag();
        }
    }
}
