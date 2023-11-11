using UnityEngine;
using Vmaya;
using Vmaya.Scene3D;

[RequireComponent(typeof(HitMouse))]
public class TargetToClick : BaseSmoothMovement
{
    private void Awake()
    {
        GetComponent<HitMouse>().onClick.AddListener(onClick);
    }

    private void Start()
    {
        if (target == null) target = CameraManager.getCurrent().GetComponent<DragMouseOrbitOrigin>().Target;
    }

    private void onClick(baseHitMouse hit)
    {
        Collider collider = GetComponent<Collider>();
        if (hitDetector.hits.Length > 0)
        {
            if (collider)
            {
                foreach (RaycastHit ahit in hitDetector.hits)
                    if (ahit.collider == collider)
                    {
                        beginTo(ahit.point);
                        break;
                    }
            } else beginTo(hitDetector.hits[hitDetector.hits.Length - 1].point);
        }
    }
}
