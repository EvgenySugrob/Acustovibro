using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sinewave : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int points;
    public float amplitude = 1;
    public float freauency = 1;
    public Vector2 xLimits = new Vector2(0, 1);
    public float movementSpeed = 1;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
     public void Draw()
    {
        float xStart = xLimits.x;
        float Tau = 2 * Mathf.PI;
        float xFinish = xLimits.y;

        lineRenderer.positionCount = points;
        for (int currentPoint = 0; currentPoint < points; currentPoint++)
        {
            float progress = (float)currentPoint / (points - 1);
            float x = Mathf.Lerp(xStart, xFinish, progress);
            float y = amplitude * Mathf.Sin((Tau*freauency*x)+(Time.timeSinceLevelLoad*movementSpeed));
            lineRenderer.SetPosition(currentPoint, new Vector3(x, y, 0));
        }
    }

    private void Update()
    {
        Draw();
    }
}
