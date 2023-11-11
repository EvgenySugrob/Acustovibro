using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vmaya.Scene3D
{
    public class TargetFrameRate : MonoBehaviour
    {
        [Header("The restriction only works in the editor")]
        [SerializeField]
        private int _targetFrameRate = 100;
        private int _lastTragetFrameRate;

#if (UNITY_EDITOR)
        private void Update()
        {
            if (_targetFrameRate != _lastTragetFrameRate)
                Application.targetFrameRate = _lastTragetFrameRate = _targetFrameRate;
        }
#endif
    }
}
