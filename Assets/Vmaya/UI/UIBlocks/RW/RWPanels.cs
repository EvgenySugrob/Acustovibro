using Vmaya.RW;
using UnityEngine;

namespace Vmaya.UI.UIBlocks.RW
{
    public class RWPanels : RWEvents
    {
        [SerializeField]
        private RWPanelSpawner _panelSpawner;
        protected override string doWriteData()
        {
            ContentList data = new ContentList();
            for (int i=0; i<transform.childCount; i++)
            {
                UIBPanel panel = transform.GetChild(i).GetComponent<UIBPanel>();
                if (panel) data.list.Add(panel.getData());
            }
            return JsonUtility.ToJson(data);
        }

        protected override void doReadData(dataRecord rec)
        {
            ContentList data = JsonUtility.FromJson<ContentList>(rec.data);
            foreach (ComponentData item in data.list)
                _panelSpawner.createPanel(item, transform);
        }
    }
}
