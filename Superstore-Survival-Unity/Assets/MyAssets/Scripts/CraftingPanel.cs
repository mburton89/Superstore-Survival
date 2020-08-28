using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CraftingPanel : MonoBehaviour
{
    [SerializeField] Transform craftingSlotsParent;

    public CraftingItemSlot[] craftingSlots;

    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    private void Start()
    {
        for (int i = 0; i < craftingSlots.Length; i++)
        {
            craftingSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            craftingSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            craftingSlots[i].OnRightClickEvent += OnRightClickEvent;
            craftingSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            craftingSlots[i].OnEndDragEvent += OnEndDragEvent;
            craftingSlots[i].OnDragEvent += OnDragEvent;
            craftingSlots[i].OnDropEvent += OnDropEvent;
        }
    }

    private void OnValidate()
    {
        craftingSlots = craftingSlotsParent.GetComponentsInChildren<CraftingItemSlot>();
    }

    public bool AddItem(EquipableItem item, out EquipableItem previousItem)
    {
        for (int i = 0; i < craftingSlots.Length; i++)
        {
            if (craftingSlots[i].ItemType == item.ItemType)
            {
                previousItem = (EquipableItem)craftingSlots[i].Item;
                craftingSlots[i].Item = item;
                return true;
            }
        }
        previousItem = null;
        return false;
    }

    public bool RemoveItem(EquipableItem item)
    {
        for (int i = 0; i < craftingSlots.Length; i++)
        {
            if (craftingSlots[i].Item == item)
            {
                craftingSlots[i].Item = null;
                return true;
            }
        }
        return false;
    }
}
