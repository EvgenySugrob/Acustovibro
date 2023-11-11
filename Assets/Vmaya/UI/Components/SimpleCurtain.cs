using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Vmaya.UI.Components
{
    [RequireComponent(typeof(Image))]
    public class SimpleCurtain : UIComponent, ICurtain
    {
        private Image _image => GetComponent<Image>();
        public void Hide()
        {
            _image.enabled = false;
        }

        public void Show()
        {
            _image.enabled = true;
        }
    }
}
