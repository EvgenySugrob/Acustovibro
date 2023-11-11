using System.Collections.Generic;
using UnityEngine;
using Vmaya.UI;

namespace Vmaya.UI.UIBlocks
{
    public class UIBComponent : UIComponent
    {

        public UIBManager Manager => GetComponentInParent<UIBManager>();
        public UIBDropBox DropBox => Trans.parent ? Trans.parent.GetComponent<UIBDropBox>() : null;

        public enum DivideType { None, Horisontal, Vertical };
        public enum MagnetType { Top, Right, Bottom, Left, Full };

        protected List<Rect> getBlockRects(float w = 0, float h = 0)
        {
            Rect rect = Trans.rect;
            List<Rect> result = new List<Rect>();

            Vector2 minSize = Manager.defaultLimitSize;

            if (w == 0) w = Mathf.Max(rect.width * Manager.sizePercent.x, minSize.x);
            if (h == 0) h = Mathf.Max(rect.height * Manager.sizePercent.y, minSize.y);

            result.Add(new Rect(rect.xMin, rect.yMax - h, rect.width, h));
            result.Add(new Rect(rect.xMax - w, rect.yMin, w, rect.height));
            result.Add(new Rect(rect.xMin, rect.yMin, rect.width, h));
            result.Add(new Rect(rect.xMin, rect.yMin, w, rect.height));
            //result.Sort(cmdRect);

            return result;
        }

        protected virtual List<Rect> getBlockHitRects()
        {
            return getBlockRects(30, 30);
        }

        public virtual bool checkBlockRect(int index, Rect rect)
        {
            return true;
        }

        public static void setAnchor(RectTransform trans, MagnetType magnetType, Rect rect)
        {
            switch (magnetType)
            {
                case MagnetType.Top:
                    trans.anchorMin = new Vector2(0, 1);
                    trans.anchorMax = new Vector2(1, 1);
                    trans.sizeDelta = new Vector2(0, rect.height);
                    trans.localPosition = rect.position + rect.size * trans.pivot;
                    break;
                case MagnetType.Right:
                    trans.anchorMin = new Vector2(1, 0);
                    trans.anchorMax = new Vector2(1, 1);
                    trans.sizeDelta = new Vector2(rect.width, 0);
                    trans.localPosition = rect.position + rect.size * trans.pivot;
                    break;
                case MagnetType.Bottom:
                    trans.anchorMin = new Vector2(0, 0);
                    trans.anchorMax = new Vector2(1, 0);
                    trans.sizeDelta = new Vector2(0, rect.height);
                    trans.localPosition = rect.position + rect.size * trans.pivot;
                    break;
                case MagnetType.Left:
                    trans.anchorMin = new Vector2(0, 0);
                    trans.anchorMax = new Vector2(0, 1);
                    trans.sizeDelta = new Vector2(rect.width, 0);
                    trans.localPosition = rect.position + rect.size * trans.pivot;
                    break;
                default:
                    trans.anchorMin = new Vector2(0, 0);
                    trans.anchorMax = new Vector2(1, 1);
                    trans.sizeDelta = new Vector2(0, 0);
                    trans.localPosition = rect.position + rect.size * trans.pivot;
                    break;
            }
        }

        public static void setAnchorSpace(RectTransform trans, MagnetType magnetType, Rect rect)
        {
            trans.anchorMin = Vector2.zero;
            trans.anchorMax = new Vector2(1, 1);
            trans.sizeDelta = new Vector2(0, 0);
            switch (magnetType)
            {
                case MagnetType.Top:
                    trans.sizeDelta = new Vector2(0, -rect.height);
                    trans.localPosition = new Vector2(0, -rect.height * trans.pivot.y);
                    break;
                case MagnetType.Right:
                    trans.sizeDelta = new Vector2(-rect.width, 0);
                    trans.localPosition = new Vector2(-rect.width * trans.pivot.x, 0);
                    break;
                case MagnetType.Bottom:
                    trans.sizeDelta = new Vector2(0, -rect.height);
                    trans.localPosition = new Vector2(0, rect.height * trans.pivot.y);
                    break;
                case MagnetType.Left:
                    trans.sizeDelta = new Vector2(-rect.width, 0);
                    trans.localPosition = new Vector2(rect.width * trans.pivot.x, 0);
                    break;
            }
        }

        public virtual Vector3 limitSize()
        {   
            return Vector3.zero;
        }

        protected List<T> getChildren<T>()
        {
            List<T> result = new List<T>();
            for (int i = 0; i < Trans.childCount; i++)
            {
                T child = Trans.GetChild(i).GetComponent<T>();
                if (child != null) result.Add(child);
            }
            return result;
        }
    }
}