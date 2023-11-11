using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vmaya.RW;

namespace Vmaya.UI.FileList.Example
{
    public class SaveFileExample : MonoBehaviour, IWriter
    {
        public void Save(string dataName)
        {
            Debug.Log("Save to " + dataName);
        }
    }
}

