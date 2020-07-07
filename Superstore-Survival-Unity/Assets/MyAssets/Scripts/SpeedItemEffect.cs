using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[CreateAssetMenu]
public class SpeedItemEffect : EquipableItemEffect
{
    public float SpeedMultiplier;

    public override void ExecuteEffect(EquipableItem parentItem, FirstPersonController character)
    {
        character.m_RunSpeed *= SpeedMultiplier;
    }
}
