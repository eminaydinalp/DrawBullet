using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float moveSpeed;

    private void Update()
    {
        PlaneMove();
    }

    private void PlaneMove()
    {
        transform.Translate(transform.forward * (moveSpeed * Time.deltaTime));
    }
}
