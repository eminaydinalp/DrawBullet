using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject currentLine;

    public LineRenderer lineRenderer;

    public List<Vector3> fingerPosition;
    private Camera _camera;

    [SerializeField] private GameObject bullet;
    [SerializeField] private MoveBullet[] moveObjects;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        #region Standalone Inputs

        if (Input.GetMouseButtonDown(0))
        {
            CreateLine(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 tempFingerPos = RayToTouch(Input.mousePosition);

            if (Vector3.Distance(tempFingerPos, fingerPosition[fingerPosition.Count - 1]) > 0.1f)
            {
                UpdateLine(tempFingerPos);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Destroy(currentLine);
            StartCoroutine(ThrowBullet());
        }

        #endregion

        #region Mobile Input

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                CreateLine(touch.position);
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 tempFingerPos = RayToTouch(touch.position);

                if (Vector3.Distance(tempFingerPos, fingerPosition[fingerPosition.Count - 1]) > 0.1f)
                {
                    UpdateLine(tempFingerPos);
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                Destroy(currentLine);
                StartCoroutine(ThrowBullet());
            }
        }

        #endregion
        
    }

    public void CreateLine(Vector3 touchPosition)
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        
        fingerPosition.Clear();
        
        fingerPosition.Add(RayToTouch(touchPosition));
        fingerPosition.Add(RayToTouch(touchPosition));

        lineRenderer.SetPosition(0, fingerPosition[0]);
        lineRenderer.SetPosition(1, fingerPosition[1]);
        
    }

    public void UpdateLine(Vector3 newFingerPos)
    {
        fingerPosition.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
    }


    public Vector3 RayToTouch(Vector3 touchPosition)
    {
        Ray ray = _camera.ScreenPointToRay(touchPosition);
        
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            
            return new Vector3(raycastHit.point.x, raycastHit.point.y + .5f, raycastHit.point.z);
        }
        else
        {
            return Vector3.zero;
        }
    }

    public IEnumerator ThrowBullet()
    {
        foreach (var moveObject in moveObjects)
        {
            moveObject.gameObject.SetActive(true);
            //moveObject.MooveBullet();
            moveObject.isMove = true;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
