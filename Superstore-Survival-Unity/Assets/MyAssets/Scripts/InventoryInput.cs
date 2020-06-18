using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class InventoryInput : MonoBehaviour
{
    [SerializeField] GameObject inventoryGameObject;
    [SerializeField] GameObject craftedItemsGameObject;
    [SerializeField] GameObject craftingGameObject;
    [SerializeField] KeyCode[] toggleInventoryKeys;
    [SerializeField] KeyCode[] toggleCraftedItemKeys;
    [SerializeField] KeyCode[] toggleCraftingKeys;

    public GameObject Player;
    
    void Awake()
    {
        HideCursor();
    }

    void Update()
    {
        for (int i = 0; i < toggleInventoryKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleInventoryKeys[i]))
            {
                inventoryGameObject.SetActive(!inventoryGameObject.activeSelf);
                
                if (inventoryGameObject.activeSelf)
                {
                    craftedItemsGameObject.SetActive(false);
                    ShowCursor();
                }
                else
                {
                    craftingGameObject.SetActive(false);
                    HideCursor();
                }

                break;
            }
        }

        for (int i = 0; i < toggleCraftedItemKeys.Length; i++)
        {
            if (!craftedItemsGameObject.activeSelf && inventoryGameObject.activeSelf)
            {
                if (Input.GetKeyDown(toggleCraftedItemKeys[i]))
                {
                    craftingGameObject.SetActive(!craftingGameObject.activeSelf);
                }
            }
            else
            {
                craftingGameObject.SetActive(false);
            }

            break;
        }

        for (int i = 0; i < toggleCraftingKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleCraftingKeys[i]))
            {
                craftedItemsGameObject.SetActive(!craftedItemsGameObject.activeSelf);

                if (craftedItemsGameObject.activeSelf)
                {
                    inventoryGameObject.SetActive(false);
                    craftingGameObject.SetActive(false);
                    ShowCursor();
                }
                else
                {
                    HideCursor();
                }

                break;
            }
        }
    }

    public void ShowCursor()
    {
        Player.GetComponent<FirstPersonController>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideCursor()
    {
        Player.GetComponent<FirstPersonController>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ToggleCraftedItemPanel()
    {
        craftedItemsGameObject.SetActive(!craftedItemsGameObject.activeSelf);
    }
}
