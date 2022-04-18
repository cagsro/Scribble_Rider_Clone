using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRnder : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField]
    private Camera cam;
    private List<Vector3> pointsForDraw = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            DrawTheLine();
        }
    }
    private void DrawTheLine()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 converted = cam.ScreenToWorldPoint(mousePos);
        converted.z = 0;
        pointsForDraw.Add(converted);
        lineRenderer.positionCount = pointsForDraw.Count;
        lineRenderer.SetPositions(pointsForDraw.ToArray());

    }
}
