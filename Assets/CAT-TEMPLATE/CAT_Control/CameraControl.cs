using TMPro;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private GameObject pointForSpawn;
    [SerializeField] private float mouseSpeed, zoomSpeed;
    [SerializeField] private TMP_Text mettersToDeviceField;
    [SerializeField] private float maxDistance, minDistance;

    private float mouseXCoordinate;
    private float mouseYCoordinate;
    private float cameraZCoordinate;
    private Vector2 savedMousePosition;

    void Update()
    {
        cameraZCoordinate = transform.position.z;
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            SaveCoordinate();
            Zoom();
            RotateObject();
            LoadCoordinate();
        }
        mettersToDeviceField.text = Vector3.Distance(transform.position, pointForSpawn.transform.position).ToString("f1") + " ì"; //Calculation distance to device

    }
    private void RotateObject()
    {
        if (Input.GetMouseButton(0)) //holding the right mouse button is rotate object (camera): left/right, up/down
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            mouseXCoordinate = Input.GetAxis("Mouse X") * mouseSpeed;
            mouseYCoordinate = Input.GetAxis("Mouse Y") * mouseSpeed;
            pointForSpawn.transform.Rotate(Vector3.left, -mouseYCoordinate, Space.World);
            pointForSpawn.transform.Rotate(Vector3.up, -mouseXCoordinate, Space.World);
        }
    }
    private void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) //zoom camera
        {
            cameraZCoordinate = Mathf.Clamp(cameraZCoordinate + Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, maxDistance, minDistance);
            transform.position = new Vector3(transform.position.x, transform.position.y, cameraZCoordinate);
        }
    }
    private void SaveCoordinate()
    {
        if (Input.GetMouseButtonDown(0)) //Save coordinates of cursor
        {
            savedMousePosition = Input.mousePosition;
        }
    }
    private void LoadCoordinate()
    {
        if (Input.GetMouseButtonUp(0)) //Move cursor to saved coordinates
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Mouse.current.WarpCursorPosition(savedMousePosition);
        }
    }

    private void DestroyChilds()
    {
        foreach (Transform child in pointForSpawn.transform)
            Destroy(child.gameObject);
    }
    public void PrepareObject(GameObject newObject, float newMinDistance, float newMaxDistance, float speed)
    {
        DestroyChilds();
        maxDistance = -newMaxDistance;
        minDistance = -newMinDistance;
        mouseSpeed = speed;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, minDistance);
        pointForSpawn.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        Instantiate(newObject, pointForSpawn.transform);
    }
}
