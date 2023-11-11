using System;
using UnityEngine;

namespace Vmaya.UI.UITabs
{
    public class RWContentSpawner : MonoBehaviour
    {
        public TabContent[] Templates;

        public TabContent createContent(string prefabName, Transform parent)
        {
            TabContent tmpl = findTemplate(prefabName);
            TabContent result = null;
            if (tmpl)
            {
                result = Instantiate(tmpl, parent);
                result.Origin = tmpl;
            }
            else Debug.Log("Content " + prefabName  + " not found");
            return result;
        }

        public TabContent findTemplate(string prefabName)
        {
            foreach (TabContent tmpl in Templates)
                if (tmpl.name.Equals(prefabName)) return tmpl;

            return null;
        }

        public void createRandom(Transform parent)
        {
            createContent(Templates[(int)Math.Round(UnityEngine.Random.Range(0f, 1f) * (Templates.Length - 1))].name, parent);
        }
    }
}
