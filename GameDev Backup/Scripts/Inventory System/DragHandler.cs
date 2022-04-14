using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class DragHandler : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    [HideInInspector]
    public InventorySlotScript currentSlot;
    [HideInInspector]
    public PlayerInventoryHandler inventoryHandler;
    [HideInInspector]
    public InventoryInspectorScript inspector;

    public virtual void Awake()
    {
        currentSlot = getCurrentSlot();
        inventoryHandler = getInventoryHandler();
        inspector = inventoryHandler.transform.Find("Inventory/Inspector").GetComponent<InventoryInspectorScript>();

    }



    public virtual InventorySlotScript getCurrentSlot()
    {

        var slotObject = gameObject;

        if (transform.parent.tag == "Hotbar")
        {
            slotObject = transform.parent.transform.parent.gameObject;
            return slotObject.GetComponent<InventorySlotScript>();
        }

        slotObject = transform.parent.gameObject;

        return slotObject.GetComponent<InventorySlotScript>();
    }

    public virtual PlayerInventoryHandler getInventoryHandler()
    {

        if (currentSlot.isHotbarItemSlot())
        {
            var inventoryObject = transform.parent.transform.parent.transform.parent.transform.parent.gameObject;
            Debug.Log(inventoryObject, inventoryObject);
            return inventoryObject.GetComponent<PlayerInventoryHandler>();
        }

        var inventoryObject2 = transform.parent.transform.parent.transform.parent.gameObject;
        Debug.Log(inventoryObject2, inventoryObject2);

        return inventoryObject2.GetComponent<PlayerInventoryHandler>();
    }



    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        inspector.item = currentSlot;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        inspector.item = null;
    }


    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        inventoryHandler.setItemSlotsRaycastTarget(false);
        GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        currentSlot.setDisplayQuantity(false);
        transform.position = Input.mousePosition;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {

        resetIconsProperties();

        RaycastResult onDropRaycastResult = eventData.pointerCurrentRaycast;
        if (onDropRaycastResult.Equals(null)) return;

        RaycastResult onDragRaycastResult = eventData.pointerPressRaycast;
        GameObject onDropItemSlot = onDropRaycastResult.gameObject;

        inventoryHandler.setItemSlotsRaycastTarget(true);

        if (onDropItemSlot == null)
        {
            currentSlot.Drop();
            return;
        }

        if (onDropItemSlot == gameObject) return;
        if (!isSlot(onDropItemSlot)) return;

        InventorySlotScript nextSlot = onDropItemSlot.GetComponent<InventorySlotScript>();

        if (currentSlot.gameObject == nextSlot.gameObject) return;

        switch (eventData.button)
        {

            case PointerEventData.InputButton.Left:

                if (!nextSlot.hasItem())
                {
                    moveItemSlot(currentSlot, nextSlot);
                    return;
                }

                if (isSameItemType(currentSlot, nextSlot))
                {

                    if (isFullStacked(currentSlot, nextSlot))
                    {
                        swapItemSlot(currentSlot, nextSlot);
                        return;
                    }

                    if (!exceededMaxStackLimit(currentSlot, nextSlot))
                    {
                        stackItemSlot(currentSlot, nextSlot);
                        return;

                    }
                    else
                    {

                        swapStackItemSlot(currentSlot, nextSlot);
                        return;
                    }


                }

                swapItemSlot(currentSlot, nextSlot);
                return;


            case PointerEventData.InputButton.Right:

                if (currentSlot.isSingleItem()) goto case PointerEventData.InputButton.Left;

                if (!nextSlot.hasItem())
                {
                    moveSplitItemSlot(currentSlot, nextSlot);
                    return;
                }

                if (isSameItemType(currentSlot, nextSlot))
                {

                    if (isFullStacked(currentSlot, nextSlot))
                    {
                        swapItemSlot(currentSlot, nextSlot);
                        return;
                    }

                    if (!exceededMaxStackLimit(currentSlot, nextSlot))
                    {
                        splitStackItemSlot(currentSlot, nextSlot);
                        return;
                    }
                    else
                    {
                        swapSplitStackItemSlot(currentSlot, nextSlot);
                        return;
                    }

                }

                swapItemSlot(currentSlot, nextSlot);
                return;


            case PointerEventData.InputButton.Middle:
                if (currentSlot.isSingleItem()) goto case PointerEventData.InputButton.Left;

                if (!nextSlot.hasItem())
                {
                    moveSingleItemSlot(currentSlot, nextSlot);
                    return;
                }


                if (isSameItemType(currentSlot, nextSlot))
                {

                    if (isFullStacked(currentSlot, nextSlot))
                    {
                        swapItemSlot(currentSlot, nextSlot);
                        return;
                    }

                    if (!exceedMaxStackLimit(1, nextSlot))
                    {
                        stackSingleItemSlot(currentSlot, nextSlot);
                        return;
                    }

                }
                swapItemSlot(currentSlot, nextSlot);
                return;

            default:
                return;
        }

    }

   protected virtual bool isSlot(GameObject slot)
    {
        return slot.GetComponent<InventorySlotScript>() != null;
    }

    protected virtual void resetIconsProperties()
    {
        transform.localPosition = Vector3.zero;
        currentSlot.setDisplayQuantity(true);
        GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }


    protected virtual void swapItemSlot(InventorySlotScript oldSlot, InventorySlotScript newSlot)
    {
        Item oldSlotItem = oldSlot.getItem();
        Item newSlotItem = newSlot.getItem();

        int newSlotQuantity = newSlot.getQuantity();
        int oldSlotQuantity = oldSlot.getQuantity();

        oldSlot.setItem(newSlotItem);
        oldSlot.setQuantity(newSlotQuantity);
        newSlot.setItem(oldSlotItem);
        newSlot.setQuantity(oldSlotQuantity);
        Debug.Log("Item Swapped", currentSlot.gameObject);
    }

    protected virtual void moveItemSlot(InventorySlotScript oldSlot, InventorySlotScript newSlot)
    {
        Item oldSlotItem = oldSlot.getItem();
        Item newSlotItem = newSlot.getItem();

        int oldSlotQuantity = oldSlot.getQuantity();

        newSlot.setQuantity(oldSlotQuantity);
        oldSlot.setQuantity(0);
        newSlot.setItem(oldSlotItem);
        oldSlot.setItem(null);
        Debug.Log("Item Moved", currentSlot.gameObject);
    }

    protected virtual void stackItemSlot(InventorySlotScript oldSlot, InventorySlotScript newSlot)
    {
        int newSlotQuantity = newSlot.getQuantity();
        int oldSlotQuantity = oldSlot.getQuantity();

        oldSlot.setItem(null);
        oldSlot.setQuantity(0);
        newSlot.setQuantity(oldSlotQuantity + newSlotQuantity);
        Debug.Log("Item Stacked", currentSlot.gameObject);
    }

    protected virtual void swapStackItemSlot(InventorySlotScript oldSlot, InventorySlotScript newSlot)
    {

        Item oldSlotItem = oldSlot.getItem();
        Item newSlotItem = newSlot.getItem();

        int newSlotQuantity = newSlot.getQuantity();
        int oldSlotQuantity = oldSlot.getQuantity();

        int restAfterFirstStack = (oldSlotQuantity + newSlotQuantity) - newSlot.getItemMaxStack();
        oldSlot.setQuantity(restAfterFirstStack);
        newSlot.setQuantity(oldSlot.getItemMaxStack());

        Debug.Log("Item Swap-Stacked", currentSlot.gameObject);
    }

    protected virtual void moveSplitItemSlot(InventorySlotScript oldSlot, InventorySlotScript newSlot)
    {
        int halfSlotScriptQuantity = Mathf.RoundToInt(oldSlot.getQuantity() / 2);
        int quantityWithoutHalf = oldSlot.getQuantity() - halfSlotScriptQuantity;

        newSlot.setQuantity(halfSlotScriptQuantity);
        oldSlot.setQuantity(quantityWithoutHalf);
        newSlot.setItem(oldSlot.getItem());
        Debug.Log("Item Moved-Split", currentSlot.gameObject);
    }

    protected virtual void splitStackItemSlot(InventorySlotScript oldSlot, InventorySlotScript newSlot)
    {
        int halfSlotScriptQuantity = Mathf.RoundToInt(oldSlot.getQuantity() / 2);
        int quantityWithoutHalf = oldSlot.getQuantity() - halfSlotScriptQuantity;

        int newSlotQuantity = newSlot.getQuantity();
        int oldSlotQuantity = oldSlot.getQuantity();

        oldSlot.setQuantity(quantityWithoutHalf);
        newSlot.setQuantity(oldSlotQuantity + newSlotQuantity);
        Debug.Log("Item Split-Stack", currentSlot.gameObject);
    }


    protected virtual void swapSplitStackItemSlot(InventorySlotScript oldSlot, InventorySlotScript newSlot)
    {

        int halfSlotScriptQuantity = Mathf.RoundToInt(oldSlot.getQuantity() / 2);
        int quantityWithoutHalf = oldSlot.getQuantity() - halfSlotScriptQuantity;

        int newSlotQuantity = newSlot.getQuantity();
        int oldSlotQuantity = oldSlot.getQuantity();

        int restAfterFirstStack = (oldSlotQuantity + newSlotQuantity) - newSlot.getItemMaxStack();
        oldSlot.setQuantity(quantityWithoutHalf + restAfterFirstStack);
        newSlot.setQuantity(oldSlot.getItemMaxStack());
        Debug.Log("Item Swap-Split-Stacked", currentSlot.gameObject);
    }

    protected virtual void moveSingleItemSlot(InventorySlotScript oldSlot, InventorySlotScript newSlot)
    {
        newSlot.setQuantity(1);
        oldSlot.removeQuantity(1);
        newSlot.setItem(oldSlot.getItem());
        Debug.Log("Item Single-Moved", currentSlot.gameObject);
    }

    protected virtual void stackSingleItemSlot(InventorySlotScript oldSlot, InventorySlotScript newSlot)
    {
        int newSlotQuantity = newSlot.getQuantity();

        oldSlot.removeQuantity(1);
        newSlot.setQuantity(1 + newSlotQuantity); ;
        Debug.Log("Item Single-Stacked", currentSlot.gameObject);
    }

    protected virtual bool isSameItemType(InventorySlotScript oldSlot, InventorySlotScript newSlot)
    {
        return oldSlot.getItem() == newSlot.getItem();
    }


    protected virtual bool isFullStacked(InventorySlotScript oldSlot, InventorySlotScript newSlot)
    {
        return oldSlot.getQuantity() == oldSlot.getItemMaxStack()
        || newSlot.getQuantity() == newSlot.getItemMaxStack()
        || !oldSlot.isStackable()
        || !newSlot.isStackable();
    }

    protected virtual bool exceededMaxStackLimit(InventorySlotScript oldSlot, InventorySlotScript newSlot)
    {
        return !(oldSlot.getQuantity() + newSlot.getQuantity() <= newSlot.getItemMaxStack());
    }

    protected virtual bool exceedMaxStackLimit(int oldSlotCustomQuantity, InventorySlotScript newSlot)
    {
        return !(oldSlotCustomQuantity + newSlot.getQuantity() <= newSlot.getItemMaxStack());
    }
}
