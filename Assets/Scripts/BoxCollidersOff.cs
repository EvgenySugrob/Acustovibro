using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollidersOff : MonoBehaviour
{
    public MeshCollider colliderWhall;

    public void BoxCollidelWhallOff()
    {
        colliderWhall.enabled = false;
    }

    public void BoxColliderWhallOn()
    {
        colliderWhall.enabled = true;
    }
}
