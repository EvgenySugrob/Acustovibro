using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Vmaya.UI
{
    public class Prompt : MonoBehaviour
    {
        [System.Serializable]
        public delegate void PromptCallback(string value);

        [SerializeField]
        private TMP_InputField _inputField;

        [SerializeField]
        private TMP_Text _description;

        private PromptCallback _callback;

        public static Prompt instance => FindObjectOfType<Prompt>(true);

        public static void Show(string descrition, PromptCallback callback)
        {
            if (instance) instance.show(descrition, null, callback);
        }

        public static void Show(string descrition, string value, PromptCallback callback)
        {
            if (instance) instance.show(descrition, value, callback); 
        }

        private void show(string descrition, string value, PromptCallback callback)
        {
            _inputField.text = value;
            _callback = callback;
            _description.text = descrition;
            gameObject.SetActive(true);
        }

        public void Ok()
        {
            _callback(_inputField.text);
        }
    }
}
