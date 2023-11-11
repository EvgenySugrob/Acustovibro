using UnityEngine;

namespace Vmaya.Scene3D
{
    namespace Vmaya
    {
        public class FreezerGo : MonoBehaviour, IFreezer
        {
            private void Awake()
            {
                FreezerList.instance.Add(this);
            }

            private void OnDestroy()
            {
                FreezerList.instance.Remove(this);
            }

            public bool Freeze()
            {
                return gameObject.activeSelf;
            }
        }
    }
}