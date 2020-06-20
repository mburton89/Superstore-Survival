using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] CraftingPanel craftingPanel;
    [SerializeField] Image craftedItemImage;
    [SerializeField] CraftedPanel craftedPanel;

    private ItemSlot newItemSlot;

    public Button craftButton;

    private void Awake()
    {

    }

    private void Start()
    {
        Button btn = craftButton.GetComponent<Button>();
        btn.onClick.AddListener(CheckSlots);
    }

    public void CheckCraftedItems()
    {

    }

    public void CheckSlots(ItemSlot itemSlot1, ItemSlot itemSlot2)
    {
        if (itemSlot1 != null && itemSlot2 != null)
        {
            CraftItem();
        }
    }

    public void CraftItem()
    {

    }

    // If crafted items menu is full, diasbled ability to open crafting screen
    // If crafted items menu is not full, enable ability to open crafting screen

    // When CRAFT button is clicked:
    //     - Is crafting screen item slot 1 & item slot 2 both occupied?
    //         - No:  Do nothing.
    //         - Yes: Destroy item in slot 1 & slot 2 and add new item to crafted item slot.
    //                Wait 5 seconds then add crafted item to crafted item UI and remove from
    //                crafted item slot in crafting screen.

    // Crafted item Recipes Are:
    //     - Trash + Trash = Big Pile Of Trash
    //     - Trash + Thefted Merchandise = Unsellable Merchandise
    //     - Trash + Box Cutter = Trash Confetti
    //     - Trash + Tape = Trash Grenade
    //     - Trash + Cheap Ballpoint Pen = Stanky Pen
    //     - Trash + Toilet Paper = Soiled Toilet Paper
    //     - Trash + Broken Hanger = Complete Trash
    //     - Thefted Merchandise + Thefted Merchandise = Suspicious Activity
    //     - Thefted Merchandise + Box Cutter = Incriminating Footage
    //     - Thefted Merchandise + Tape = Employee of the Month
    //     - Thefted Merchandise + Cheap Ballpoint Pen = Recorded Loss
    //     - Thefted Merchandise + Toilet Paper = Putrid Box
    //     - Thefted Merchandise + Broken Hanger = Absurd Concoction
    //     - Box Cutter + Box Cutter = Emo's Paradise
    //     - Box Cutter + Tape = Tape Strips
    //     - Box Cutter + Cheap Ballpoint Pen = Nasty Inky Mess
    //     - Box Cutter + Toilet Paper = Toilet Paper Shreds
    //     - Box Cutter + Broken Hanger = Severely Broken Hanger
    //     - Tape + Tape = Tape Ball
    //     - Tape + Cheap Ballpoint Pen = Yo-Yo Pen
    //     - Tape + Toilet Paper = Toilet Paper Bomb
    //     - Tape + Broken Hanger = Fully Functional Hanger
    //     - Cheap Ballpoint Pen + Cheap Ballpoint Pen = Pen Barrage
    //     - Cheap Ballpoint Pen + Toilet Paper = Cry For Help
    //     - Cheap Ballpoint Pen + Broken Hanger = Hanger Frisbee
    //     - Toilet Paper + Toilet Paper = Toilet Paper Hoarder
    //     - Toilet Paper + Broken Hanger = Toilet Paper Launcher
    //     - Broken Hanger + Broken Hanger = Redneck Nunchucks
}
