using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class DoorKey : MonoBehaviour
{

    public bool inTrigger = false;


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<FirstPersonController>())
        {
            inTrigger = true;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        print("exit");
        if (other.gameObject.GetComponent<FirstPersonController>())
        {
           
            inTrigger = false;
        }
    }

    void Update()
    {
        print(inTrigger);
        if (inTrigger == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DoorScript.doorKey = true;
                Destroy(this.gameObject);
            }
        }
    }
    private bool _canCollectKey = true;

    private IEnumerator InTriggerBuffer()
    {
        _canCollectKey = false;
        yield return new WaitForSeconds(1);
        _canCollectKey = true;
    }
}


    /*void OnGUI()
    {
        if (inTrigger)
        {
            GUI.Box(new Rect(0, 60, 200, 25), "Press E to take key");
        }
    }
}*/