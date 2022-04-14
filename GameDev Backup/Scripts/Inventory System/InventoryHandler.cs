using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{

    public virtual List<InventorySlotScript> getItemSlots()
    {
        List<InventorySlotScript> allItemSlots = new List<InventorySlotScript>();

        foreach (Transform inventoryTransform in transform)
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
        Debug.Log("Polling Everything");
        return allItemSlots;
    }

    public virtual bool hasItemTypeInInventory(Item item)
    {
        List<InventorySlotScript> itemSlots = getItemSlots();
        //Debug.Log("Pooling Size: " + itemSlots.Count);
        foreach (InventorySlotScript itemSlot in itemSlots)
        {
            //Debug.Log(itemSlot.item);
            if (itemSlot.getItem() == item)
                return true;
        }
        return false;
    }

    public virtual List<InventorySlotScript> getItemSlotsThatHasItemTypeInInventory(Item item, bool includeFullSlots)
    {
        List<InventorySlotScript> itemSlots = getItemSlots();
        List<InventorySlotScript> returnItemSlots = new List<InventorySlotScript>();

        //Debug.Log("Pooling Size: " + itemSlots.Count);
        foreach (InventorySlotScript itemSlot in itemSlots)
        {
            //Debug.Log(itemSlot.item);
            if (itemSlot.getItem() == item)
            {

                if (!includeFullSlots && itemSlot.getQuantity() == itemSlot.getItemMaxStack()) continue;

                returnItemSlots.Add(itemSlot);
            }
        }

        return returnItemSlots;
    }

    public virtual List<InventorySlotScript> getEmpty()
    {
        List<InventorySlotScript> itemSlots = getItemSlots();
        List<InventorySlotScript> returnItemSlots = new List<InventorySlotScript>();

        //Debug.Log("Pooling Size: " + itemSlots.Count);
        foreach (InventorySlotScript itemSlot in itemSlots)
        {
            //Debug.Log(itemSlot.item);
            if (itemSlot.getItem() == null)
            {
                returnItemSlots.Add(itemSlot);

            }
        }

        return returnItemSlots;
    }

    public virtual bool isFull()
    {
        List<InventorySlotScript> emptySlots = getEmpty();
        return (emptySlots.Count == 0);
    }

    public virtual bool addItem(Item itemType, int quantity)
    {
        List<InventorySlotScript> itemSlots = getItemSlotsThatHasItemTypeInInventory(itemType, false);
        if (itemSlots.Count == 0)
        {
            // Add to First Empty
            InventorySlotScript[] itemSlot = getEmpty().ToArray();
            if (isFull()) return false;

            itemSlot[0].setItem(itemType);
            itemSlot[0].setQuantity(quantity);

            return true;
        }
        else
        {
            // Add to existing stack
            foreach (InventorySlotScript itemSlot in itemSlots)
            {
                if (itemSlot.getQuantity() + quantity <= itemSlot.getItemMaxStack())
                {
                    itemSlot.setQuantity(itemSlot.getQuantity() + quantity);
                    return true;
                }
                else
                {
                    InventorySlotScript[] itemSlotEmpty = getEmpty().ToArray();
                    if (isFull()) return false;

                    int restAfterFirstStack = (itemSlot.getQuantity() + quantity) - itemSlot.getItemMaxStack();

                    itemSlot.setQuantity(itemSlot.getItemMaxStack());
                    itemSlotEmpty[0].setItem(itemType);
                    itemSlotEmpty[0].setQuantity(restAfterFirstStack);
                    return true;
                }
            }
        }

        return false;
    }
    public virtual bool addItem(Item itemType, ItemDropScript dropScript)
    {
        List<InventorySlotScript> itemSlots = getItemSlotsThatHasItemTypeInInventory(itemType, false);
        if (itemSlots.Count == 0)
        {
            // Add to First Empty
            InventorySlotScript[] itemSlot = getEmpty().ToArray();
            if (isFull()) return false;

            itemSlot[0].setItem(itemType);
            itemSlot[0].setQuantity(dropScript.getQuantity());
            dropScript.setQuantity(0);
            return true;

        }
        else
        {
            // Add to existing stack
            foreach (InventorySlotScript itemSlot in itemSlots)
            {
                if (itemSlot.getQuantity() + dropScript.getQuantity() <= itemSlot.getItemMaxStack())
                {
                    itemSlot.setQuantity(itemSlot.getQuantity() + dropScript.getQuantity());
                    dropScript.setQuantity(0);
                    return true;
                }
                else
                {
                    //Check if inventory has space, then if yes add to the next stack;
                    InventorySlotScript[] itemSlotEmpty = getEmpty().ToArray();

                    int restAfterFirstStack = (itemSlot.getQuantity() + dropScript.getQuantity()) - itemSlot.getItemMaxStack();
                    if (itemSlotEmpty.Length == 0)
                    {
                        dropScript.setQuantity(restAfterFirstStack);
                        itemSlot.setQuantity(itemSlot.getItemMaxStack());
                    }
                    else
                    {
                        itemSlot.setQuantity(itemSlot.getItemMaxStack());
                        itemSlotEmpty[0].setItem(itemType);
                        itemSlotEmpty[0].setQuantity(restAfterFirstStack);
                        dropScript.setQuantity(0);
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public virtual bool removeItem(Item itemType, int quantity)
    {
        if (!hasItemTypeInInventory(itemType))
        {
            Debug.Log("No item found");
            return false;
        }

        List<InventorySlotScript> itemSlots = getItemSlotsThatHasItemTypeInInventory(itemType, true);
        foreach (InventorySlotScript itemSlot in itemSlots)
        {
            if (itemSlot.getQuantity() - quantity < 0) return false;
            itemSlot.removeQuantity(quantity);
            return true;
        }
        return false;
    }

    public virtual void setItemSlotsRaycastTarget(bool boolean)
    {
        Debug.Log(getItemSlots().Count);
        foreach (InventorySlotScript itemSlot in getItemSlots())
        {
            itemSlot.setIconRayCastStatus(boolean);
        }
        
    }
}
