using Vmaya.RW;
using UnityEngine;
using static Vmaya.UI.UIBlocks.UIBComponent;

namespace Vmaya.UI.UIBlocks.RW
{
    [RequireComponent(typeof(UIBDropBox))]
    public class RWBlock : RWEvents
    {
        [SerializeField]
        private RWPanelSpawner _panelSpawner;
        private UIBDropBox dropBox => GetComponent<UIBDropBox>();

        protected override void doReadData(dataRecord rec)
        {
            parseData(rec.data);
        }

        protected virtual void parseData(string dataJson)
        {
            if (!string.IsNullOrEmpty(dataJson))
            {
                if (_panelSpawner)
                {

                    ComponentData pdata = JsonUtility.FromJson<ComponentData>(dataJson);

                    if (!string.IsNullOrEmpty(pdata.prefabName))
                    {
                        UIBPanel panel = _panelSpawner.findOrCreatePanel(pdata.prefabName, transform);

                        if (panel)
                        {
                            panel.fullSpace();
                            if (panel.resizePanel) panel.resizePanel.enabled = false;
                            panel.setData(pdata);
                            return;
                        }
                    }
                }

                DropBoxData data = JsonUtility.FromJson<DropBoxData>(dataJson);

                if (data.divideType != DivideType.None)
                {
                    dropBox.Div(data.magnetType, 
                        data.divideType == DivideType.Horisontal ? data.edgeSize : 0,
                        data.divideType == DivideType.Vertical ? data.edgeSize : 0);

                    //dropBox.setDivOffset(data.separate);
                    dropBox.EdgeBox.GetComponent<RWBlock>().parseData(data.edgeBoxJson);
                    dropBox.OtherBox.GetComponent<RWBlock>().parseData(data.otherBoxJson);
                }
            }
        }

        protected override string doWriteData()
        {
            if (dropBox.Divided != DivideType.None)
            {
                DropBoxData data = new DropBoxData();
                data.divideType     = dropBox.Divided;
                data.magnetType     = dropBox.magnetType;
                //data.separate       = dropBox.DivOffset;
                data.edgeSize       = dropBox.vtof(dropBox.EdgeBox.Trans.rect.size);
                data.edgeBoxJson    = dropBox.EdgeBox.GetComponent<RWBlock>().doWriteData();
                data.otherBoxJson   = dropBox.OtherBox.GetComponent<RWBlock>().doWriteData();

                return JsonUtility.ToJson(data);
            }
            else
            {
                UIBPanel panel = dropBox.getPanel();
                if (panel)
                    return JsonUtility.ToJson(panel.getData());
            }
            return "";
        }
    }
}
