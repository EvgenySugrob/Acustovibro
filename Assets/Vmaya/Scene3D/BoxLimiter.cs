using UnityEngine;

namespace Vmaya.Scene3D
{
    public class BoxLimiter : MonoBehaviour, ILimiter
    {
        [SerializeField]
        private Vector3 _size;
        [SerializeField]
        private Transform _center;

        protected Vector3 center => _center ? _center.position : transform.position;

        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawWireCube(center, _size);
        }

        public Vector3 checkLimits(Bounds current, Vector3 delta)
        {
            Bounds b = new Bounds(center, _size);
            Vector3 newPos = current.center += delta;
            newPos.x += Mathf.Max(0, b.min.x - current.min.x) + Mathf.Min(0, b.max.x - current.max.x);
            newPos.y += Mathf.Max(0, b.min.y - current.min.y) + Mathf.Min(0, b.max.y - current.max.y);
            newPos.z += Mathf.Max(0, b.min.z - current.min.z) + Mathf.Min(0, b.max.z - current.max.z);

            return newPos;
        }

        public Vector3 degreesFreedom(Bounds current)
        {
            return current.center;
        }
    }
}
