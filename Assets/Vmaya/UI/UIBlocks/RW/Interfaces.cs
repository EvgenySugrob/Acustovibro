using System.Collections.Generic;
using UnityEngine;

namespace Vmaya.UI.UIBlocks.RW
{

    [System.Serializable]
    public class DropBoxData
    {
        public UIBComponent.DivideType divideType;
        public UIBComponent.MagnetType magnetType;
        //public float separate;
        public string edgeBoxJson;
        public string otherBoxJson;
        public float edgeSize;
    }

    [System.Serializable]
    public class ComponentData
    {
        public string prefabName;
        public string jsonData;
    }

    [System.Serializable]
    public class ContentList
    {
        public List<ComponentData> list;
        public ContentList()
        {
            list = new List<ComponentData>();
        }
    }

    [System.Serializable]
    public class PanelData: ContentList
    {
        public Rect rect;

        public PanelData(Rect rect)
        {
            this.rect = rect;
        }
    }
}