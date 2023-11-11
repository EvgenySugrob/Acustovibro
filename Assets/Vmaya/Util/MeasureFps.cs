using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Vmaya.Util
{
    [RequireComponent(typeof(Text))]
    public class MeasureFps : MonoBehaviour
    {
        [SerializeField]
        private float UpdateTime = 1f;
        private Text dataLabel => GetComponent<Text>();

        private float _frameTimeAccum = 0;

        private void Start()
        {
            if (dataLabel)
                Vmaya.Utils.Periodical(this, outData, UpdateTime, 0);
        }

        private bool outData()
        {
            dataLabel.text = "FPS: " + Mathf.Round(1f / Time.deltaTime).ToString() + ", AVG: " +
                                                    Mathf.Round(1f / (_frameTimeAccum / Time.frameCount)).ToString();
            return false;
        }

        private void Update()
        {
            _frameTimeAccum += Time.deltaTime;
        }
    }
}
