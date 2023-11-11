using UnityEngine;
using UnityEditor;
using Vmaya.UI.Menu;

namespace Vmaya.Drawable
{
    [CustomEditor(typeof(MainMenu))]
    public class MainMenuTool : Editor
    {
        [UnityEditor.MenuItem("GameObject/UI/MainMenu/TopBar %#r")]
        public static void init()
        {
            RectTransform top = Selection.activeTransform.GetComponent<RectTransform>();

            if (top)
            {
                Object tmpl = AssetDatabase.LoadAssetAtPath("Assets/Vmaya/UI/Menu/Prefabs/TopBar.prefab", typeof(GameObject));

                if (tmpl)
                {
                    GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(tmpl, top);
                    Selection.activeObject = go;
                }
                else Debug.Log("Prefab TopBar.prefab not found");
            }
            else Debug.Log("Only for UI layers");
        }
    }
}
