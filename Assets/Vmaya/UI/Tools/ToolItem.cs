using Vmaya.Command;
using UnityEngine;
using UnityEngine.Events;

namespace Vmaya.UI.Tools
{
    [System.Serializable]
    public class ToolItem 
    {
        public string title;
        public Sprite sprite;
        [HideInInspector]
        public int number;

        public ToolItem(string name, Sprite a_sprite)
        {
            this.title = name;
            this.sprite = a_sprite;
        }
        public virtual void action(bool value) { }
    }

    [System.Serializable]
    public class ToolModeItem: ToolItem
    {
        public string mode;

        public ToolModeItem(string name, string mode, Sprite a_sprite): base (name, a_sprite)
        {
            this.mode = mode;
        }

        public override void action(bool value) {
            if (value)
            {
                Vmaya.Utils.PendingCondition(EditorMode.instance, () => {
                    return CommandManager.instance && !CommandManager.instance.isCommandExecute;
                }, () =>
                {
                    EditorMode.instance.mode = mode;
                });
            }
            else if (EditorMode.instance.EqualsMode(mode))
                EditorMode.instance.mode = null;
        }
    }

    [System.Serializable]
    public class ToolActionItem : ToolItem
    {
        [System.Serializable]
        public class onAction : UnityEvent<bool> { };
        public onAction externalAction;
        public ToolActionItem(string name, onAction a_externalAction, Sprite a_sprite) : base(name, a_sprite)
        {
            externalAction = a_externalAction;
        }

        public override void action(bool value)
        {
            if (externalAction != null) externalAction.Invoke(value);
        }
    }
}