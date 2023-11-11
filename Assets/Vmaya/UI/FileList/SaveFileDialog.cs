using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Vmaya.Collections;
using Vmaya.Language;
using Vmaya.RW;
using Vmaya.UI.Collections;
using Vmaya.UI.Components;
using static Vmaya.Collections.BaseFSListSource;

namespace Vmaya.UI.FileList
{
    public class SaveFileDialog : BaseFileListDialog
    {
        [SerializeField]
        private InputField _fileName;

        public Component writer;
        private IWriter _writer;

        private void OnValidate()
        {
            checkWriter();
        }

        private void checkWriter()
        {
            _writer = writer ? writer.GetComponent<IWriter>() : null;
            writer = _writer as Component;
        }

        public void setWriter(IWriter a_writer)
        {
            _writer = a_writer;
            writer = _writer as Component;
        }

        protected override void Awake()
        {
            base.Awake();
            checkWriter();
            _fileName.onValueChanged.AddListener(onValueChanged);
        }

        private void onValueChanged(string text)
        {
            OkButton.interactable = !string.IsNullOrEmpty(text);
        }

        protected override void onOkButton()
        {
            if (FileListView.FileListSource.FindByName(_fileName.text) > -1)
                base.onOkButton();
            else doSelectFile(_fileName.text);
        }

        protected override void onSelectItem(string id)
        {
            base.onSelectItem(id);
            FileRecord fr = selectedFileRecord;
            if (OkButton.interactable = fr.type == FSType.File)
                _fileName.text = fr.name;
        }

        protected override void doSelectFile(string fullPathFileName)
        {
            string filePath = (FileListView.Source as FileListSource).relativePath + _fileName.text;

            if (!string.IsNullOrEmpty(filePath))
            {
                if (_writer != null)
                {
                    void saveProc()
                    {
                        _writer.Save(filePath);
                        hide();
                    }

                    if (File.Exists(filePath) && Question.instance)
                    {
                        hide();
                        Question.Show(Lang.instance.get("File {0} exists, overwrite this file?", filePath), (bool result) =>
                        {
                            if (result) saveProc();
                            else gameObject.SetActive(true);
                        });
                    }
                    else saveProc();
                }
                else Debug.Log("There is not writer");
            }
        }

        public void Save(Transform writerContainer)
        {
            IWriter a_writer = writerContainer.GetComponent<IWriter>();
            if (a_writer != null)
            {
                setWriter(a_writer);
                gameObject.SetActive(true);
            }
            else Debug.Log(writerContainer.name + " does not contain a writer");
        }
    }
}