using UnityEngine;


namespace Vmaya.Scene3D
{
    public class SwimTarget : MonoBehaviour
    {
        public Camera _camera;
        public float _movement;

        private float _incY;
        private float _incX;

        public void MoveUp(float inc)
        {
            _incY = inc;
        }
        public void MoveLeft(float inc)
        {
            _incX = inc;
        }

        private void Update()
        {
            float ditance = (_camera.transform.position - transform.position).magnitude;

            float k = ditance / 2f * (50 / _camera.focalLength);

            Vector3 f = _camera.transform.forward;
            Vector3 l = Quaternion.AngleAxis(90, Vector3.up) * f;

            Vector3 v = new Vector3(f.x * _incY * k, 0, f.z * _incY * k) +
                        new Vector3(l.x * _incX * k, 0, l.z * _incX * k);

            transform.position += v * Time.deltaTime * _movement;
        }
    }
}