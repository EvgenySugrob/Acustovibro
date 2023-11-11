using UnityEngine;
using Vmaya.Scene3D;

namespace Vmaya.RW
{
    public class RWTransform : RWEvents
    {
        private struct recData
        {
            public Quaternion rotation;
            public Vector3 position;
        }

        public void ReadData(dataRecord rec)
        {
            doReadData(rec);
        }

        override protected void doReadData(dataRecord rec)
        {
            recData data = JsonUtility.FromJson<recData>(rec.data);

            IPositioned p = GetComponent<IPositioned>();
            if (p != null) 
                p.setPosition(data.position, data.rotation);
            else {
                transform.position = data.position;
                transform.rotation = data.rotation;
            }
        }

        override protected string doWriteData()
        {
            recData data;
            data.rotation = transform.rotation;
            data.position = transform.position;
            return JsonUtility.ToJson(data);
        }
    }
}