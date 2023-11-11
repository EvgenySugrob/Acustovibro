using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Vmaya.Command;

namespace Vmaya.Command
{
    [RequireComponent(typeof(CommandManager))]
    public class UICommandManager : MonoBehaviour
    {
        private Key _UKey, _RKey;
        private bool _isCtrl;
        private CommandManager _cm;

        [SerializeField]
        private Text _undoMenuCaption;

        [SerializeField]
        private Text _redoMenuCaption;

        [SerializeField]
        private Text _commandPoint;

        private void Awake()
        {
            if (Application.isEditor)
            {
                _UKey = Key.U;// "u";
                _RKey = Key.R;//"r";
                _isCtrl = false;

                if (_undoMenuCaption) _undoMenuCaption.text = "U";
                if (_redoMenuCaption) _redoMenuCaption.text = "R";
            }
            else
            {
                _UKey = Key.Z;// "z";
                _RKey = Key.Y;//"y";
                _isCtrl = true;

                if (_undoMenuCaption) _undoMenuCaption.text = "Ctrl+" + "Z";
                if (_redoMenuCaption) _redoMenuCaption.text = "Ctrl+" + "Y";
            }

            _cm = GetComponent<CommandManager>();

            if (_commandPoint) _cm.onChange.AddListener(doChange);
        }

        private void doChange()
        {
            _commandPoint.text = CommandManager.instance.pointerNameCommand;
        }

        private void Update()
        {
            if (!_isCtrl || VKeyboard.GetKey(Key.LeftCtrl))
            {
                if (VKeyboard.GetKeyDown(_UKey)) _cm.undo();
                else if (VKeyboard.GetKeyDown(_RKey)) _cm.redo();
            }
        }
    }
}