using Vmaya.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vmaya.UI.Components;
using Vmaya.UI.Collections;

namespace Vmaya.UI.FileList
{
    public class SaveAsDialog : ModalWindow
    {
        [SerializeField]
        private ListView _dirList;

        [SerializeField]
        private InputField _fileName;

        [SerializeField]
        private Button _saveButton;

        private OnPreserver _currentPreserver;

        private void Awake()
        {
            _saveButton.onClick.AddListener(onSaveClick);
        }

        private void onSaveClick()
        {
            if (_currentPreserver != null) saveApply(_currentPreserver);
        }

        private void saveApply(OnPreserver preserver)
        {
            if (_fileName.text.Trim().Length > 0) {
                FileListSource fsl = _dirList.Source as FileListSource;

                if (fsl) {

                    if (fsl.relativePath.Length > 0)
                        preserver(fsl.relativePath + "/" + _fileName.text);
                }
            }
        }

        public void Show(OnPreserver preserver)
        {
            _currentPreserver = preserver;
            gameObject.SetActive(true);
        }

        override protected void OnDisable()
        {
            base.OnDisable();
            _currentPreserver = null;
        }
    }
}