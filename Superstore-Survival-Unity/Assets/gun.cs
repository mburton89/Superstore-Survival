using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    private RaycastHit hit;

    void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 100, Color.magenta);

        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * 100, out hit, Mathf.Infinity);

        print(hit.transform);
    }
}
