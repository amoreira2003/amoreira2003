using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropScript : MonoBehaviour
{


    Item item;
    int quantity;

    void Start()
    {
        if (item == null) return;
        if (quantity > item.maxStack) quantity = item.maxStack;
    }

    public string getItemName()
    {
        return item.name;
    }

    public bool tryToPick(InventoryHandler inventoryHandler)
    {
        if (inventoryHandler.addItem(item, this))
        {
            if (quantity <= 0)
            {
                GameObject.Destroy(gameObject);
                return true;
            }

        }
        return false;

    }

    public Item getItem()
    {
        return this.item;
    }

    public void setItem(Item newItem)
    {
        this.item = newItem;
    }

    public int getQuantity()
    {
        return this.quantity;
    }
    public void setQuantity(int newQuantity)
    {
        this.quantity = newQuantity;
    }


}
