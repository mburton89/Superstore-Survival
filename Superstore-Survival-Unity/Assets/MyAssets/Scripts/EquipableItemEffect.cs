using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public abstract class EquipableItemEffect : ScriptableObject
{
    //Activate an items effect
    public abstract void ExecuteEffect(EquipableItem parentItem, FirstPersonController character);
}
