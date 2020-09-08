using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] Text ItemNameText;

    //Make item name apprear
    public void ShowTooltip (EquipableItem item)
    {
        ItemNameText.text = item.ItemName;
        
        gameObject.SetActive(true);
    }

    //Hide item name
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
