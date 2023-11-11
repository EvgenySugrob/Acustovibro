using System;
using UnityEngine;
using Vmaya.Util;

namespace Vmaya.Scene3D
{
    public class GotoHitObject : MonoBehaviour
    {
        public float distance;
        public FreeFlyCamera flyCamera;
        public void cameraTo(baseHitMouse hit)
        {
            if (checkHit(hit))
                cameraTo(hit.transform);
        }

        protected virtual bool checkHit(baseHitMouse hit)
        {
            return true;
        }

        public void cameraTo(Transform trans)
        {
            Vector3 directAbs = trans.position - flyCamera.transform.position;
            Vector3 direcrtNorm = directAbs.normalized;
            Vector3 pos = flyCamera.transform.position;

            if (directAbs.magnitude > distance)
                pos = trans.position - direcrtNorm * distance;

            flyCamera.setPosition(pos, Quaternion.LookRotation(direcrtNorm));
        }
    }
}
