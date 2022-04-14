using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class InventoryInspectorScript : MonoBehaviour
{
    public Image ItemIcon;
    public TMP_Text ItemNameDisplay;
    public TMP_Text ItemDescription;
    public TMP_Text ItemQuantityText;


    public InventorySlotScript item;
    Image inspectorBackground;


    void Start()
    {
        inspectorBackground = GetComponent<Image>();
    }

    private void Update()
    {
        if (item == null)
        {
            inspectorBackground.enabled = false;
            ItemIcon.enabled = false;
            ItemIcon.sprite = null;
            ItemDescription.text = string.Empty;
            ItemNameDisplay.text = string.Empty;
            ItemQuantityText.text = string.Empty;
            return;
        } 
        inspectorBackground.enabled = true;
        ItemIcon.enabled = true;
        ItemIcon.sprite = item.getItemIcon();
        ItemDescription.text = item.getItemDescription();
        ItemNameDisplay.text = item.getItemName();
        ItemQuantityText.text = string.Empty+ item.getQuantity() + item.getQuantitySuffix();
    }

}


