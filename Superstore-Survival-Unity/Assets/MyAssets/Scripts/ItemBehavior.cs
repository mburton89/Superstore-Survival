using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public EquipableItem ScribtableObject;
    public Rigidbody ItemRigidbody;
    public InventoryManager InventoryManager;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void Awake()
    {
        ItemRigidbody = gameObject.GetComponent<Rigidbody>();
        ItemRigidbody.AddRelativeForce(Vector3.forward * ScribtableObject.Force);
        
    }
}
