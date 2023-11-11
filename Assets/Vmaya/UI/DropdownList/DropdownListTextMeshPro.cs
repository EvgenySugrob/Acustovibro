using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Vmaya.UI.DropdownList
{
    public class DropdownListTextMeshPro : TMP_Dropdown
    {
        protected override GameObject CreateDropdownList(GameObject template)
        {
            DropdownListFrame dlf = template.GetComponent<DropdownListFrame>();
            return (GameObject)Instantiate(template, dlf ? dlf.Frame : transform);
        }
    }
}
