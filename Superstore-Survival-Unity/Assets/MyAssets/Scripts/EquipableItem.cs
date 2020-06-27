using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Base,
    Crafted,
    Special
}

<<<<<<< Updated upstream
=======
public enum ItemName
{
    Trash,
    TheftedMerchandise,
    BoxCutter,
    Tape,
    CheapBallpointPen,
    ToiletPaper,
    BrokenHanger,
    PileOfTrash
}

>>>>>>> Stashed changes
[CreateAssetMenu]
public class EquipableItem : Item
{
    public int SpeedBonus;
    public int TimeBonus;
    public int DetectionBonnus;
    public int EnemySpeedBonus;
    [Space]
    public float SpeedPercentBonus;
    public float TimePercentBonus;
    public float DetectionPercentBonus;
    public float EnemySpeedPercentBonus;
    [Space]
    public ItemType ItemType;

    public void Equip(InventoryManager c)
    {

    }
    public void Unequip(InventoryManager c)
    {

    }
}
