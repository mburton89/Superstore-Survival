using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] Text ItemNameText;

    public void ShowTooltip (EquipableItem item)
    {
        ItemNameText.text = item.ItemName;
        
        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
