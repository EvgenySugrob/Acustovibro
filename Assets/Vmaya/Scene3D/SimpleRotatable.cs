using UnityEngine;

namespace Vmaya.Scene3D
{
    public class SimpleRotatable : MonoBehaviour, IRotatable
    {
        [SerializeField]
        private Transform _body;

        [SerializeField]
        private Vector3 _localAxis = Vector3.up;

        [SerializeField]
        private Vector3 _localBaseVector = Vector3.left;

        private float _angle;

        public Vector3 getAxis()
        {
            return transform.TransformDirection(_localAxis);
        }

        public Vector3 getBaseVector()
        {
            return transform.TransformDirection(_localBaseVector);
        }

        public float getAngle()
        {
            return _angle;
        }

        public bool rotateAvailable()
        {
            return gameObject.activeSelf && enabled;
        }

        public void setAngle(float angle)
        {
            _angle = angle;
            _body.localRotation = Quaternion.AngleAxis(_angle, _localAxis);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(new Ray(transform.position, getAxis()));
        }
    }
}
