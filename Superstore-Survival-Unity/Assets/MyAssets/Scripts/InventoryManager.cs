using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] Inventory inventory;
    [SerializeField] CraftingPanel craftingPanel;
    [SerializeField] Inventory craftedPanel;
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] ItemTooltip craftedTooltip;
    [SerializeField] Image draggableItem;
    [SerializeField] DropItemArea dropItemArea;
    [SerializeField] QuestionDialog questionDialog;
    [SerializeField] DropItemArea UseItemArea;
    public Item UsedItem;
    public Camera Player;
    private Vector3 PlayerDirection;
    private Vector3 PlayerPosition;
    private Quaternion PlayerRotation;
    private Vector3 SpawnPosition;
    private ItemSlot draggedSlot;
    public ItemBehavior ItemBehavior;

    public AudioSource audioSource;

    public AudioClip itemHover;
    public AudioClip itemSelected;
    public AudioClip itemUsed;
    public AudioClip itemDiscarded;

    private void Update()
    {
        PlayerPosition = Player.transform.position;
        PlayerRotation = Player.transform.rotation;
        PlayerDirection = Player.transform.forward;
        SpawnPosition = PlayerPosition + PlayerDirection * 2;
    }

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
        inventory.OnRightClickEvent += InventoryRightClick;
        craftingPanel.OnRightClickEvent += CraftingPanelRightClick;
        craftedPanel.OnRightClickEvent += InventoryRightClick;
        //Pointer Enter
        inventory.OnPointerEnterEvent += ShowTooltip;
        craftingPanel.OnPointerEnterEvent += ShowTooltip;
        craftedPanel.OnPointerEnterEvent += ShowTooltip;
        //Pointer Exit
        inventory.OnPointerExitEvent += HideTooltip;
        craftingPanel.OnPointerExitEvent += HideTooltip;
        craftedPanel.OnPointerExitEvent += HideTooltip;
        //Begin Drag
        inventory.OnBeginDragEvent += BeginDrag;
        craftingPanel.OnBeginDragEvent += BeginDrag;
        craftedPanel.OnBeginDragEvent += BeginDrag;
        //End Drag
        inventory.OnEndDragEvent += EndDrag;
        craftingPanel.OnEndDragEvent += EndDrag;
        craftedPanel.OnEndDragEvent += EndDrag;
        //Drag
        inventory.OnDragEvent += Drag;
        craftingPanel.OnDragEvent += Drag;
        craftedPanel.OnDragEvent += Drag;
        //Drop
        inventory.OnDropEvent += Drop;
        craftingPanel.OnDropEvent += Drop;
        craftedPanel.OnDropEvent += Drop;
        dropItemArea.OnDropEvent += DiscardItem;
        //Use
        UseItemArea.OnDropEvent += UseItem;
    }

    private void InventoryRightClick(ItemSlot itemSlot)
    {
        EquipableItem equipableItem = itemSlot.Item as EquipableItem;
        if (equipableItem != null)
        {
            audioSource.clip = itemSelected;
            audioSource.Play();
            Equip(equipableItem);
        }
    }

    private void CraftingPanelRightClick(ItemSlot itemSlot)
    {
        EquipableItem equipableItem = itemSlot.Item as EquipableItem;
        if (equipableItem != null)
        {
            audioSource.clip = itemSelected;
            audioSource.Play();
            Unequip(equipableItem);
        }
    }

    private void ShowTooltip(ItemSlot itemSlot)
    {
        EquipableItem equipableItem = itemSlot.Item as EquipableItem;
        if (equipableItem != null)
        {
            audioSource.clip = itemHover;
            audioSource.Play();
            itemTooltip.ShowTooltip(equipableItem);
            craftedTooltip.ShowTooltip(equipableItem);
        }
    }

    private void HideTooltip(ItemSlot itemSlot)
    {
        itemTooltip.HideTooltip();
        craftedTooltip.HideTooltip();
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

    private void DiscardItem()
    {
        if (draggedSlot == null)
        {
            return;
        }

        questionDialog.Show();
        ItemSlot itemSlot = draggedSlot;
        questionDialog.OnYesEvent += () => DestroyItemInSlot(itemSlot);
        audioSource.clip = itemDiscarded;
        audioSource.Play();
    }

    private void DestroyItemInSlot(ItemSlot itemSlot)
    {
        Destroy(itemSlot.Item);
        itemSlot.Item = null;
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
    public void UseItem()
    {
        audioSource.clip = itemUsed;
        audioSource.Play();
        GameObject UsedItem = Instantiate(draggedSlot.Item.Prefab, SpawnPosition, PlayerRotation);
        inventory.RemoveItem(draggedSlot.Item);
        UsedItem.AddComponent(typeof(ItemBehavior));
    }
}
