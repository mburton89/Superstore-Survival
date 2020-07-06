using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField] Inventory craftedPanel;
    [SerializeField] CraftingPanel craftingPanel;
    [SerializeField] Image CraftedItemImage;
    [SerializeField] Button craftButton;
    [SerializeField] Item item;

    [SerializeField] Item pileOfTrash;
    [SerializeField] Item unsellableMerchandise;
    [SerializeField] Item trashConfetti;
    [SerializeField] Item trashGrenade;
    [SerializeField] Item stankyPen;
    [SerializeField] Item soiledToiletPaper;
    [SerializeField] Item completeTrash;
    [SerializeField] Item suspiciousActivity;
    [SerializeField] Item incriminatingFootage;
    [SerializeField] Item employeeMonth;
    [SerializeField] Item recordedLoss;
    [SerializeField] Item putridBox;
    [SerializeField] Item absurdConcoction;
    [SerializeField] Item emosParadise;
    [SerializeField] Item tapeStrips;
    [SerializeField] Item inkyMess;
    [SerializeField] Item toiletPaperShreds;
    [SerializeField] Item severelyBrokenHanger;
    [SerializeField] Item tapeBall;
    [SerializeField] Item yoyoPen;
    [SerializeField] Item toiletPaperBomb;
    [SerializeField] Item functionalHanger;
    [SerializeField] Item penMissle;
    [SerializeField] Item cryForHelp;
    [SerializeField] Item hangerFrisbee;
    [SerializeField] Item toiletPaperHoarder;
    [SerializeField] Item toiletPaperLauncher;
    [SerializeField] Item redneckNunchucks;

    public float timeRemaining = 2;
    public bool timerIsRunning = false;

    public GameObject notEnoughItems;
    public GameObject inventoryFull;

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
                notEnoughItems.SetActive(false);
                inventoryFull.SetActive(false);
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
        if (craftedPanel.IsFull() == true)
        {
            inventoryFull.SetActive(true);
            Countdown();
        }
        else
        {
            CraftItem();
        }
    }

    private void Countdown()
    {
        timerIsRunning = true;
    }

    public void CraftItem()
    {
        if (craftingPanel.craftingSlots[0].Item == null || craftingPanel.craftingSlots[1].Item == null)
        {
            //Show user BOTH slots need to be filled
            notEnoughItems.SetActive(true);
            Countdown();
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
            craftedPanel.AddItem(pileOfTrash);
            return;
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
            craftedPanel.AddItem(unsellableMerchandise);
            return;
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
            craftedPanel.AddItem(trashConfetti);
            return;
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
            craftedPanel.AddItem(trashGrenade);
            return;
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
            craftedPanel.AddItem(stankyPen);
            return;
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
            craftedPanel.AddItem(soiledToiletPaper);
            return;
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
            craftedPanel.AddItem(completeTrash);
            return;
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
            craftedPanel.AddItem(suspiciousActivity);
            return;
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
            craftedPanel.AddItem(incriminatingFootage);
            return;
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
            craftedPanel.AddItem(employeeMonth);
            return;
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
            craftedPanel.AddItem(recordedLoss);
            return;
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
            craftedPanel.AddItem(putridBox);
            return;
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
            craftedPanel.AddItem(absurdConcoction);
            return;
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
            craftedPanel.AddItem(emosParadise);
            return;
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
            craftedPanel.AddItem(tapeStrips);
            return;
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
            craftedPanel.AddItem(inkyMess);
            return;
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
            craftedPanel.AddItem(toiletPaperShreds);
            return;
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
            craftedPanel.AddItem(severelyBrokenHanger);
            return;
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
            craftedPanel.AddItem(tapeBall);
            return;
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
            craftedPanel.AddItem(yoyoPen);
            return;
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
            craftedPanel.AddItem(toiletPaperBomb);
            return;
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
            craftedPanel.AddItem(functionalHanger);
            return;
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
            craftedPanel.AddItem(penMissle);
            return;
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
            craftedPanel.AddItem(cryForHelp);
            return;
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
            craftedPanel.AddItem(hangerFrisbee);
            return;
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
            craftedPanel.AddItem(toiletPaperHoarder);
            return;
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
            craftedPanel.AddItem(toiletPaperLauncher);
            return;
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
            craftedPanel.AddItem(redneckNunchucks);
            return;
        }
    }
}
