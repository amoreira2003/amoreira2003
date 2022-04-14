using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryHandler : InventoryHandler
{

    public ItemSlotContainer currentViewingSlot;

    public override void setItemSlotsRaycastTarget(bool boolean)
    {
        Debug.Log(getItemSlots().Count);
        foreach (InventorySlotScript itemSlot in getItemSlots())
        {
            itemSlot.setIconRayCastStatus(boolean);
        }

        if (currentViewingSlot == null) return; // Guard Clause
  
        currentViewingSlot.setContainerIconsRaycastAcceptance(boolean);
        Debug.Log("Swooshed");
    }

    public List<InventorySlotScript> getHotbarSlots()
    {
        List<InventorySlotScript> allItemSlots = new List<InventorySlotScript>();

        foreach (Transform inventoryTransform in transform)
        {
            if (inventoryTransform.gameObject.tag == "Hotbar")
            {
                foreach (Transform itemSlotTransform in inventoryTransform)
                {
                    if (itemSlotTransform.gameObject.GetComponent<InventorySlotScript>() != null)
                    {
                        GameObject itemSlot = itemSlotTransform.gameObject;
                        allItemSlots.Add(itemSlot.GetComponent<InventorySlotScript>());
                    }
                }
            }
        }
        Debug.Log("Polling Hotbar");
        return allItemSlots;
    }
}


