using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Characters.ThirdPerson;

public enum ItemType
{
    Base,
    Crafted,
    Special
}
public enum ItemName
{
    Trash,
    TheftedMerchandise,
    BoxCutter,
    Tape,
    CheapBallpointPen,
    ToiletPaper,
    BrokenHanger,
    PileOfTrash,
    UnsellableMerchandise,
    TrashConfetti,
    TrashGrenade,
    StankyPen,
    SoiledToiletPaper,
    CompleteTrash,
    SuspiciousActivity,
    IncriminatingFootage,
    EmployeeMonth,
    RecordedLoss,
    PutridBox,
    AbsurdConcoction,
    EmosParadise,
    TapeStrips,
    InkMess,
    ToiletPaperShreds,
    SeverelyBrokenHanger,
    TapeBall,
    YoyoPen,
    ToiletPaperBomb,
    FunctionalHanger,
    PenMissle,
    CryForHelp,
    HangerFrisbee,
    ToiletPaperHoarder,
    ToiletPaperLauncher,
    RedneckNunchucks
}

[CreateAssetMenu]
public class EquipableItem : Item
{
    public float PlayerSpeedIncrease;
    public float PlayerSpeedDecrease;
    public float TimeSpeedIncrease;
    public float TimeSpeedDecrease;
    public float DetectionIncrease;
    public float DetectionDecrease;
    public float EnemySpeedIncrease;
    public float EnemySpeedDecrease;
    public float Force;
  
    [Space]
    public ItemType ItemType;

    public void Equip(InventoryManager c)
    {

    }
    public void Unequip(InventoryManager c)
    {

    }

    public void IncreasePlayerSpeed(FirstPersonController character)
    {
        character.m_RunSpeed *= PlayerSpeedIncrease;
    }

    public void DecreasePlayerSpeed(FirstPersonController character)
    {
        character.m_RunSpeed /= PlayerSpeedDecrease;
    }

    public void IncreaseTimeCount(Timer timer)
    {
        timer.speed = TimeSpeedIncrease;
    }

    public void DecreaseTimeCount(Timer timer)
    {
        timer.speed = TimeSpeedDecrease;
    }

    public void IncreaseDetection(EnemySight enemy)
    {
        enemy.sightDist = DetectionIncrease;
    }

    public void DecreaseDetection(EnemySight enemy)
    {
        enemy.sightDist = DetectionDecrease;
    }

    public void IncreaseEnemySpeed(EnemySight enemy)
    {
        enemy.patrolSpeed = EnemySpeedIncrease;
    }

    public void DecreaseEnemySpeed(EnemySight enemy)
    {
        enemy.patrolSpeed = EnemySpeedDecrease;
    }

}
