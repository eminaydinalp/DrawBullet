using System;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject currentLine;

    public LineRenderer lineRenderer;

    public List<Vector3> fingerPosition;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 tempFingerPos = RayToTouch();

            if (Vector3.Distance(tempFingerPos, fingerPosition[fingerPosition.Count - 1]) > 0.1f)
            {
                UpdateLine(tempFingerPos);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Destroy(currentLine);
        }
    }

    public void CreateLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        
        fingerPosition.Clear();
        
        fingerPosition.Add(RayToTouch());
        fingerPosition.Add(RayToTouch());

        lineRenderer.SetPosition(0, fingerPosition[0]);
        lineRenderer.SetPosition(1, fingerPosition[1]);
        
    }

    public void UpdateLine(Vector3 newFingerPos)
    {
        fingerPosition.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
    }


    public Vector3 RayToTouch()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            
            return new Vector3(raycastHit.point.x, raycastHit.point.y + 0.5f, raycastHit.point.z);
        }
        else
        {
            return Vector3.zero;
        }
    }
}
