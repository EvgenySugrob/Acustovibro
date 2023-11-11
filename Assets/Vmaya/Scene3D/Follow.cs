using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour
{
    [SerializeField]
    public float smoothLook = 0.9f;
    public float distance = 1;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private bool backHitTest;

    public Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAtAround = true;

    private Vector3 mouseOffset = Vector3.zero;
    private float s_distance;

    private void Start()
    {
        s_distance = distance;
    }

    private void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (target == null)
        {
            Debug.LogWarning("Missing target ref !", this);
            return;
        }

        s_distance += (distance - s_distance) * smoothLook;
        if (backHitTest)
        {
            RaycastHit hit;
            if (Physics.Linecast(target.position, target.position + (transform.position - target.position) * 8f, out hit))
            {
                s_distance = Mathf.Min(hit.distance, distance);
            }                
        }

        Vector3 offset = Vector3.Normalize(offsetPosition + mouseOffset) * s_distance;

        if (offsetPositionSpace == Space.Self)
            transform.position = target.TransformPoint(offset);
        else transform.position = target.position + offset;

        // compute rotation
        if (lookAtAround) transform.LookAt(target);
        //else transform.rotation = target.rotation;
    }
}