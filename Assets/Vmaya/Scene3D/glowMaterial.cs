using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vmaya.Scene3D
{
    public class glowMaterial : MonoBehaviour
    {

        public float hitValue = 0.5f;
        public Color color = Color.white;

        [SerializeField]
        private bool _emission = true;

        private List<Color> _lcolors;
        private List<bool> _lemm;
        private List<Material> _materials;

        private float maxShow;
        private Color _color = Color.red;
        private float _inx;
        private int _cycles;

        //  public static string nameColorComponent = "_EmissionColor";
        //  public static string nameColorKeyword   = "_EMISSION";
        protected static string nameEmissionComponent = "_EmissionColor";
        protected static string nameColorComponent = "_Color";
        protected static string nameColorKeyword = "_EMISSION";
        public static Color DefaultColor = Color.white;

        [System.Serializable]
        public struct GlowState
        {
            public float maxShow;
            public Color color;
        }

        public bool isGlow => maxShow > 0;

        public GlowState state
        {
            get
            {
                GlowState result;
                result.maxShow = maxShow;
                result.color = _color;
                return result;
            }

            set
            {
                glowBegin(value.maxShow, value.color);
            }
        }

        public void initColors()
        {
            _lcolors = new List<Color>();
            _lemm = new List<bool>();
            _materials = new List<Material>();
            if (Application.isPlaying)
            {
                storeColors(transform);
                if (!isGlowAvailable())
                    Debug.Log("Glow cannot be used");
            }
        }

        protected bool isInitialize
        {
            get
            {
                return _lcolors != null;
            }
        }

        protected void clearColors()
        {
            if (isInitialize)
            {
                _lcolors.Clear();
                _lemm.Clear();
                _materials.Clear();
            }
        }

        public void glowBegin()
        {
            glowBegin(hitValue, Color.Equals(color, Color.black) ? DefaultColor : color);
        }

        public void glowBegin(float value = 0)
        {
            glowBegin(value, Color.Equals(color, Color.black) ? DefaultColor : color);
        }

        public void glowBegin(float value, Color a_color)
        {
            if (maxShow > 0)
            {
                SetGlow(transform, 0);
                clearColors();
            }

            maxShow = value;
            _color = a_color;
            _inx = 0;
            _cycles = 0;

            if (value == 0)
            {
                SetGlow(transform, 0);
                clearColors();
            }
            else initColors();
        }

        public void glowBegin(float value, Color a_color, int a_cycles)
        {
            glowBegin(value, a_color);
            _cycles = a_cycles;
        }

        public bool isRun
        {
            get
            {
                return maxShow > 0;
            }
        }

        virtual protected void doUpdate()
        {
            if (isRun)
            {
                _inx += Time.deltaTime * 8;
                SetGlow(transform, (Mathf.Sin(_inx) + 1) / 2 * 1 * maxShow);

                if (_cycles > 0)
                {
                    int _ccs = (int)Math.Floor(_inx % (Math.PI * 2));
                    if (_ccs >= _cycles) glowBegin(0, _color);
                }
            }
        }

        void Update()
        {
            doUpdate();
        }

        public void SetGlow(Transform obj, float value)
        {
            if (_materials != null)
            {
                for (int i = 0; i < _materials.Count; i++)
                {
                    Material mat = _materials[i];
                    Color c = _lcolors[i];
                    c = Color.Lerp(c, _color, value);

                    if (value == 0)
                    {
                        if (!_lemm[i]) mat.DisableKeyword(nameColorKeyword);
                    }
                    else mat.EnableKeyword(nameColorKeyword);

                    mat.SetColor(_emission ? nameEmissionComponent : nameColorComponent, c);
                }
            }
        }

        public bool isGlowAvailable()
        {
            return _materials.Count > 0;
        }

        private void pushMaterial(Material mat)
        {
            if (mat.HasProperty(_emission ? nameEmissionComponent : nameColorComponent))
            {
                _lcolors.Add(mat.GetColor(_emission ? nameEmissionComponent : nameColorComponent));
                _lemm.Add(mat.IsKeywordEnabled(nameColorKeyword));
                _materials.Add(mat);
            }
        }

        private void storeColors(Transform obj)
        {
            MeshRenderer render = obj.GetComponent<MeshRenderer>();
            if (render)
            {
                foreach (Material mat in render.materials)
                    pushMaterial(mat);
            }

            LineRenderer line_render = obj.GetComponent<LineRenderer>();
            if (line_render) pushMaterial(line_render.material);

            for (int i = 0; i < obj.childCount; i++)
            {
                if (obj.GetChild(i).GetComponent<glowMaterial>() == null)
                    storeColors(obj.GetChild(i));
            }
        }

        internal void toggle(bool value)
        {
            glowBegin(value ? hitValue : 0);
        }
    }
}