using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
    public float Speed = 20;
    public Vector3 axis = Vector3.up;

    [HideInInspector]
    public float angle;

    private Quaternion _startRotate;
    private void Start()
    {
        _startRotate = transform.localRotation;
    }

    void FixedUpdate () {

        angle += Speed * Time.deltaTime;
        transform.localRotation = Quaternion.AngleAxis(angle, axis) * _startRotate;
	}
}
