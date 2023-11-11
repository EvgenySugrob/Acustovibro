using System;
using System.Collections.Generic;
using UnityEngine;
namespace Vmaya.Scene3D
{
    public class FreezerList
    {
        private List<IFreezer> items;

        private static FreezerList _instance;

        internal void Add(IFreezer freezerGo)
        {
            items.Add(freezerGo);
        }

        internal void Remove(IFreezer freezerGo)
        {
            items.Remove(freezerGo);
        }

        public FreezerList()
        {
            items = new List<IFreezer>();
        }

        public static FreezerList instance => getInstance();

        private static FreezerList getInstance()
        {
            if (_instance == null)
                _instance = new FreezerList();

            return _instance;
        }

        public bool isFreeze()
        {
            for (int i = 0; i < items.Count; i++)
                if (!Utils.IsDestroyed(items[i] as Component) && items[i].Freeze())
                    return true;

            return false;
        }
    }
}