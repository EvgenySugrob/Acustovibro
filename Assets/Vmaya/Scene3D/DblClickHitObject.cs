using System.Collections;
using UnityEngine;
using static Vmaya.Scene3D.HitMouse;

namespace Vmaya.Scene3D
{
    public class DblClickHitObject : MonoBehaviour
    {
        [SerializeField]
        private float _waitTime;
        private bool _waitDbl;

        public HMEvent onDblClick;

        void Start()
        {
            hitDetector.instance.onClick.AddListener(onClick);
        }

        private void onClick(baseHitMouse hit)
        {
            if (_waitDbl) doDblClick(hit);
            else StartCoroutine(waitDblClick(hit));
        }

        private IEnumerator waitDblClick(baseHitMouse hit)
        {
            _waitDbl = true;
            yield return new WaitForSeconds(_waitTime);
            _waitDbl = false;
        }

        protected virtual void doDblClick(baseHitMouse hit)
        {
            onDblClick.Invoke(hit);
        }
    }
}
