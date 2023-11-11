using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MicroPosition : MonoBehaviour
{
    [SerializeField]
    private float Accuracy = 0.1f;

    [SerializeField]
    private Vector3 Position;

    private void Start()
    {
        enabled = Application.isEditor;
    }

    private void OnEnable()
    {   
        Position = transform.position / Accuracy;
    }

    private void Update()
    {
        if (Accuracy > 0)
        {
            if (!transform.position.Equals(Position * Accuracy))
            {
                transform.position = Position * Accuracy;
            }
            Position = transform.position / Accuracy;
        }
    }
}
