﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorSpinner : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3 (0,0,1) * (500 * Time.deltaTime));
    }
}
