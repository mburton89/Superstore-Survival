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

    public AudioSource audioSource;

    public AudioClip inventoryOpen;
    public AudioClip craftedOpen;
    public AudioClip craftingOpen;

    void Awake()
    {
        HideCursor();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                timerIsRunning = false;
                timeRemaining = 2;
            }
        }

        for (int i = 0; i < toggleInventoryKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleInventoryKeys[i]))
            {
                inventoryGameObject.SetActive(!inventoryGameObject.activeSelf);
                audioSource.clip = inventoryOpen;
                audioSource.Play();

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

#pragma warning disable CS0162 // Unreachable code detected
        for (int i = 0; i < toggleCraftedItemKeys.Length; i++)
#pragma warning restore CS0162 // Unreachable code detected
        {
                if (!craftedItemsGameObject.activeSelf && inventoryGameObject.activeSelf)
                {
                    if (Input.GetKeyDown(toggleCraftedItemKeys[i]))
                    {
                        craftingGameObject.SetActive(!craftingGameObject.activeSelf);
                        audioSource.clip = craftingOpen;
                        audioSource.Play();
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
                    audioSource.clip = craftedOpen;
                    audioSource.Play();

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
