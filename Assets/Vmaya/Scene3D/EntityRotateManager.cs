using System;
using System.Collections;
using UnityEngine;
using Vmaya.Util;
using MyBox;

namespace Vmaya.Scene3D
{
    public class EntityRotateManager : MonoBehaviour
    {
        public GameObject arrowAround;

        private IRotatable _current;

        public IRotatable Current => _current;

        public Component CurrentComponent => _current as Component;
        public bool isFocus => getIsFocus();

        [SerializeField]
        private float _timeoutBlur = 1;

        [SerializeField]
        private bool _scaleToCurrent = true;

        [ConditionalField("_scaleToCurrent")]
        [SerializeField] Limit _scaleLimits;

        public DragDrop3d ddCurrent => !Utils.IsDestroyed(CurrentComponent) ? CurrentComponent.GetComponent<DragDrop3d>() : null;

        private void Start()
        {
            if (arrowAround != null)
            {
                hitDetector.instance.onFocus.AddListener(onHitFocus);
                arrowAround.gameObject.SetActive(false);
            }
        }

        private void onDown(baseHitMouse hit)
        {
            if (!hit.transform.IsChildOf(arrowAround.transform))
                arrowAround.gameObject.SetActive(false);
        }

        protected void setCurrentEntity(IRotatable entity)
        {
            if (_current != entity)
            {
                if (ddCurrent)
                {
                    ddCurrent.onEndDrag.RemoveListener(onEndDrag);
                    ddCurrent.onBeginDrag.RemoveListener(onBeginDrag);
                }
                _current = !Utils.IsDestroyed(entity as Component) ? entity : null;
                if (CurrentComponent)
                {

                    if (_scaleToCurrent)
                    {
                        Bounds box = Utils.getRBounds(CurrentComponent.transform);
                        float scale = _scaleLimits.Clamp(Mathf.Max(box.size.x, Mathf.Max(box.size.y, box.size.z)));
                        arrowAround.transform.localScale = new Vector3(scale, scale, scale);
                    }

                    if (ddCurrent)
                    {
                        ddCurrent.onEndDrag.AddListener(onEndDrag);
                        ddCurrent.onBeginDrag.AddListener(onBeginDrag);
                    }

                    Quaternion q = Quaternion.LookRotation(_current.getAxis(), _current.getBaseVector());
                    arrowAround.transform.rotation = q;
                    arrowAround.transform.position = CurrentComponent.transform.position;

                    PulseRotate pr = arrowAround.GetComponent<PulseRotate>();
                    if (pr) pr.SetStartRotate(q);
                }

                arrowAround.gameObject.SetActive((_current != null) && !(ddCurrent && ddCurrent.freeze));
            }
        }

        private void onEndDrag(baseHitMouse hit)
        {
            if (isActiveManager())
            {
                arrowAround.transform.position = CurrentComponent.transform.position;
                arrowAround.gameObject.SetActive(true);
            }
        }

        private void onBeginDrag(baseHitMouse hit)
        {
            if (arrowAround != null) arrowAround.gameObject.SetActive(false);
        }

        protected virtual bool isActiveManager()
        {
            return !(VMouse.GetMouseButton(0) || VMouse.GetMouseButton(2));
        }

        protected virtual void onHitFocus(baseHitMouse hit)
        {
            if (isActiveManager() && !isFocus)
            {
                IRotatable rotatable = hit.GetComponent<IRotatable>();
                if ((rotatable != null) && rotatable.rotateAvailable()) 
                    setCurrentEntity(rotatable);
            }
        }

        private bool getIsFocus()
        {
            baseHitMouse focus = hitDetector.Focus;
            if (focus)
            {
                return (CurrentComponent && focus.transform.IsChildOf(CurrentComponent.transform)) || 
                            focus.transform.IsChildOf(transform);
            }
            return false;
        }

        private bool _waitBlur;
        private void BeginBlur()
        {
            if (!_waitBlur && arrowAround && !BaseDragDrop.isDragging) {
                _waitBlur = true;
                StartCoroutine(doUnfocus());
            }
        }

        IEnumerator doUnfocus()
        {
            yield return new WaitForSeconds(_timeoutBlur);
            _waitBlur = false;
            if (!isFocus) {
                arrowAround.gameObject.SetActive(false);
                setCurrentEntity(null);
            }
        }

        private void Update()
        {
            if (!isFocus)
                BeginBlur();
        }
    }
}