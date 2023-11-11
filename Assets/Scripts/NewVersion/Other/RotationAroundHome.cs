using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAroundHome : MonoBehaviour
{
    [SerializeField] GameObject cameraRotation;
    [SerializeField] Transform rotationPoint;
    private Vector3 startPosition;
    private Quaternion startRotation;

    [SerializeField] Vector3 angleRotation = new Vector3(0, 15f, 0);

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void Update()
    {
        rotationPoint.Rotate(angleRotation * Time.deltaTime);
    }

    public void DisableScript()
    {
        gameObject.SetActive(false);
        enabled = false;
    }
}
