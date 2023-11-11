using TMPro;
using UnityEngine;
using Vmaya.UI;

namespace Vmaya.UI.UIBlocks
{
    public class UIBExample : MonoBehaviour
    {
        [SerializeField]
        protected UIBPanel _panelTemplate1;
        public void createPanel1()
        {
            UIBPanel panel = Instantiate(_panelTemplate1, transform);
            panel.name = Utils.UniqueName(_panelTemplate1.name);
            panel.title.SetText("Панель " + GetComponentsInChildren<UIBPanel>(true).Length);
        }
    }
}