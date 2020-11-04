using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public static bool doorKey = false;
    public bool open;
    public bool close;
    public bool inTrigger;
    public Transform hinge;
    void OnTriggerEnter(Collider other)
    {
        
        inTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }

    void Update()
    {
        if (inTrigger)
        {
            if (close)
            {
                if (doorKey)
                {
                    if (Input.GetKeyDown(KeyCode.E) && close == true)
                    {
                        open = true;
                        close = false;
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    close = true;
                    open = false;
                }
            }
        }

        if (open)
        {
            print("open");
            var newRot = Quaternion.RotateTowards(hinge.rotation, Quaternion.Euler(0.0f, -90.0f, 0.0f), Time.deltaTime * 200);
            hinge.rotation = newRot;
        }
        else
        {
            var newRot = Quaternion.RotateTowards(hinge.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), Time.deltaTime * 200);
            hinge.rotation = newRot;
        }
    }
}

    /*void OnGUI()
    {
        if (inTrigger)
        {
            if (open)
            {
                GUI.Box(new Rect(0, 0, 200, 25), "Press E to close");
            }
            else
            {
                if (doorKey)
                {
                    GUI.Box(new Rect(0, 0, 200, 25), "Press E to open");
                }
                else
                {
                    GUI.Box(new Rect(0, 0, 200, 25), "Need a key!");
                           
            }
        }
    }
}*/