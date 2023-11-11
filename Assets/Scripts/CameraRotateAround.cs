using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraRotateAround : MonoBehaviour
{
    public GameObject pauseMenu;
    [SerializeField] InventoryReplaceItem items;
    public Transform target;
    public Vector3 offset;
    public float sensitivity = 3;
    public float limit = 80;
    public float zoom = 0.25f;
    public float zoomMax = 10; 
    public float zoomMin = 3;
    private float limitMin = 0;
    private float X, Y;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Confined;
        limit = Mathf.Abs(limit);
        if (limit > 90) 
            limit = 90;
        offset = new Vector3(offset.x, offset.y, -Mathf.Abs(zoomMax) / 2);
        transform.position = target.position + offset;
    }

    void Update()
    {
        if (!items.IsSystemActive())
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0) offset.z += zoom;
                else if (Input.GetAxis("Mouse ScrollWheel") < 0) offset.z -= zoom;
                offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));

                if (Input.GetMouseButton(1))
                {
                    X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
                    Y += Input.GetAxis("Mouse Y") * sensitivity;
                    Y = Mathf.Clamp(Y, limitMin, limit);
                    transform.localEulerAngles = new Vector3(Y, X, 0);
                }
                transform.position = transform.localRotation * offset + target.position;
            }

        }
    }

    public void ChangeZoomMinMaxValue(float max,float min)
    {
        zoomMax = max;
        zoomMin = min;
    }
}
