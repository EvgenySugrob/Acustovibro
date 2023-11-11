using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintsSpanwCamera : MonoBehaviour
{
    public GameObject hints;
    public Transform panel;
    public float distance = 10f;

    void Update()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;

        //var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePosition.z = 0;
        //panel.transform.localPosition = mousePosition;
        //Vector3 pos1 = UnityEngine.Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //Vector3 pos2 = UnityEngine.Camera.main.WorldToViewportPoint(panel.transform.position);
        //Vector3 pos = new Vector3(pos1.x, pos1.y, pos2.z);
        //// panel.localPosition = pos Input.mousePosition +(Vector3.up * verticalOffset) + (Vector3.left * horizontalOffset);
        //panel.transform.position = UnityEngine.Camera.main.ViewportToWorldPoint(pos);

    }

    public void OnMouseEnter()
    {
        hints.SetActive(true);
    }
    public void OnMouseExit()
    {
        hints.SetActive(false);
    }
}
