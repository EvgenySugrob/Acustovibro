using UnityEngine;


namespace Vmaya.Util
{
    public class RotateCycle : MonoBehaviour
    {
        [SerializeField]
        private Vector3 axis;

        [SerializeField]
        private float range;

        [SerializeField]
        protected EasingFunction.Ease _moveEase;

        [SerializeField]
        protected float _speed;

        private float _rollIndex = 0;
        private float _dir = 1;

        private void Update()
        {
            _rollIndex += _speed * _dir * Time.deltaTime;
            if ((_rollIndex > 1) || (_rollIndex < 0))
            {
                _dir = -_dir;
                _rollIndex += _speed * _dir * Time.deltaTime;
            }

            transform.localRotation = Quaternion.AngleAxis(EasingFunction.GetEasingFunction(_moveEase)(-0.5f, 0.5f, _rollIndex) * range, axis);
        }
    }
}