using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public EquipableItem ScribtableObject;
    private Rigidbody ItemRigidbody;
    private void Awake()
    {
        ItemRigidbody = gameObject.GetComponent<Rigidbody>();
        ItemRigidbody.AddRelativeForce(Vector3.forward * ScribtableObject.Force);
    }
}
