using System;
using System.Collections.Generic;
using UnityEngine;

public class DrawPoint : MonoBehaviour
{
    public List<Vector3> hitPoints;

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                Transform objectHit = hit.transform;
                
                Debug.Log(objectHit.name);
                
                hitPoints.Add(hit.point);
            }
        }
    }
}
