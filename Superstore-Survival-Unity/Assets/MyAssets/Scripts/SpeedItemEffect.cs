using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

//Attempt at first item effect implementation

[CreateAssetMenu]
public class SpeedItemEffect : EquipableItemEffect
{
    public float SpeedMultiplier;

    //Player's run speed equals the multiplier run speed and moves quicker
    public override void ExecuteEffect(EquipableItem parentItem, FirstPersonController character)
    {
        character.m_RunSpeed *= SpeedMultiplier;
    }
}
