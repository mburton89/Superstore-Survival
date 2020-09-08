﻿
public class CraftingItemSlot : ItemSlot
{
    public ItemType ItemType;

    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = ItemType.ToString() + " Slot";
    }

    public override bool CanReceiveItem(Item item)
    {
        //Determine if item slot is empty
        if (item == null)
        {
            return true;
        }

        EquipableItem equipableItem = item as EquipableItem;
        return equipableItem != null && equipableItem.ItemType == ItemType;
    }
}
