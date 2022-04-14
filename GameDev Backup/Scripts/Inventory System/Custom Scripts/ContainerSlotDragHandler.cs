using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerSlotDragHandler : DragHandler
{
 
    public override void Awake() {
        currentSlot = getCurrentSlot();
        ItemSlotContainer containerScript = transform.parent.transform.parent.transform.parent.transform.parent.gameObject.GetComponent<ItemSlotContainer>();
        inventoryHandler = containerScript.viewerHandler;
        inspector = inventoryHandler.transform.Find("Inventory/Inspector").GetComponent<InventoryInspectorScript>();
    }
    
}
