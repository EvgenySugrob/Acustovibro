using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vmaya.Command;

namespace Vmaya.Command.UI
{
    public class UIEditorSubMenu : MonoBehaviour
    {
        [SerializeField]
        private Button undoButton;
        [SerializeField]
        private Button redoButton;
        [SerializeField]
        private CommandManager commandManager;

        private string undoText;

        private void Awake()
        {
            undoButton.onClick.AddListener(onUndoButtonClick);
            redoButton.onClick.AddListener(onRedoButtonClick);
            undoText = undoButton.GetComponentInChildren<Text>().text;
            commandManager.onChange.AddListener(OnEnable);
        }

        private void onUndoButtonClick()
        {
            commandManager.undo();
        }

        private void onRedoButtonClick()
        {
            commandManager.redo();
        }

        private void OnEnable()
        {
            Text btext = undoButton.GetComponentInChildren<Text>();
            if (!commandManager.pointerBottom) btext.text = undoText + " " + commandManager.pointerNameCommand;
            else btext.text = undoText;

            undoButton.interactable = !commandManager.pointerBottom;
            redoButton.interactable = !commandManager.poinerUp;
        }
    }
}