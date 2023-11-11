using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Vmaya.UI;

namespace Vmaya.Command.UI
{
    public class HistoryList : CheckboxList
    {
        protected int headPoint => commandManager.pointer + 1;

        protected override void OnValidate()
        {
            if (Source == null) setSource(FindObjectOfType<CommandManager>());
        }
        protected CommandManager commandManager => Source as CommandManager;

        protected override void Awake()
        {
            base.Awake();
            if (Source == null) setSource(CommandManager.instance);
        }

        protected override void initSource()
        {
            base.initSource();
            if (commandManager) commandManager.onEndExecuteList.AddListener(updateItems);
        }

        protected override void releaseSource()
        {
            base.releaseSource();
            if (commandManager) commandManager.onEndExecuteList.RemoveListener(updateItems);
        }

        private void updateItems()
        {
            for (int i=0; i<toggleList.Count; i++)
            {
                HistoryItem item = toggleList[i].GetComponent<HistoryItem>();
                if (item) item.pointImage.gameObject.SetActive(headPoint == i);

                toggleList[i].interactable = !commandManager.IsListRun;
            }

            if ((headPoint >= 0) && (headPoint < toggleList.Count)) 
                scrollToItem(toggleList[headPoint].GetComponent<CheckboxItem>());
        }

        public override void updateList()
        {
            if ((toggleList == null) || (Source.getCount() != toggleList.Count))
                base.updateList();
            updateItems();
        }

        public void beginToSelect()
        {
            commandManager.beginTo(getSelectedIndex() - 1);
        }
    }
}
