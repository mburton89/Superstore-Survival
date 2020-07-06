using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] CraftingPanel craftingPanel;
    [SerializeField] Image CraftedItemImage;
    [SerializeField] Button craftButton;
    [SerializeField] Item item;

    public float timeRemaining = 2;
    public bool timerIsRunning = false;

    public Sprite PileOfTrash;
    public Sprite UnsellableMerchandise;
    public Sprite TrashConfetti;
    public Sprite TrashGrenade;
    public Sprite StankyPen;
    public Sprite SoiledToiletPaper;
    public Sprite CompleteTrash;
    public Sprite SuspiciousActivity;
    public Sprite IncriminatingFootage;
    public Sprite EmployeeMonth;
    public Sprite RecordedLoss;
    public Sprite PutridBox;
    public Sprite AbsurdConcoction;
    public Sprite EmosParadise;
    public Sprite TapeStrips;
    public Sprite InkyMess;
    public Sprite ToiletPaperShreds;
    public Sprite SeverelyBrokenHanger;
    public Sprite TapeBall;
    public Sprite YoyoPen;
    public Sprite ToiletPaperBomb;
    public Sprite FunctionalHanger;
    public Sprite PenMissle;
    public Sprite CryForHelp;
    public Sprite HangerFrisbee;
    public Sprite ToiletPaperHoarder;
    public Sprite ToiletPaperLauncher;
    public Sprite RedneckNunchucks;

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
    //     - Cheap Ballpoint Pen + Cheap Ballpoint Pen = Pen Missle
    //     - Cheap Ballpoint Pen + Toilet Paper = Cry For Help
    //     - Cheap Ballpoint Pen + Broken Hanger = Hanger Frisbee
    //     - Toilet Paper + Toilet Paper = Toilet Paper Hoarder
    //     - Toilet Paper + Broken Hanger = Toilet Paper Launcher
    //     - Broken Hanger + Broken Hanger = Redneck Nunchucks

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
                CraftedItemImage.GetComponent<Image>().sprite = null;
                timeRemaining = 2;
            }
        }
    }

    private void OnEnable()
    {
        craftButton.onClick.AddListener(HandleCraftPressed);
    }

    private void OnDisable()
    {
        craftButton.onClick.RemoveListener(HandleCraftPressed);
    }

    void HandleCraftPressed()
    {
        CraftItem();
    }

    private void Countdown()
    {
        timerIsRunning = true;
    }

    public void CraftItem()
    {
        if (craftingPanel.craftingSlots[0] == null || craftingPanel.craftingSlots[1] == null)
        {
            //TODO show user BOTH slots need to be filled
            return; //won't execute rest of method
        }

        // Trash Pile
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Trash" && craftingPanel.craftingSlots[1].Item.ItemName == "Trash")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Big Pile Of Trash" Item
            CraftedItemImage.GetComponent<Image>().sprite = PileOfTrash;
            Countdown();
        }

        // Unsellable Merchandise
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Trash" && craftingPanel.craftingSlots[1].Item.ItemName == "Thefted Merchandise"
            || craftingPanel.craftingSlots[0].Item.ItemName == "Thefted Merchandise" && craftingPanel.craftingSlots[1].Item.ItemName == "Trash")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = UnsellableMerchandise;
            Countdown();
        }

        // Tash Confetti
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Trash" && craftingPanel.craftingSlots[1].Item.ItemName == "Box Cutter"
            || craftingPanel.craftingSlots[0].Item.ItemName == "Box Cutter" && craftingPanel.craftingSlots[1].Item.ItemName == "Trash")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = TrashConfetti;
            Countdown();
        }

        // Trash Grenade
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Trash" && craftingPanel.craftingSlots[1].Item.ItemName == "Tape"
            || craftingPanel.craftingSlots[0].Item.ItemName == "Tape" && craftingPanel.craftingSlots[1].Item.ItemName == "Trash")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = TrashGrenade;
            Countdown();
        }

        // Stanky Pen
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Trash" && craftingPanel.craftingSlots[1].Item.ItemName == "Cheap Ballpoint Pen"
            || craftingPanel.craftingSlots[0].Item.ItemName == "Cheap Ballpoint Pen" && craftingPanel.craftingSlots[1].Item.ItemName == "Trash")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = StankyPen;
            Countdown();
        }

        // Soiled Toilet Paper
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Trash" && craftingPanel.craftingSlots[1].Item.ItemName == "Toilet Paper"
            || craftingPanel.craftingSlots[0].Item.ItemName == "Toilet Paper" && craftingPanel.craftingSlots[1].Item.ItemName == "Trash")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = SoiledToiletPaper;
            Countdown();
        }

        // Complete Trash
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Trash" && craftingPanel.craftingSlots[1].Item.ItemName == "Broken Hanger"
            || craftingPanel.craftingSlots[0].Item.ItemName == "Broken Hanger" && craftingPanel.craftingSlots[1].Item.ItemName == "Trash")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = CompleteTrash;
            Countdown();
        }

        // Suspicious Activity
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Thefted Merchandise" && craftingPanel.craftingSlots[1].Item.ItemName == "Thefted Merchandise")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = SuspiciousActivity;
            Countdown();
        }

        // Incriminating Footage
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Thefted Merchandise" && craftingPanel.craftingSlots[1].Item.ItemName == "Box Cutter"
            || craftingPanel.craftingSlots[0].Item.ItemName == "Box Cutter" && craftingPanel.craftingSlots[1].Item.ItemName == "Thefted Merchandise")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = IncriminatingFootage;
            Countdown();
        }

        // Employee of the Month
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Thefted Merchandise" && craftingPanel.craftingSlots[1].Item.ItemName == "Tape"
            || craftingPanel.craftingSlots[0].Item.ItemName == "Tape" && craftingPanel.craftingSlots[1].Item.ItemName == "Thefted Merchandise")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = EmployeeMonth;
            Countdown();
        }

        // Recorded Loss
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Thefted Merchandise" && craftingPanel.craftingSlots[1].Item.ItemName == "Cheap Ballpoint Pen"
            || craftingPanel.craftingSlots[0].Item.ItemName == "Cheap Ballpoint Pen" && craftingPanel.craftingSlots[1].Item.ItemName == "Thefted Merchandise")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = RecordedLoss;
            Countdown();
        }

        // Putrid Box
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Thefted Merchandise" && craftingPanel.craftingSlots[1].Item.ItemName == "Toilet Paper"
            || craftingPanel.craftingSlots[0].Item.ItemName == "Toilet Paper" && craftingPanel.craftingSlots[1].Item.ItemName == "Thefted Merchandise")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = PutridBox;
            Countdown();
        }

        // Absurd Concoction
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Thefted Merchandise" && craftingPanel.craftingSlots[1].Item.ItemName == "Broken Hanger"
            || craftingPanel.craftingSlots[0].Item.ItemName == "Broken Hanger" && craftingPanel.craftingSlots[1].Item.ItemName == "Thefted Merchandise")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = AbsurdConcoction;
            Countdown();
        }

        // Emo's Paradise
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Box Cutter" && craftingPanel.craftingSlots[1].Item.ItemName == "Box Cutter")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = EmosParadise;
            Countdown();
        }

        // Tape Strips
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Box Cutter" && craftingPanel.craftingSlots[1].Item.ItemName == "Tape"
            || craftingPanel.craftingSlots[0].Item.ItemName == "Tape" && craftingPanel.craftingSlots[1].Item.ItemName == "Box Cutter")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = TapeStrips;
            Countdown();
        }

        // Nasty Inky Mess
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Box Cutter" && craftingPanel.craftingSlots[1].Item.ItemName == "Cheap Ballpoint Pen"
            || craftingPanel.craftingSlots[0].Item.ItemName == "Cheap Ballpoint Pen" && craftingPanel.craftingSlots[1].Item.ItemName == "Box Cutter")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = InkyMess;
            Countdown();
        }

        // Toilet Paper Shreds
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Box Cutter" && craftingPanel.craftingSlots[1].Item.ItemName == "Toilet Paper"
            || craftingPanel.craftingSlots[0].Item.ItemName == "Toilet Paper" && craftingPanel.craftingSlots[1].Item.ItemName == "Box Cutter")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = ToiletPaperShreds;
            Countdown();
        }

        // Severely Broken Hanger
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Box Cutter" && craftingPanel.craftingSlots[1].Item.ItemName == "Broken Hanger"
            || craftingPanel.craftingSlots[0].Item.ItemName == "Broken Hanger" && craftingPanel.craftingSlots[1].Item.ItemName == "Box Cutter")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = SeverelyBrokenHanger;
            Countdown();
        }

        // Tape Ball
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Tape" && craftingPanel.craftingSlots[1].Item.ItemName == "Tape")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = TapeBall;
            Countdown();
        }

        // Yo-Yo Pen
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Tape" && craftingPanel.craftingSlots[1].Item.ItemName == "Cheap Ballpoint Pen"
            || craftingPanel.craftingSlots[0].Item.ItemName == "Cheap Ballpoint Pen" && craftingPanel.craftingSlots[1].Item.ItemName == "Tape")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = YoyoPen;
            Countdown();
        }

        // Toilet Paper Bomb
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Tape" && craftingPanel.craftingSlots[1].Item.ItemName == "Toilet Paper"
            || craftingPanel.craftingSlots[0].Item.ItemName == "Toilet Paper" && craftingPanel.craftingSlots[1].Item.ItemName == "Tape")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = ToiletPaperBomb;
            Countdown();
        }

        // Fully Functional Hanger
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Tape" && craftingPanel.craftingSlots[1].Item.ItemName == "Broken Hanger"
            || craftingPanel.craftingSlots[0].Item.ItemName == "Broken Hanger" && craftingPanel.craftingSlots[1].Item.ItemName == "Tape")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = FunctionalHanger;
            Countdown();
        }

        // Pen Missle
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Cheap Ballpoint Pen" && craftingPanel.craftingSlots[1].Item.ItemName == "Cheap Ballpoint Pen")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = PenMissle;
            Countdown();
        }

        // Cry For Help
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Cheap Ballpoint Pen" && craftingPanel.craftingSlots[1].Item.ItemName == "Toilet Paper"
            || craftingPanel.craftingSlots[0].Item.ItemName == "Toilet Paper" && craftingPanel.craftingSlots[1].Item.ItemName == "Cheap Ballpoint Pen")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = CryForHelp;
            Countdown();
        }

        // Hanger Frisbee
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Cheap Ballpoint Pen" && craftingPanel.craftingSlots[1].Item.ItemName == "Broken Hanger"
            || craftingPanel.craftingSlots[0].Item.ItemName == "Broken Hanger" && craftingPanel.craftingSlots[1].Item.ItemName == "Cheap Ballpoint Pen")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = HangerFrisbee;
            Countdown();
        }

        // Toilet Paper Hoarder
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Toilet Paper" && craftingPanel.craftingSlots[1].Item.ItemName == "Toilet Paper")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = ToiletPaperHoarder;
            Countdown();
        }

        // Toilet Paper Launcher
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Toilet Paper" && craftingPanel.craftingSlots[1].Item.ItemName == "Broken Hanger"
            || craftingPanel.craftingSlots[0].Item.ItemName == "Broken Hanger" && craftingPanel.craftingSlots[1].Item.ItemName == "Toilet Paper")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = ToiletPaperLauncher;
            Countdown();
        }

        // Redneck Nunchucks
        if (craftingPanel.craftingSlots[0].Item.ItemName == "Broken Hanger" && craftingPanel.craftingSlots[1].Item.ItemName == "Broken Hanger")
        {
            //Destroy itemOne and itemTwo
            Destroy(craftingPanel.craftingSlots[0].Item);
            Destroy(craftingPanel.craftingSlots[1].Item);
            craftingPanel.craftingSlots[0].Item = null;
            craftingPanel.craftingSlots[1].Item = null;
            //Create "Unsellable Merchandise" Item
            CraftedItemImage.GetComponent<Image>().sprite = RedneckNunchucks;
            Countdown();
        }

        else
        { 
            //TODO Don't create new item and show user invalid permutation
        }
    }
}
