using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] CraftingPanel craftingPanel;
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] Image draggableItem;

    private ItemSlot draggedSlot;

    private void OnValidate()
    {
        if (itemTooltip == null)
        {
            itemTooltip = FindObjectOfType<ItemTooltip>();
        }
    }

    private void Awake()
    {
        //Setup Events
        //Right Click
        inventory.OnRightClickEvent += Equip;
        craftingPanel.OnRightClickEvent += Unequip;
        //Pointer Enter
        inventory.OnPointerEnterEvent += ShowTooltip;
        craftingPanel.OnPointerEnterEvent += ShowTooltip;
        //Pointer Exit
        inventory.OnPointerExitEvent += HideTooltip;
        craftingPanel.OnPointerExitEvent += HideTooltip;
        //Begin Drag
        inventory.OnBeginDragEvent += BeginDrag;
        craftingPanel.OnBeginDragEvent += BeginDrag;
        //End Drag
        inventory.OnEndDragEvent += EndDrag;
        craftingPanel.OnEndDragEvent += EndDrag;
        //Drag
        inventory.OnDragEvent += Drag;
        craftingPanel.OnDragEvent += Drag;
        //Drop
        inventory.OnDropEvent += Drop;
        craftingPanel.OnDropEvent += Drop;
    }

    private void Equip(ItemSlot itemSlot)
    {
        EquipableItem equipableItem = itemSlot.Item as EquipableItem;
        if (equipableItem != null)
        {
            Equip(equipableItem);
        }
    }

    private void Unequip(ItemSlot itemSlot)
    {
        EquipableItem equipableItem = itemSlot.Item as EquipableItem;
        if (equipableItem != null)
        {
            Unequip(equipableItem);
        }
    }

    private void ShowTooltip(ItemSlot itemSlot)
    {
        EquipableItem equipableItem = itemSlot.Item as EquipableItem;
        if (equipableItem != null)
        {
            itemTooltip.ShowTooltip(equipableItem);
        }
    }

    private void HideTooltip(ItemSlot itemSlot)
    {
        itemTooltip.HideTooltip();
    }

    private void BeginDrag(ItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            draggedSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.Icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

    private void EndDrag(ItemSlot itemSlot)
    {
        draggedSlot = null;
        draggableItem.enabled = false;
    }

    private void Drag(ItemSlot itemSlot)
    {
        if (draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
    }

    private void Drop(ItemSlot dropItemSlot)
    {
        if (dropItemSlot.CanReceiveItem(draggedSlot.Item) && draggedSlot.CanReceiveItem(dropItemSlot.Item))
        {
            EquipableItem dragItem = draggedSlot.Item as EquipableItem;
            EquipableItem dropItem = dropItemSlot.Item as EquipableItem;

            if (draggedSlot is CraftingItemSlot)
            {
                if (dragItem != null)
                {
                    dragItem.Unequip(this);
                }
                if (dropItem != null)
                {
                    dropItem.Equip(this);
                }
            }
            if (dropItemSlot is CraftingItemSlot)
            {
                if (dragItem != null)
                {
                    dragItem.Equip(this);
                }
                if (dropItem != null)
                {
                    dropItem.Unequip(this);
                }
            }

            Item draggedItem = draggedSlot.Item;
            draggedSlot.Item = dropItemSlot.Item;
            dropItemSlot.Item = draggedItem;
        }
    }

    public void Equip(EquipableItem item)
    {
        if (inventory.RemoveItem(item))
        {
            EquipableItem previousItem;
            if (craftingPanel.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    inventory.AddItem(previousItem);
                }
                else
                {
                    inventory.AddItem(item);
                }
            }
        }
    }

    public void Unequip(EquipableItem item)
    {
        if (!inventory.IsFull() && craftingPanel.RemoveItem(item))
        {
            inventory.AddItem(item);
        }
    }
}
