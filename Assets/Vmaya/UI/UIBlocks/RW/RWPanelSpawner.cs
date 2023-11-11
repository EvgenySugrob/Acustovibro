using System;
using UnityEngine;

namespace Vmaya.UI.UIBlocks.RW
{
    public class RWPanelSpawner : MonoBehaviour
    {
        public UIBPanel[] Templates;

        public UIBPanel createPanel(Transform parent)
        {
            UIBPanel result = null;
            if (Templates.Length > 0)
            {
                UIBPanel tmpl = Templates[0];
                if (tmpl) result = createPanel(tmpl, parent);
            }
            else Debug.Log("Templates is empty");
            return result;
        }

        public UIBPanel createPanel(string prefabName, Transform parent)
        {
            UIBPanel tmpl = findTemplate(prefabName);
            UIBPanel result = null;
            if (tmpl) result = createPanel(tmpl, parent);
            else Debug.Log("Template " + prefabName  + " not found " + new Indent(this));
            return result;
        }

        public UIBPanel createPanel(UIBPanel prefab, Transform parent)
        {
            UIBPanel result = null;
            if (prefab)
            {
                result = Instantiate(prefab, parent);
                result.Origin = prefab;
            }
            return result;
        }

        public UIBPanel findTemplate(string prefabName)
        {
            foreach (UIBPanel tmpl in Templates)
                if (tmpl.name.Equals(prefabName)) return tmpl;

            return null;
        }

        internal UIBPanel createPanel(ComponentData data, Transform parent)
        {
            UIBPanel result = createPanel(data.prefabName, parent);
            if (result) result.setData(data);
            return result;
        }

        internal UIBPanel findOrCreatePanel(string prefabName, Transform transform)
        {
            UIBPanel panel = createPanel(prefabName, transform);
            if (!panel)
            {
                GameObject ga = GameObject.Find(prefabName);
                if (ga) panel = ga.GetComponent<UIBPanel>();
            }
            return panel;
        }

        public UIBPanel createRandom(Transform parent)
        {
            return createPanel(Templates[(int)Math.Round(UnityEngine.Random.Range(0f, 1f) * (Templates.Length - 1))].name, parent);
        }
    }
}
