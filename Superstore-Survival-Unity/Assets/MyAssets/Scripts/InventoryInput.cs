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

    public float timeRemaining = 2;
    public bool timerIsRunning = false;

    //Start the game with the cursor disabled
    void Awake()
    {
        HideCursor();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            //Countdown timer
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            //Timer has ended. Reset timer
            else if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                timerIsRunning = false;
                timeRemaining = 2;
            }
        }

        //Open inventory panel
        for (int i = 0; i < toggleInventoryKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleInventoryKeys[i]))
            {
                inventoryGameObject.SetActive(!inventoryGameObject.activeSelf);
                
                //If inventory panel is open show cursor and close crafted panel
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

        //Open crafted panel
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

        //Open crafting window
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

    private void Countdown()
    {
        timerIsRunning = true;
    }

    //Make cursor visiable and useable to click on items
    public void ShowCursor()
    {
        Player.GetComponent<FirstPersonController>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    //Make cursor disappear
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
