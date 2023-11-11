using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Vmaya.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Image _shadowImage;
        [SerializeField]
        private string _enterClip;
        [SerializeField]
        private string _exitClip;

        [SerializeField] InventoryRotationObject inventoryRotation;
        [SerializeField] bool _isRotationBt;

        private Button _button => GetComponent<Button>();
        private Animator _animator => GetComponent<Animator>();
        protected bool isAnimation => _animator && (Time.timeScale > 0);

        void Start()
        {
            _shadowImage.gameObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_button.interactable)
                _shadowImage.gameObject.SetActive(true);

            if (_animator && !string.IsNullOrEmpty(_enterClip) && isAnimation)
                _animator.Play(_enterClip);

            if(_isRotationBt)
                inventoryRotation.RotationObject(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _shadowImage.gameObject.SetActive(false);

            if (_animator && !string.IsNullOrEmpty(_exitClip) && isAnimation)
                _animator.Play(_exitClip);

            if (_isRotationBt)
                inventoryRotation.RotationObject(false);
        }
    }
}
