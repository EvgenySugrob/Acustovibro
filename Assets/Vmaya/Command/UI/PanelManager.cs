using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vmaya.Language;
using Vmaya.UI.Components;

namespace Vmaya.Command.UI
{
    public class PanelManager : MonoBehaviour
    {
        public HistoryList listView;
        public Button cleanButton;
        public Button goButton;
        public Button pauseButton;
        public Button stepBackButton;
        public Button stepForwardButton;
        public Slider speed;

        protected CommandManager manager => CommandManager.instance;

        private void Start()
        {
            refreshButtons();

            speed.value = speed.maxValue - manager.waitSec;
            manager.onAfterChange(onAfterChange);
            cleanButton.onClick.AddListener(cleanButtonClick);
            goButton.onClick.AddListener(goButtonClick);
            pauseButton.onClick.AddListener(pauseButtonClick);
            stepBackButton.onClick.AddListener(stepBackButtonClick);
            stepForwardButton.onClick.AddListener(stepForwardButtonClick);
            listView.onSelect.AddListener(refreshButtons);
            speed.onValueChanged.AddListener(onChangeSpeed);

            manager.onStartExecuteList.AddListener(onChangePlayList);
            manager.onEndExecuteList.AddListener(onChangePlayList);
        }

        private void onChangeSpeed(float value)
        {
            manager.waitSec = speed.maxValue - value;
        }

        private void onChangePlayList()
        {
            refreshButtons();
        }

        private void pauseButtonClick()
        {
            manager.stopExecuteList();
        }

        private void stepForwardButtonClick()
        {
            manager.redo();
        }

        private void stepBackButtonClick()
        {
            manager.undo();
        }

        private void goButtonClick()
        {
            listView.beginToSelect();
        }

        private void cleanButtonClick()
        {
            Question.Show(Lang.instance["Clear history?"], (bool result) =>
            {
                if (result) manager.clearAll();
            });
        }

        private void onAfterChange()
        {
            refreshButtons();
        }

        private void refreshButtons()
        {
            bool noEmpty = manager.getCount() > 1;

            cleanButton.interactable = noEmpty;

            goButton.interactable = noEmpty && 
                    (listView.getSelectedIndex() > -1) && (manager.pointer + 1 != listView.getSelectedIndex());

            goButton.gameObject.SetActive(!manager.IsListRun);
            pauseButton.gameObject.SetActive(manager.IsListRun);

            stepBackButton.interactable = noEmpty && (manager.pointer >= 0);
            stepForwardButton.interactable = noEmpty && (manager.pointer < manager.getCount() - 2);
        }
    }
}
