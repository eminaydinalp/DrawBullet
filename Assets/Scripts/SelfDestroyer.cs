using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 2f);
    }
}
