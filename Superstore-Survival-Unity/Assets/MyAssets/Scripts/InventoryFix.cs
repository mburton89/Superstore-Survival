using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryFix : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Image image;
    private Color normalColor = Color.white;
    private Color disabledColor = new Color(1, 1, 1, 0);

    void Awake()
    {
        StartCoroutine(Fix());
    }

    IEnumerator Fix()
    {
        yield return new WaitForSeconds(1);
        inventoryPanel.SetActive(false);
    }

}
