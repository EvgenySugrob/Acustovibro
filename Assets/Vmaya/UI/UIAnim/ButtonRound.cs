using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Vmaya.UI.UIAmin
{
    public class ButtonRound : MonoBehaviour
    {
        [SerializeField]
        private string _clipName;
        private Button _parent => GetComponentInParent<Button>();
        private Animator _animator => _parent ? _parent.GetComponent<Animator>() : null;
        protected bool isAnimation => _animator && (Time.timeScale > 0);


        private void Awake()
        {
            _parent.onClick.AddListener(onClick);
        }

        private void onClick()
        {
            transform.position = VMouse.mousePosition;
            if (isAnimation)
                _animator.Play(_clipName);
        }
    }
}
