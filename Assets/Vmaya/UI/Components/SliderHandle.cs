using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Vmaya.UI.Controls
{
    public class SliderHandle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Graphic _shadow;
        [SerializeField]
        private int accuracy = 0;
        private Slider _slider => GetComponentInParent<Slider>();

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_shadow) _shadow.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_shadow) _shadow.gameObject.SetActive(false);
        }

        private void Awake()
        {
            _slider.onValueChanged.AddListener(onValueChanged);
            updateValue();
        }

        private void onValueChanged(float value)
        {
            updateValue();
        }

        private void updateValue()
        {
            Vmaya.Utils.setText(this, Vmaya.Utils.round2(_slider.value, accuracy).ToString());
        }
    }
}
