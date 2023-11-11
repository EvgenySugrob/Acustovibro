using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Vmaya.UI.Components
{
    public class CurtainBlur : UIComponent
    {
        [SerializeField]
        private RawImage _background;

        [SerializeField]
        private float _blur = 4;
        private float _prevBlur;

        private RenderTexture rt;
        private Vector2 _prevSize;

        private void OnValidate()
        {
            if (_background == null) _background = GetComponent<RawImage>();
        }

        protected void updateBackground()
        {
            Vector2 size = new Vector2(Screen.width, Screen.height);
            rt = new RenderTexture((int)size.x, (int)size.y, 24);
            ScreenCapture.CaptureScreenshotIntoRenderTexture(rt);

            _background.texture = rt;
            _prevSize = size;
        }

        protected virtual bool checkChange()
        {
            return _prevBlur != _blur;
        }

        private void updateBlur()
        {
            _background.material.SetFloat("_Size", _blur);
            _prevBlur = _blur;
        }

        private void Update()
        {

            Vector2 curSize = new Vector2(Screen.width, Screen.height);
            if (!curSize.Equals(_prevSize)) updateBackground();

            if (checkChange()) updateBlur();
        }
    }
}
