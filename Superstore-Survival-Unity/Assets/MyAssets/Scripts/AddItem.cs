using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Inventory inventory;
    [SerializeField] KeyCode itemPickupKeyCode = KeyCode.E;

    private bool isInRange;


    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
    }
    void Update()
    {
        if (isInRange && Input.GetKeyDown(itemPickupKeyCode))
        {
            if (item != null)
            {
                inventory.AddItem(Instantiate(item));
                item = null;
                Destroy(gameObject);
            }   
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
}
