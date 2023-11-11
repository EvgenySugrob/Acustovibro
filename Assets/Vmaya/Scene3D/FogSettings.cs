using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vmaya.Scene3D
{
    public class FogSettings : MonoBehaviour
    {
        public bool Fog;
        [SerializeField]
        private Color FogColor;
        [SerializeField]
        [Range(0, 1)]
        private float FogDensity;

        private void Start()
        {
            Apply();
        }

        public void Apply()
        {
            if (RenderSettings.fog = Fog)
            {
                RenderSettings.fogColor = FogColor;
                RenderSettings.fogDensity = FogDensity;
            }
        }
    }
}
