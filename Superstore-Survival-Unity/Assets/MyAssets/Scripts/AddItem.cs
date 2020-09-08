using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Inventory inventory;
    [SerializeField] KeyCode itemPickupKeyCode = KeyCode.E;

    private bool isInRange;

    //Pickup item and add to player's inventory
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
    
    //Determine that player is close enough to pickup item
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    //Determine that player is not close enough to pickup item
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
}
