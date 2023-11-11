using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vmaya.RW;

namespace Vmaya.UI.FileList.Example
{
    public class OpeningFileExample : MonoBehaviour, IOpener
    {
        public void Open(string dataName)
        {
            Debug.Log(dataName);
        }
    }
}
