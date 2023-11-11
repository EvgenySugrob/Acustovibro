using UnityEngine;

namespace Vmaya.Util
{
    public class PulseRotate : MonoBehaviour
    {

        public float Period = 20;
        public float MaxAngle = 20;
        public Vector3 localAxis = Vector3.up;
        private float _index;

        private Quaternion _startRotate;

        private void OnEnable()
        {
            SetStartRotate(transform.localRotation);
            _index = 0;
        }

        public void SetStartRotate(Quaternion a_startRotate)
        {
            _startRotate = a_startRotate;
            updateRotation();
        }

        private void updateRotation()
        {
            transform.localRotation = _startRotate * Quaternion.AngleAxis(Mathf.Sin(_index) * MaxAngle, localAxis);
        }

        void Update()
        {
            _index += Period * Time.deltaTime;
            updateRotation();
        }
    }
}
